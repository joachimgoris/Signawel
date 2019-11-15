using Signawel.Domain.Authentication.Models;
using Signawel.Dto;
using Signawel.Dto.Authentication;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        ///     Confirm the email address of a user. Using the <see cref="UserManager{TUser}.GenerateEmailConfirmationTokenAsync"/> token.
        /// </summary>
        /// <param name="userId">
        ///     Id of the <see cref="User"/> to confirm the email address of.
        /// </param>
        /// <param name="confirmationToken">
        ///     Email configuration token as created by <see cref="UserManager{TUser}.GenerateEmailConfirmationTokenAsync"/>.
        /// </param>
        /// <returns>
        ///     A bool resembling success or failure.
        /// </returns>
        Task<bool> ConfirmEmailAsync(string userId, string confirmationToken);

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
        ///     A bool resembling success or failure.
        /// </returns>
        Task<TokenResponseDto> LoginEmailAsync(string email, string password, string ipAddress);

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
        ///     A bool resembling success or failure.
        /// </returns>
        Task<RegisterResponseDto> RegisterAsync(string email, string password);

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
        ///     A bool resembling succes or failure.
        /// </returns>
        Task<TokenResponseDto> RefreshJwtTokenAsync(string jwtToken, string refreshToken);
    }
}
