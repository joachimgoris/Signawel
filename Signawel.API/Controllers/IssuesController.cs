using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Dto.DefaultIssue;
using Swashbuckle.AspNetCore.Annotations;

namespace Signawel.API.Controllers
{
    [ApiController]
    [JwtTokenAuthorize(Roles = Role.Constants.Admin)]
    [Route("api/issues/default")]
    public class IssuesController : BaseController
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation("GetDefaultIssues")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of default issues", typeof(IList<DefaultIssueResponseDto>))]
        public async Task<ActionResult> GetDefaultIssues()
        {
            var data = _issueService.GetDefaultIssues();
            var list = await data.ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        [SwaggerOperation("AddDefaultIssue")]
        [SwaggerResponse(StatusCodes.Status201Created, "Created successfully", typeof(DefaultIssueResponseDto))]
        public async Task<IActionResult> AddDefaultIssue([FromBody] DefaultIssueRequestDto dto)
        {
            var result = await _issueService.AddDefaultIsueAsync(dto);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok(result.Entity);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteDefaultIssue")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Delete successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Default issue not found")]
        public async Task<IActionResult> DeleteDefaultIssue(string id)
        {
            var result = await _issueService.DeleteDefaultIssueAsync(id);

            if(result.HasError(ErrorCodes.NotFoundError))
            {
                return NotFound(result);
            }

            if(!result.Succeeded)
            {
                return BadRequest(result);
            }

            return NoContent();
        }
    }
}
