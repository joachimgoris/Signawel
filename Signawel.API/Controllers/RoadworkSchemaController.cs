using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.API.Controllers
{
    [ApiController]
    // TODO [JwtTokenAuthorize]
    [Route("api/roadwork-schemas")]
    public class RoadworkSchemaController : ControllerBase
    {
        private readonly IRoadworkSchemaService _schemaService;

        public RoadworkSchemaController(IRoadworkSchemaService schemaService)
        {
            this._schemaService = schemaService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation("getRoadworkSchema")]
        [SwaggerResponse(StatusCodes.Status200OK, "Roadwork schema", typeof(RoadworkSchemaResponseDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Roadwork schema was not found")]
        public async Task<IActionResult> GetRoadworkSchema(string id)
        {
            var schema = await _schemaService.GetRoadworkSchema(id);

            if(schema == null)
                return NotFound();

            return Ok(schema);
        }

        [HttpGet]
        [SwaggerOperation("getAllRoadworkSchemas")]
        [SwaggerResponse(StatusCodes.Status200OK, "All roadwork schema", typeof(RoadworkSchemaPaginationResponseDto))]
        public async Task<IActionResult> GetAllRoadworkSchemas([FromQuery] string search = null, [FromQuery] int page = 0, [FromQuery] int limit = 20)
        {
            var schemas = _schemaService.GetAllRoadworkSchemas();

            var result = new RoadworkSchemaPaginationResponseDto
            {
                Total = schemas.Count()
            };

            if (page < 0)
            {
                page = 0;
            }

            if(limit <= 0)
            {
                limit = -1;
            }

            var schemaResult = schemas.Skip(page * limit);

            if(limit != -1)
                schemaResult = schemaResult.Take(limit);

            if(!string.IsNullOrEmpty(search))
                schemaResult = schemaResult.Where(x => x.Name.Contains(search));

            result.Schemas = await schemaResult.ToListAsync();

            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation("createRoadworkSchema")]
        [SwaggerResponse(StatusCodes.Status200OK, "The created roadwork schema", typeof(RoadworkSchemaResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something whent wrong")]
        public async Task<IActionResult> AddRoadworkSchema([FromBody] RoadworkSchemaCreationRequestDto dto)
        {
            var creation = await _schemaService.CreateRoadworkSchema(dto);

            if(creation == null)
                return BadRequest();

            return Ok(creation);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("putUpdateRoadworkSchema")]
        [SwaggerResponse(StatusCodes.Status200OK, "The updated roadwork schema", typeof(RoadworkSchemaResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something whent wrong")]
        public async Task<IActionResult> PutRoadworkSchema(string id, [FromBody] RoadworkSchemaPutRequestDto dto)
        {
            var updated = await _schemaService.PutRoadworkSchema(id, dto);

            if(updated == null)
                return BadRequest();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("deleteRoadworkSchema")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Deleted roadwork schema successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something whent wrong")]
        public async Task<IActionResult> DeleteRoadworkSchema(string id)
        {
            var result = await _schemaService.DeleteRoadworkSchema(id);

            if (!result)
                return BadRequest();

            return NoContent();
        }

    }
}
