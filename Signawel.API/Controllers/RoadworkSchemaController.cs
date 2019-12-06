using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;
using Signawel.Dto.RoadworkSchema;
using Signawel.API.Attributes;
using System.Collections.Generic;
using Signawel.Domain.DataResults;
using System.IO;
using System.Drawing.Imaging;
using System;
using Signawel.Domain.Enums;
using Signawel.Domain;

namespace Signawel.API.Controllers
{
    /// <summary>
    ///     Controller for doing CRUD actions with RoadworkSchemas
    /// </summary>
    [ApiController]
    [JwtTokenAuthorize]
    [Route("api/roadwork-schemas")]
    public class RoadworkSchemaController : BaseController
    {
        private readonly IRoadworkSchemaService _schemaService;
        private readonly IImageService _imageService;

        public RoadworkSchemaController(IRoadworkSchemaService schemaService, IImageService imageService)
        {
            _schemaService = schemaService;
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation("getRoadworkSchema")]
        [SwaggerResponse(StatusCodes.Status200OK, "Roadwork schema", typeof(RoadworkSchemaResponseDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Roadwork schema was not found")]
        public async Task<IActionResult> GetRoadworkSchema(string id)
        {
            var result = await _schemaService.GetRoadworkSchema(id);

            if(!result.Succeeded)
                return NotFound(result);

            return Ok(result.Entity);
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation("getAllRoadworkSchemas")]
        [SwaggerResponse(StatusCodes.Status200OK, "All roadwork schema", typeof(RoadworkSchemaPaginationResponseDto))]
        public async Task<IActionResult> GetAllRoadworkSchemas([FromQuery] string search = null, [FromQuery] int page = 0, [FromQuery] int limit = 20, [FromQuery] RoadworkCategory roadworkCategory = RoadworkCategory.NoCategory)
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

            var schemaResult = schemas.Skip(page * (limit <= 0 ? 0 : limit));

            if(limit > 0)
                schemaResult = schemaResult.Take(limit);

            if(!string.IsNullOrEmpty(search))
                schemaResult = schemaResult.Where(x => x.Name.Contains(search));

            if (roadworkCategory != RoadworkCategory.NoCategory)
                schemaResult = schemaResult.Where(x => x.RoadworkCategory == roadworkCategory);

            result.Schemas = await schemaResult.ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Role.Constants.Admin)]
        [SwaggerOperation("createRoadworkSchema")]
        [SwaggerResponse(StatusCodes.Status200OK, "The created roadwork schema", typeof(RoadworkSchemaResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something whent wrong", typeof(IList<ErrorResponseDto>))]
        public async Task<IActionResult> AddRoadworkSchema([FromJson] RoadworkSchemaCreationRequestDto value, IList<IFormFile> files)
        {
            var image = files.FirstOrDefault();

            if(image == null || image.Length <= 0)
            {
                return BadRequest(DataResult.WithError("ImageRequired", "An image is required and cannot have size of 0 for creating a roadwork schema.", DataErrorVisibility.Public));
            }

            var imageId = string.Empty;

            using(var copyMemoryStream = new MemoryStream())
            using(var changeFormatMemoryStream = new MemoryStream())
            {
                await image.CopyToAsync(copyMemoryStream);
                try
                {
                    var bitmap = System.Drawing.Image.FromStream(copyMemoryStream);

                    bitmap.Save(changeFormatMemoryStream, ImageFormat.Png);
                    bitmap.Dispose();

                    var result = await _imageService.AddImage(changeFormatMemoryStream);

                    if(result.Succeeded)
                    {
                        imageId = result.Entity;
                    } else
                    {
                        return BadRequest(result);
                    }
                } catch (Exception)
                {
                    return BadRequest(DataResult.WithError("InvalidFileFormat", "The uploaded file is not a valid image format.", DataErrorVisibility.Public));
                }
            }

            var creationResult = await _schemaService.CreateRoadworkSchema(value, imageId);

            if(!creationResult.Succeeded)
            {
                await _imageService.DeleteImage(imageId);
                return BadRequest(creationResult);
            }

            return Ok(creationResult.Entity);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.Constants.Admin)]
        [SwaggerOperation("putUpdateRoadworkSchema")]
        [SwaggerResponse(StatusCodes.Status200OK, "The updated roadwork schema", typeof(RoadworkSchemaResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something whent wrong")]
        public async Task<IActionResult> PutRoadworkSchema(string id, [FromJson] RoadworkSchemaPutRequestDto value, IList<IFormFile> files)
        {
            var image = files.FirstOrDefault();

            if(image != null)
            {
                if(image.Length <= 0)
                {
                    return BadRequest(DataResult.WithError("FileInvalid", "An image cannot have size lower of equal to 0.", DataErrorVisibility.Public));
                }

                using(var copyMemoryStream = new MemoryStream())
                using(var changeFormatMemoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(copyMemoryStream);
                    try
                    {
                        var bitmap = System.Drawing.Image.FromStream(copyMemoryStream);

                        bitmap.Save(changeFormatMemoryStream, ImageFormat.Png);
                        bitmap.Dispose();

                        var result = await _imageService.UpdateImage(value.ImageId, changeFormatMemoryStream);

                        if(!result.Succeeded)
                        {
                            return BadRequest(result);
                        }
                    } catch(Exception)
                    {
                        return BadRequest(DataResult.WithError("InvalidFileFormat", "The uploaded file is not a valid image format.", DataErrorVisibility.Public));
                    }
                }
            }

            var updated = await _schemaService.PutRoadworkSchema(id, value);

            if(!updated.Succeeded)
                return BadRequest(updated);

            return Ok(updated.Entity);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Constants.Admin)]
        [SwaggerOperation("deleteRoadworkSchema")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Deleted roadwork schema successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something whent wrong")]
        public async Task<IActionResult> DeleteRoadworkSchema(string id)
        {
            var result = await _schemaService.DeleteRoadworkSchema(id);

            if (!result.Succeeded)
                return BadRequest(result);

            return NoContent();
        }

    }
}
