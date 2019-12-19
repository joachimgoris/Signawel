using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain;
using Signawel.Dto;
using Signawel.Dto.BlacklistEmail;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signawel.API.Controllers
{
    [ApiController]
    [JwtTokenAuthorize(Roles = Role.Constants.Admin)]
    [Route("api/blacklist-emails")]
    public class BlacklistEmailController : BaseController
    {
        private readonly IBlacklistEmailService _blacklistEmailService;

        public BlacklistEmailController(IBlacklistEmailService blacklistEmailService)
        {
            _blacklistEmailService = blacklistEmailService;
        }

        [HttpGet]
        [SwaggerOperation(nameof(GetAllBlacklistEmails))]
        [SwaggerResponse(StatusCodes.Status200OK, "A list of all blacklisted emails.", typeof(IList<BlacklistEmailCreationRequestDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed to retrieve all blacklisted emails.", typeof(IList<ErrorResponseDto>))]
        public async Task<IActionResult> GetAllBlacklistEmails()
        {
            var result = _blacklistEmailService.GetBlacklistEmails();

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok(await result.Entity.ToListAsync());
        }

        [HttpPost]
        [SwaggerOperation(nameof(AddBlacklistEmail))]
        [SwaggerResponse(StatusCodes.Status200OK, "Newly added blacklist email.", typeof(BlacklistEmailResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed to add new blacklisted email.", typeof(IList<ErrorResponseDto>))]
        public async Task<IActionResult> AddBlacklistEmail(BlacklistEmailCreationRequestDto dto)
        {
            var result = await _blacklistEmailService.AddBlacklistEmailAsync(dto);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok(result.Entity);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(nameof(DeleteBlacklistEmail))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Deleted successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed to delete blacklist email.", typeof(IList<ErrorResponseDto>))]
        public async Task<IActionResult> DeleteBlacklistEmail(string id)
        {
            var result = await _blacklistEmailService.RemoveBlacklistEmailAsync(id);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return NoContent();
        }
    }
}
