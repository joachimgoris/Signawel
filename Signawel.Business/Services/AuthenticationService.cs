using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Signawel.Business.Abstractions.Services;
using Signawel.Data.Abstractions.Repositories;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.Authentication;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Business.Services
{
    /// <inheritdoc cref="IAuthenticationService"/>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        private readonly IJwtTokenFactory _tokenFactory;
        private readonly UserManager<User> _userManager;
        
        public AuthenticationService(ILogger<AuthenticationService> logger, IAuthenticationRepository authenticationRepository, IUserService userService, IJwtTokenFactory jwtTokenFactory, UserManager<User> userManager,
            IMailService mailService)
        {
            _logger = logger;
            _authenticationRepository = authenticationRepository;
            _userService = userService;
            _userManager = userManager;
            _tokenFactory = jwtTokenFactory;
            _mailService = mailService;
        }

        #region ConfirmEmail

        /// <inheritdoc cref="IAuthenticationService.ConfirmEmailAsync(EmailConfirmRequestDto)"/>
        public async Task<DataResult> ConfirmEmailAsync(EmailConfirmRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                _logger.LogWarning("Unable to find user with id {userId}", request.UserId);
                return DataResult.WithPublicError(ErrorCodes.NotFoundError, "There is no user associated with this id in the database.");
            }

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                _logger.LogWarning("Failed to confirm email address of {user}", user.Id);
                return DataResult.WithPublicError(ErrorCodes.EmailNotConfirmedError, "The email failed to confirm.");
            }

            return DataResult.Success;
        }

        #endregion

        #region LoginEmail

        /// <inheritdoc cref="IAuthenticationService.LoginEmailAsync(string, string, string)"/>
        public async Task<DataResult<TokenResponseDto>> LoginEmailAsync(string email, string password, string ipAddress)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation("Login attempted, but username and password combination was incorrect. (ip: {ipAddress})", ipAddress);
                await _authenticationRepository.AddLoginRecordAsync(null, ipAddress, succes: false);
                return DataResult<TokenResponseDto>.WithPublicError(ErrorCodes.NotFoundError, "The user was not found in the database.");
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                _logger.LogInformation("Login attempted, but username and password combination was incorrect. (ip: {ipAddress})", ipAddress);
                await _authenticationRepository.AddLoginRecordAsync(user.Id, ipAddress, succes: false);
                return DataResult<TokenResponseDto>.WithPublicError(ErrorCodes.AuthenticationIncorrectCredentialsError, "Credentials are incorrect.");
            }
            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                _logger.LogInformation("Login attempted, but email was not confirmed.");
                return DataResult<TokenResponseDto>.WithPublicError(ErrorCodes.EmailNotConfirmedError, "The email associated to this account has not been confirmed.");
                
            }

            var token = await ReturnTokenResponseAsync(user);
            await _authenticationRepository.AddLoginRecordAsync(user.Id, ipAddress, succes: true);
            return DataResult<TokenResponseDto>.WithEntityOrError(token, ErrorCodes.LoginError, "Something went wrong during the login process.", DataErrorVisibility.Public);
        }

        #endregion

        #region RefreshJwtToken

        /// <inheritdoc cref="IAuthenticationService.RefreshJwtTokenAsync(string, string)"/>
        public async Task<DataResult<TokenResponseDto>> RefreshJwtTokenAsync(string jwtToken, string refreshToken)
        {
            var validatedTokenResult = _tokenFactory.GetPrincipalFromToken(jwtToken);

            if (!validatedTokenResult.Succeeded)
            {
                _logger.LogInformation("Unable to refresh tokens, JWT token {@token} invalid.", jwtToken);
                return DataResult<TokenResponseDto>.WithErrorsFromDataResult(validatedTokenResult);
            }

            var jti = validatedTokenResult.Entity.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshTokenResult = await _authenticationRepository.GetRefreshTokenByTokenAsync(refreshToken);

            if (DateTime.UtcNow > storedRefreshTokenResult.Entity.ExpiryDate ||
                storedRefreshTokenResult.Entity.Invalidated ||
                storedRefreshTokenResult.Entity.Used ||
                storedRefreshTokenResult.Entity.JwtId != jti)
            {
                _logger.LogInformation("Unable to refresh token, token {@token} invalid.", refreshToken);
                return DataResult<TokenResponseDto>.WithErrorsFromDataResult(storedRefreshTokenResult);
            }

            storedRefreshTokenResult.Entity.Used = true;
            var updateResult = await _authenticationRepository.UpdateRefreshTokenAsync(storedRefreshTokenResult.Entity);

            var userId = validatedTokenResult.Entity.Claims.Single(x => x.Type == "user_id").Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning("Unable to find user with id {userId}", userId);
                return DataResult<TokenResponseDto>.WithPublicError(ErrorCodes.NotFoundError, "User not found.");
            }

            return DataResult<TokenResponseDto>.WithEntityOrError(await ReturnTokenResponseAsync(user), ErrorCodes.RefreshTokenError, "Something went wrong during the refresh token process.", DataErrorVisibility.Public);
        }

        #endregion

        #region Register

        /// <inheritdoc cref="IAuthenticationService.RegisterAsync(string, string)"/>
        public async Task<DataResult<RegisterResponseDto>> RegisterAsync(string email, string password)
        {
            var user = new User
            {
                UserName = email.Split('@')[0],
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                _logger.LogWarning("Failed to create user.");
                return DataResult<RegisterResponseDto>.WithPublicError(ErrorCodes.UserCreationError, "Something went wrong during the process of creating a user.");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var mailResult = await _mailService.SendConfirmationEmailAsync(user, token);

            if (!mailResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return DataResult<RegisterResponseDto>.WithErrorsFromDataResult(mailResult);
            }

            _logger.LogInformation("User {userId} registered successfully.", user.Id);
            var responseDto = new RegisterResponseDto
            {
                Email = user.Email,
                UserName = user.UserName
            };

            return DataResult<RegisterResponseDto>.WithEntityOrError(responseDto, ErrorCodes.RegisterError, "Something went wrong during the register process.", DataErrorVisibility.Public);
        }

        #endregion

        #region Private Methods

        private async Task<TokenResponseDto> ReturnTokenResponseAsync(User user)
        {
            var claims = await _userService.GetUserClaimsAsync(user.Id);

            var tokenResult = await _tokenFactory.GenerateToken(user, claims);

            return new TokenResponseDto
            {
                Token = tokenResult.Entity.Token,
                RefreshToken = tokenResult.Entity.RefreshToken
            };
        }

        #endregion
    }
}
