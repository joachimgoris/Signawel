using Signawel.Domain.DataResults;
using Signawel.Dto.Authentication;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    /// <summary>
    ///     The service that manages authentication in the api.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        ///     Confirm the email address of a user. Using the <see cref="UserManager{TUser}.GenerateEmailConfirmationTokenAsync"/> token.
        /// </summary>
        /// <param name="request">
        ///     An instance of <see cref="EmailConfirmRequestDto"/> containing the userId and the token of the request.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult"/>
        /// </returns>
        Task<DataResult> ConfirmEmailAsync(EmailConfirmRequestDto request);

        /// <summary>
        ///     Login a user.
        /// </summary>
        /// <param name="email">
        ///     Email of the <see cref="User"/>.
        /// </param>
        /// <param name="password">
        ///     Password of the <see cref="User"/>.
        /// </param>
        /// <param name="ipAddress">
        ///     Ip address of the login request.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="TokenResponseDto"/>.
        /// </returns>
        Task<DataResult<TokenResponseDto>> LoginEmailAsync(string email, string password, string ipAddress);

        /// <summary>
        ///     Register a new user.
        /// </summary>
        /// <param name="email">
        ///     Email of the <see cref="User"/>.
        /// </param>
        /// <param name="password">
        ///     Password of the <see cref="User"/>.
        /// </param>
        /// <param name="repeatedPassword">
        ///     Repeated password of the <see cref="User"/>.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="RegisterResponseDto"/>.
        /// </returns>
        Task<DataResult<RegisterResponseDto>> RegisterAsync(string email, string password);

        /// <summary>
        ///     Refresh a JWT token.
        /// </summary>
        /// <param name="jwtToken">
        ///     The expired JWT token.
        /// </param>
        /// <param name="refreshToken">
        ///     The valid RefreshToken.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="TokenResponseDto"/>.
        /// </returns>
        Task<DataResult<TokenResponseDto>> RefreshJwtTokenAsync(string jwtToken, string refreshToken);
    }
}
