using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Signawel.Data.Abstractions.Repositories;
using Signawel.Domain;
using Signawel.Domain.Authentication;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Signawel.Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ILogger<AuthenticationRepository> _logger;
        private readonly SignawelDbContext _context;
        private readonly TokenConfiguration _tokenConfiguration;

        public AuthenticationRepository(ILogger<AuthenticationRepository> logger, SignawelDbContext context, TokenConfiguration tokenConfiguration)
        {
            _logger = logger;
            _context = context;
            _tokenConfiguration = tokenConfiguration;
        }

        /// <inheritdoc cref="IAuthenticationRepository.AddLoginRecordAsync(string, string, bool)"/>
        public async Task<DataResult<LoginRecord>> AddLoginRecordAsync(string userId, string ipAddress, bool succes)
        {
            if (userId == null)
            {
                _logger.LogWarning("Failed to create LoginRecord. UserId was null.");
                return DataResult<LoginRecord>.WithPublicError(ErrorCodes.ParameterEmptyError, "UserId is empty.");
            }

            LoginRecord loginRecord = new LoginRecord
            {
                UserId = userId,
                IpAddress = ipAddress,
                Succes = succes
            };

            var result = await _context.LoginRecords.AddAsync(loginRecord);
            await _context.SaveChangesAsync();

            if(result.Entity == null)
            {
                _logger.LogWarning("Failed to create LoginRecord for user {userId}", userId);
                return DataResult<LoginRecord>.WithPublicError(ErrorCodes.LoginRecordError, "Failed to create a LoginRecord.");
            }

            return DataResult<LoginRecord>.WithEntityOrError(result.Entity, ErrorCodes.LoginRecordError, "Failed to create a LoginRecord.", DataErrorVisibility.Public);
        }

        /// <inheritdoc cref="IAuthenticationRepository.CreateRefreshTokenAsync(string, string)"/>
        public async Task<DataResult<RefreshToken>> CreateRefreshTokenAsync(string userId, string jwtId)
        {
            if(userId == null)
            {
                _logger.LogWarning("Failed to create RefreshToken. UserId was null.");
                return DataResult<RefreshToken>.WithPublicError(ErrorCodes.ParameterEmptyError, "UserId is empty.");
            }
            if(jwtId == null)
            {
                _logger.LogWarning("Failed to create RefreshToken. JwtId was null.");
                return DataResult<RefreshToken>.WithPublicError(ErrorCodes.ParameterEmptyError, "JwtId is empty.");
            }

            RefreshToken refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                UserId = userId,
                JwtId = jwtId,
                ExpiryDate = DateTime.Now.AddMonths(_tokenConfiguration.RefreshTokenLifetime)
            };

            var result = await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            if (result.Entity == null)
            {
                _logger.LogWarning("Failed to create RefreshToken for user {userId}.", userId);
                return DataResult<RefreshToken>.WithPublicError(ErrorCodes.RefreshTokenError, "Something went wrong during the refreshtoken creation process.");
            }

            return DataResult<RefreshToken>.WithEntityOrError(result.Entity, ErrorCodes.RefreshTokenError, "Something went wrong during the refreshtoken creation process.", DataErrorVisibility.Public);
        }   

        /// <inheritdoc cref="IAuthenticationRepository.GetRefreshTokenByTokenAsync(string)"/>
        public async Task<DataResult<RefreshToken>> GetRefreshTokenByTokenAsync(string requestRefreshToken)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(e => e.Token == requestRefreshToken);

            if(refreshToken == null)
            {
                _logger.LogInformation("RefreshToken {token} not found", requestRefreshToken);
                return DataResult<RefreshToken>.WithPublicError(ErrorCodes.NotFoundError, "Invalid RefreshToken.");
            }

            return DataResult<RefreshToken>.WithEntityOrError(refreshToken, ErrorCodes.RefreshTokenError, "Something went wrong during token refresh process", DataErrorVisibility.Public);
        }

        /// <inheritdoc cref="IAuthenticationRepository.UpdateRefreshTokenAsync(RefreshToken)"/>
        public async Task<DataResult<RefreshToken>> UpdateRefreshTokenAsync(RefreshToken storedRefreshToken)
        {
            var result = _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            if (result.Entity == null)
            {
                _logger.LogWarning("Failed to update RefreshToken {id}", storedRefreshToken.Id);
                return DataResult<RefreshToken>.WithPublicError(ErrorCodes.RefreshTokenError, "Failed to update RefreshToken");
            }

            return DataResult<RefreshToken>.WithEntityOrError(result.Entity, ErrorCodes.RefreshTokenError, "Failed to update RefreshToken", DataErrorVisibility.Public);
        }

        #region Private Methods

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        #endregion
    }
}
