﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.API.Controllers
{
    [ApiController]
    // TODO [JwtTokenAuthorize]
    [Route("api/images")]
    public class ImageController : ControllerBase
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

        [HttpPost]
        [SwaggerOperation("uploadImage")]
        [SwaggerResponse(StatusCodes.Status200OK, "Image", typeof(ImageResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something went wrong.")]
        public async Task<ActionResult> UploadImage()
        {
            var formFile = Request.Form.Files.FirstOrDefault();

            if(formFile == null)
            {
                ModelState.AddModelError("Image", "No image");
                return BadRequest(ModelState);
            }

            if(formFile.Length <= 0)
            {
                ModelState.AddModelError("Image", "No content");
                return BadRequest(ModelState);
            }

            var memorySteam = new MemoryStream();
            await formFile.CopyToAsync(memorySteam);

            var bitmap = Image.FromStream(memorySteam);

            // convert to png
            using(var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                var result = await _imageService.AddImage(ms);
                return Ok(result);
            }
        }

    }
}
