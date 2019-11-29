using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Signawel.Business.Abstractions.Services;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using Signawel.API.Extensions;
using Signawel.Dto.Authentication;
using Signawel.API.Attributes;
using Signawel.Domain.DataResults;
using Signawel.Domain.Constants;
using System.Collections.Generic;

namespace Signawel.API.Controllers
{
    [ApiController]
    [JwtTokenAuthorize]
    [Route("api/authentication")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        #region Login

        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerOperation("login")]
        [SwaggerResponse(StatusCodes.Status200OK, "User successfully logged in.", typeof(TokenResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error occurred while attempting to login the user.", typeof(IList<DataError>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto model)
        {
            var result = await _authenticationService.LoginEmailAsync(model.Email, model.Password, HttpContext.GetRemoteIpAddress().ToString());

            if (result.HasError(ErrorCodes.NotFoundError))
                return NotFound();

            if (!result.Succeeded) return BadRequest(result);

            return Ok(result.Entity);
        }

        #endregion

        #region Register

        [HttpPost("register")]
        [AllowAnonymous]
        [SwaggerOperation("register")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "User was registered.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error has occurred while attempting to register.", typeof(IList<DataError>))]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var result = await _authenticationService.RegisterAsync(model.Email, model.Password);

            if (!result.Succeeded)
                return BadRequest(result);
            
            return NoContent();
        }

        #endregion

        #region RefreshToken

        [HttpPost("refresh")]
        [AllowAnonymous]
        [SwaggerOperation("refresh")]
        [SwaggerResponse(StatusCodes.Status200OK, "Token was refreshed.", typeof(TokenResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error has occurred while attempting to refresh the token", typeof(IList<DataError>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Refresh failed.")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshRequestDto model)
        {
            var result = await _authenticationService.RefreshJwtTokenAsync(model.JwtToken, model.RefreshToken);

            if (result.HasError(ErrorCodes.JwtTokenError))
                return Unauthorized();

            if (result.HasError(ErrorCodes.NotFoundError))
                return NotFound();

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result.Entity);
        }

        #endregion

        #region ConfirmEmail

        [HttpPost("confirmemail")]
        [AllowAnonymous]
        [SwaggerOperation("confirmEmailAddress")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The email address has been confirmed.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error has occured while attempting to confirm the email address", typeof(IList<DataError>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ConfirmEmail([FromBody] EmailConfirmRequestDto request)
        {
            var result = await _authenticationService.ConfirmEmailAsync(request);

            if (result.HasError(ErrorCodes.NotFoundError))
                return NotFound();

            if (!result.Succeeded)
                return BadRequest(result);

            return NoContent();
        }


        #endregion

        #region GenerateForgotPasswordToken

        [HttpPost("generateforgotpasswordtoken")]
        [AllowAnonymous]
        [SwaggerOperation("generateforgotpasswordtoken")]
        [SwaggerResponse(StatusCodes.Status200OK, "Forgot password token generated.", typeof(ForgotPasswordTokenResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error has occurred while attempting generate a ForgotPasswordToken.", typeof(DataResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GenerateForgotPasswordToken([FromBody] ForgotPasswordTokenRequestDto model)
        {
            var result = await _authenticationService.GenerateForgotPasswordTokenAsync(model);

            if (result.HasError(ErrorCodes.NotFoundError))
                return NotFound();

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result.Entity);
        }

        #endregion

        #region ResetPassword

        [HttpPost("resetpassword")]
        [AllowAnonymous]
        [SwaggerOperation("resetpassword")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Password was reset")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error has occurred while attempting to set new password.", typeof(DataResult))]
        public async Task<ActionResult> ResetPasswordAsync([FromBody] PasswordResetDto model)
        {
            var result = await _authenticationService.ResetPasswordAsync(model);

            if (!result.Succeeded)
                return BadRequest(result);

            return NoContent();
        }

        #endregion
    }
}
