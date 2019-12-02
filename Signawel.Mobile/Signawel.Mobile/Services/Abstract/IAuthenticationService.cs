using Signawel.Dto.Authentication;
using System.Threading.Tasks;

namespace Signawel.Mobile.Services.Abstract
{
    public interface IAuthenticationService
    {
        /// <summary>
        ///     Method to login to the Signawel api.
        /// </summary>
        /// <param name="email">
        ///     The email of the user.
        /// </param>
        /// <param name="password">
        ///     The password of the user
        /// </param>
        /// <returns>
        ///     Returns an instance of <see cref="TokenResponseDto"/>.
        /// </returns>
        Task<TokenResponseDto> LoginAsync(string email, string password);

        /// <summary>
        ///     Method to register to the Signawel api.
        /// </summary>
        /// <param name="email">
        ///     The email of the user.
        /// </param>
        /// <param name="password">
        ///     The pasword of the user.
        /// </param>
        /// <param name="passwordRepeat">
        ///     The repeated password of the user.
        /// </param>
        /// <returns>
        ///     Returns an instance of <see cref="RegisterResponseDto"/>.
        /// </returns>
        Task<RegisterResponseDto> RegisterAsync(string email, string password, string passwordRepeat);

        /// <summary>
        ///     Check if someone is authenticated
        /// </summary>
        /// <returns>
        ///     True if the application is authenticated.
        /// </returns>
        Task<bool> IsAuthenticatedAsync();

        /// <summary>
        ///     Logout
        /// </summary>
        Task Logout();
    }
}
