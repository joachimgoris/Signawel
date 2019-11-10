using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Signawel.Data.Abstractions.Repositories;
using Signawel.Domain;
using Signawel.Domain.Authentication;
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
        public async Task<LoginRecord> AddLoginRecordAsync(string userId, string ipAddress, bool succes)
        {
            if (userId == null)
            {
                _logger.LogWarning("Failed to create LoginRecord. UserId was null.");
                return null;
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
                return null;
            }

            return result.Entity;
        }

        /// <inheritdoc cref="IAuthenticationRepository.CreateRefreshTokenAsync(string, string)"/>
        public async Task<RefreshToken> CreateRefreshTokenAsync(string userId, string jwtId)
        {
            if(userId == null)
            {
                _logger.LogWarning("Failed to create RefreshToken. UserId was null.");
                return null;
            }
            if(jwtId == null)
            {
                _logger.LogWarning("Failed to create RefreshToken. JwtId was null.");
                return null;
            }

            RefreshToken refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                UserId = userId,
                JwtId = jwtId,
                ExpiryDate = DateTime.UtcNow.AddMonths(_tokenConfiguration.RefreshTokenLifetime)
            };

            var result = await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            if (result.Entity == null)
            {
                _logger.LogWarning("Failed to create RefreshToken for user {userId}.", userId);
                return null;
            }

            return result.Entity;
        }   

        /// <inheritdoc cref="IAuthenticationRepository.GetRefreshTokenByTokenAsync(string)"/>
        public async Task<RefreshToken> GetRefreshTokenByTokenAsync(string requestRefreshToken)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(e => e.Token == requestRefreshToken);

            if(refreshToken == null)
            {
                _logger.LogInformation("RefreshToken {token} not found", requestRefreshToken);
                return null;
            }

            return refreshToken;
        }

        /// <inheritdoc cref="IAuthenticationRepository.UpdateRefreshTokenAsync(RefreshToken)"/>
        public async Task<RefreshToken> UpdateRefreshTokenAsync(RefreshToken storedRefreshToken)
        {
            var result = _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            if (result.Entity == null)
            {
                _logger.LogWarning("Failed to update RefreshToken {id}", storedRefreshToken.Id);
                return null;
            }

            return result.Entity;
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
