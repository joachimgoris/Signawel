using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto.Determination;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Signawel.API.Controllers
{
    [ApiController]
    //TODO : [JwtTokenAuthorize]
    [Route("api/determination-graph")]
    public class DeterminationGraphController : ControllerBase
    {
        private readonly IDeterminationService _determinationService;

        public DeterminationGraphController(IDeterminationService determinationService)
        {
            this._determinationService = determinationService;
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation("getDeterminationGraph")]
        [SwaggerResponse(StatusCodes.Status200OK, "Determination graph", typeof(DeterminationGraphResponseDto))]
        public async Task<ActionResult> GetDeterminationGraph()
        {
            return Ok(await _determinationService.GetGraphAsync());
        }

        [HttpPost]
        [SwaggerOperation("setDeterminationGraph")]
        [SwaggerResponse(StatusCodes.Status200OK, "Updated determination graph", typeof(DeterminationGraphResponseDto))]
        public async Task<ActionResult> SetDeterminationGraph([FromBody] DeterminationGraphCreationRequestDto dto)
        {
            var result = await _determinationService.SetGraphAsync(dto);
            return Ok(result);
        }

    }
}
