using Signawel.Domain;
using Signawel.Domain.DataResults;
using System.Threading.Tasks;

namespace Signawel.Data.Abstractions.Repositories
{
    public interface IAuthenticationRepository
    {
        /// <summary>
        ///     Create a new <see cref="LoginRecord"/> in the database for an attempted login.
        /// </summary>
        /// <param name="userId">
        ///     Id of the <see cref="User"/> that attempted to login.
        /// </param>
        /// <param name="ipAddress">
        ///     Origin address of the attempted login.
        /// </param>
        /// <param name="succes">
        ///     If the login is a succes.
        /// </param>
        /// <returns>
        ///     The newly created LoginRecord.
        /// </returns>
        Task<DataResult<LoginRecord>> AddLoginRecordAsync(string userId, string ipAddress, bool succes);

        /// <summary>
        ///     Create a new <see cref="RefreshToken"/> for a user.
        /// </summary>
        /// <param name="userId">
        ///     Id of the <see cref="User"/> to create the RefreshToken for.
        /// </param>
        /// <param name="jwtId">
        ///     Id of the JWT token that the RefreshToken is linked to.
        /// </param>
        /// <returns>
        ///     The newly created RefreshToken.
        /// </returns>
        Task<DataResult<RefreshToken>> CreateRefreshTokenAsync(string userId, string jwtId);

        /// <summary>
        ///     Get a <see cref="RefreshToken"/> based on the token value.
        /// </summary>
        /// <param name="requestRefreshToken">
        ///     Token value of the RefreshToken.
        /// </param>
        /// <returns>
        ///     The existing RefreshToken.
        /// </returns>
        Task<DataResult<RefreshToken>> GetRefreshTokenByTokenAsync(string requestRefreshToken);

        /// <summary>
        ///     Update an existing <see cref="RefreshToken"/>.
        /// </summary>
        /// <param name="storedRefreshToken">
        ///     <see cref="RefreshToken"/> to be updated.
        /// </param>
        /// <returns>
        ///     The updated RefreshToken.
        /// </returns>
        Task<DataResult<RefreshToken>> UpdateRefreshTokenAsync(RefreshToken storedRefreshToken);
    }
}
