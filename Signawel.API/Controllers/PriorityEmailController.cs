using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain;
using Signawel.Dto;
using Signawel.Dto.PriorityEmail;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signawel.API.Controllers
{
    [ApiController]
    [JwtTokenAuthorize(Roles = Role.Constants.Admin)]
    [Route("api/priority-emails")]
    public class PriorityEmailController : BaseController
    {
        private readonly IPriorityEmailService _priorityEmailService;

        public PriorityEmailController(IPriorityEmailService priorityEmailService)
        {
            _priorityEmailService = priorityEmailService;
        }

        [HttpGet]
        [SwaggerOperation(nameof(GetAllPriorityEmails))]
        [SwaggerResponse(StatusCodes.Status200OK, "A list of all priority emails.", typeof(IList<PriorityEmailResponseDto>))]
        public async Task<IActionResult> GetAllPriorityEmails()
        {
            return Ok(await _priorityEmailService.GetPriorityEmails().ToListAsync());
        }

        [HttpPost]
        [SwaggerOperation(nameof(AddPriorityEmail))]
        [SwaggerResponse(StatusCodes.Status200OK, "Newly added priority email.", typeof(PriorityEmailResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed to add new priority email.", typeof(IList<ErrorResponseDto>))]
        public async Task<IActionResult> AddPriorityEmail(PriorityEmailCreationRequestDto dto)
        {
            var result = await _priorityEmailService.AddPriorityEmailAsync(dto);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result.Entity);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(nameof(DeletePriorityEmail))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Deleted successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed to delete priority email.", typeof(IList<ErrorResponseDto>))]
        public async Task<IActionResult> DeletePriorityEmail(string id)
        {
            var result = await _priorityEmailService.RemovePriorityEmailAsync(id);

            if (!result.Succeeded)
                return BadRequest(result);

            return NoContent();
        }

    }
}
