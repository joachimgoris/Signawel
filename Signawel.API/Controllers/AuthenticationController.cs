using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Signawel.Business.Abstractions.Services;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using Signawel.API.Extensions;
using Signawel.Dto.Authentication;
using Signawel.API.Attributes;

namespace Signawel.API.Controllers
{
    [ApiController]
    [JwtTokenAuthorize]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
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
        [SwaggerResponse(StatusCodes.Status200OK, "User succesfully logged in.", typeof(TokenResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error occurred while attempting to login the user.")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto model)
        {
            TokenResponseDto result = await _authenticationService.LoginEmailAsync(model.Email, model.Password, HttpContext.GetRemoteIpAddress().ToString());

            if (result == null) return BadRequest();

            return Ok(result);
        }

        #endregion

        #region Register

        [HttpPost("register")]
        [AllowAnonymous]
        [SwaggerOperation("register")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "User was registered.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error has occurred while attempting to register.")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var result = await _authenticationService.RegisterAsync(model.Email, model.Password);

            if (result == null) return BadRequest();
            
            return NoContent();
        }

        #endregion
    }
}
