using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Signawel.API.Controllers
{
    /// <summary>
    ///     Controller for getting images from the API.
    /// </summary>
    [ApiController]
    [JwtTokenAuthorize]
    [Route("api/images")]
    public class ImageController : BaseController
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation("getImage")]
        [SwaggerResponse(StatusCodes.Status200OK, "Image")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Image not found")]
        public async Task<ActionResult> GetImage(string id)
        {
            var stream = await _imageService.GetImageAsync(id);

            if(stream == null)
                return NotFound();

            return File(stream, "image/png", $"{id}.png");
        }

    }
}
