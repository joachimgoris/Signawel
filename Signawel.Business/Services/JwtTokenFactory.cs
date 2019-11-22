using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Signawel.Business.Abstractions.Services;
using Signawel.Data.Abstractions.Repositories;
using Signawel.Data.Repositories;
using Signawel.Domain;
using Signawel.Domain.Authentication;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Business.Services
{
    public class JwtTokenFactory : IJwtTokenFactory
    {
        private readonly ILogger<JwtTokenFactory> _logger;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IAuthenticationRepository _authenticationRepository;

        public JwtTokenFactory(ILogger<JwtTokenFactory> logger, IOptions<TokenConfiguration> tokenConfiguration, TokenValidationParameters tokenValidationParameters, IAuthenticationRepository authenticationRepository)
        {
            _logger = logger;
            _tokenConfiguration = tokenConfiguration.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _authenticationRepository = authenticationRepository;
        }

        public async Task<DataResult<TokenResponseDto>> GenerateToken(User user, ICollection<Claim> additionalClaims)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            claims.AddRange(additionalClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _tokenConfiguration.Audience,
                Issuer = _tokenConfiguration.Issuer,
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.Add(_tokenConfiguration.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenConfiguration.Secret)),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            if(token == null)
            {
                _logger.LogWarning("Failed to create a JWT token for user {userId}", user.Id);
                return DataResult<TokenResponseDto>.WithPublicError(ErrorCodes.JwtTokenError, "Failed to create a JWT token.");
            }

            var refreshToken = await _authenticationRepository.CreateRefreshTokenAsync(user.Id, token.Id);

            var tokenResponse = new TokenResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Entity.Token
            };

            return DataResult<TokenResponseDto>.WithEntityOrError(tokenResponse, ErrorCodes.JwtTokenError, "Something went wrong during the JWT creation process.", DataErrorVisibility.Public);
        }

        public DataResult<ClaimsPrincipal> GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                _tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                return DataResult<ClaimsPrincipal>.WithEntityOrError(!IsJwtWithValidSecurityAlgorithm(validatedToken) ? null : principal,
                    ErrorCodes.PrincipalTokenError, "Something went wrong during the get PrincipalToken process.", DataErrorVisibility.Public);
            }
            catch (Exception)
            {
                return DataResult<ClaimsPrincipal>.WithPublicError(ErrorCodes.PrincipalTokenError, "Something went wrong during the get PrincipalToken process.");
            }
            finally
            {
                _tokenValidationParameters.ValidateLifetime = true;
            }
        }

        #region Private Methods

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
} 
