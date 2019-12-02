using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace Signawel.API.Controllers
{
    [ApiController]
    [JwtTokenAuthorize]
    [Route("api/issues")]
    public class IssuesController : BaseController
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("default")]
        [SwaggerOperation("GetDefaultIssues")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of default issues", typeof(IList<DefaultIssueResponseDto>))]
        public async Task<ActionResult> GetDefaultIssues()
        {
            var data = _issueService.GetDefaultIssues();
            var list = await data.ToListAsync();
            return Ok(list);
        }
    }
}
