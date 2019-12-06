using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain;
using Signawel.Dto.Determination;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Signawel.API.Controllers
{
    /// <summary>
    ///     Controller for creating and viewing the determination graph
    /// </summary>
    [ApiController]
    [JwtTokenAuthorize]
    [Route("api/determination-graph")]
    public class DeterminationGraphController : BaseController
    {
        private readonly IDeterminationService _determinationService;

        public DeterminationGraphController(IDeterminationService determinationService)
        {
            this._determinationService = determinationService;
        }

        /// <summary>
        ///     Endpoint for getting the determination graph.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation("getDeterminationGraph")]
        [SwaggerResponse(StatusCodes.Status200OK, "Determination graph", typeof(DeterminationGraphResponseDto))]
        public async Task<ActionResult> GetDeterminationGraph()
        {
            return Ok(await _determinationService.GetGraphAsync());
        }

        /// <summary>
        ///     Endpoint for setting the determionation graph.
        /// </summary>
        /// <param name="dto">
        ///     Instance of <see cref="DeterminationGraphCreationRequestDto"/> containg details about the determination graph to set in the database.
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("setDeterminationGraph")]
        [Authorize(Roles = Role.Constants.Admin)]
        [SwaggerResponse(StatusCodes.Status200OK, "Updated determination graph", typeof(DeterminationGraphResponseDto))]
        public async Task<ActionResult> SetDeterminationGraph([FromBody] DeterminationGraphCreationRequestDto dto)
        {
            var result = await _determinationService.SetGraphAsync(dto);
            return Ok(result);
        }

    }
}
