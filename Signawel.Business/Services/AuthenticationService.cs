using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Signawel.Business.Abstractions.Services;
using Signawel.Data.Abstractions.Repositories;
using Signawel.Domain;
using Signawel.Dto.Authentication;
using System.Threading.Tasks;

namespace Signawel.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IAuthenticationRepository _authenticationRepository;

        private readonly IUserService _userService;
        private readonly IJwtTokenFactory _tokenFactory;

        private readonly UserManager<User> _userManager;

        public AuthenticationService(ILogger<AuthenticationService> logger, IAuthenticationRepository authenticationRepository, IUserService userService, IJwtTokenFactory jwtTokenFactory, UserManager<User> userManager)
        {
            _logger = logger;
            _authenticationRepository = authenticationRepository;
            _userService = userService;
            _userManager = userManager;
            _tokenFactory = jwtTokenFactory;
        }

        public Task<bool> ConfirmEmailAsync(string userId, string confirmationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TokenResponseDto> LoginEmailAsync(string email, string password, string ipAddress)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                _logger.LogInformation("Login attempted, but username and password combination was incorrect. (ip: {ipAddress})", ipAddress);
                await _authenticationRepository.AddLoginRecordAsync(null, ipAddress, succes: false);
                return null;
            }

            if(!await _userManager.CheckPasswordAsync(user, password))
            {
                _logger.LogInformation("Login attempted, but username and password combination was incorrect. (ip: {ipAddress})", ipAddress);
                await _authenticationRepository.AddLoginRecordAsync(user.Id, ipAddress, succes: false);
                return null;
            }

            var token = await ReturnTokenResponseAsync(user);
            await _authenticationRepository.AddLoginRecordAsync(user.Id, ipAddress, succes: true);
            return token;
        }



        public Task<bool> RefreshJwtTokenAsync(string jwtToken, string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RegisterResponseDto> RegisterAsync(string email, string password)
        {
            var user = new User
            {
                UserName = email.Split('@')[0],
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if(result == null)
            {
                _logger.LogWarning("Failed to create user.");
                return null;
            }

            _logger.LogInformation("User {userId} registered successfully.", user.Id);
            return new RegisterResponseDto
            {
                Email = user.Email,
                UserName = user.UserName
            };
        }


        #region Private Methods

        private async Task<TokenResponseDto> ReturnTokenResponseAsync(User user)
        {
            var claims = await _userService.GetUserClaimsAsync(user.Id);

            var tokenResult = await _tokenFactory.GenerateToken(user, claims);

            return new TokenResponseDto
            {
                Token = tokenResult.Token,
                RefreshToken = tokenResult.RefreshToken
            };
        }

        #endregion
    }
}
