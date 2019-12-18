
using Signawel.Domain;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Signawel.Domain.DataResults;
using Signawel.Dto.Authentication;

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
        ///     An instance of <see cref="DataResult{UserResponseDto}"/> containing the new <see cref="User"/>.
        /// </returns>
        Task<DataResult<UserResponseDto>> CreateUserAsync(UserCreateRequestDto model);

        /// <summary>
        ///     Delete a <see cref="User"/>.
        /// </summary>
        /// <param name="userId">
        ///     The id of the <see cref="User"/>.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult"/>.
        /// </returns>
        Task<DataResult> DeleteUserAsync(string userId);

        /// <summary>
        ///     Get a <see cref="User"/>.
        /// </summary>
        /// <param name="userId">
        ///     The id of the <see cref="User"/>.
        /// </param>
        /// <returns>
        ///     The specified <see cref="UserResponseDto"/>.
        /// </returns>
        Task<DataResult<UserResponseDto>> GetUserAsync(string userId);

        // TODO: Filter/limit users output
        /// <summary>
        ///     Get all users.
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="ICollection{GetUserDto}"/>.
        /// </returns>
        Task<DataResult<ICollection<Dto.ReportGroup.UserResponseDto>>> GetAllUsersAsync();

        /// <summary>
        ///     Modify a <see cref="User"/>.
        /// </summary>
        /// <param name="userId">
        ///     The id of the <see cref="User"/>.
        /// </param>
        /// <param name="modifiedUser">
        ///     <see cref="UserModifyRequestDto"/> containing all the changes to the user.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="UserResponseDto"/> containing the modified user.
        /// </returns>
        Task<DataResult> ModifyUserAsync(string userId, UserModifyRequestDto model);

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
