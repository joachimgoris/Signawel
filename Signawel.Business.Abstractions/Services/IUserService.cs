
using Signawel.Domain;
using Signawel.Domain.Authentication.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IUserService
    {
        /// <summary>
        ///     Create a new <see cref="User"/>
        /// </summary>
        /// <param name="model">
        ///     Model containing all the data to create a new <see cref="User"/>.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="GetUserDto"/> containing the new <see cref="User"/>.
        /// </returns>
        Task<GetUserDto> CreateUserAsync(CreateUserDto model);

        /// <summary>
        ///     Delete a <see cref="User"/>.
        /// </summary>
        /// <param name="userId">
        ///     The id of the <see cref="User"/>.
        /// </param>
        /// <returns>
        ///     A bool representing success or failure.
        /// </returns>
        Task<bool> DeleteUserasync(string userId);

        /// <summary>
        ///     Get a <see cref="User"/>.
        /// </summary>
        /// <param name="userId">
        ///     The id of the <see cref="User"/>.
        /// </param>
        /// <returns>
        ///     The specified <see cref="GetUserDto"/>.
        /// </returns>
        Task<GetUserDto> GetUserAsync(string userId);

        // TODO: Filter/limit users output
        /// <summary>
        ///     Get all users.
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="ICollection{GetUserDto}"/>.
        /// </returns>
        ICollection<GetUserDto> GetAllUsersAsync();

        /// <summary>
        ///     Modify a <see cref="User"/>.
        /// </summary>
        /// <param name="userId">
        ///     The id of the <see cref="User"/>.
        /// </param>
        /// <param name="modifiedUser">
        ///     <see cref="ModifyUserDto"/> containing all the changes to the user.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="GetUserDto"/> containing the modified user.
        /// </returns>
        Task<GetUserDto> ModifyUserAsync(string userId, ModifyUserDto modifiedUser);

        /// <summary>
        ///     Get all claims of a <see cref="User"/>.
        /// </summary>
        /// <param name="userId">
        ///     Id of the <see cref="User"/>.
        /// </param>
        /// <param name="includeRoles">
        ///     Bool to include roles of the user.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="ICollection{Claim}"/>.
        /// </returns>
        Task<ICollection<Claim>> GetUserClaimsAsync(string userId, bool includeRoles = true);
    }
}
