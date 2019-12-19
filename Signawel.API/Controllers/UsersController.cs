using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.Authentication;
using Swashbuckle.AspNetCore.Annotations;

namespace Signawel.API.Controllers
{
    [ApiController]
    [JwtTokenAuthorize(Roles = Role.Constants.Admin)]
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #region GetUser

        [HttpGet]
        [SwaggerOperation(nameof(GetUserAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, "Retrieved user", typeof(UserResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something went wrong.", typeof(IList<DataError>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "There was no user with this id.")]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            var result = await _userService.GetUserAsync(id);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            
            if (result.HasError(ErrorCodes.NotFoundError))
            {
                return NotFound();
            }

            return BadRequest(result);
        }

        #endregion
        
        #region CreateUser

        [HttpPost]
        [SwaggerOperation(nameof(AddUserAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, "Created user", typeof(UserResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something went wrong", typeof(IList<DataError>))]
        public async Task<IActionResult> AddUserAsync([FromBody] UserCreateRequestDto model)
        {
            var result = await _userService.CreateUserAsync(model);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok(result.Entity);
        }
        
        #endregion

        #region ModifyUser

        [HttpPatch]
        [SwaggerOperation(nameof(ModifyUserAsync))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Modified user")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something went wrong", typeof(IList<DataError>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "There wasn't a user with the given id.", typeof(IList<DataError>))]
        public async Task<IActionResult> ModifyUserAsync([FromBody] UserModifyRequestDto model, [FromQuery] string userId)
        {
            var result = await _userService.ModifyUserAsync(userId, model);
            
            if (!result.Succeeded)
            {
                if (result.HasError(ErrorCodes.NotFoundError))
                {
                    return NotFound(result);
                }

                return BadRequest(result);
            }

            return NoContent();
        }

        #endregion

        #region DeleteUser

        [HttpDelete]
        [SwaggerOperation(nameof(DeleteUserAsync))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "User successfully deleted")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something went wrong.", typeof(IList<DataError>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "There wasn't a user with the given id.", typeof(IList<DataError>))]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var result = await _userService.DeleteUserAsync(userId);

            if (!result.Succeeded)
            {
                if (result.HasError(ErrorCodes.NotFoundError))
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }

            return NoContent();
        }

        #endregion
    }
}