using AutoMapper;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Signawel.Business.Services
{
    public class ImageService : IImageService
    {
        private readonly SignawelDbContext _context;
        private readonly IMapper _mapper;

        public ImageService(SignawelDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<MemoryStream> GetImageAsync(string id)
        {
            var image = await _context.Images.FindAsync(id);

            if(image == null)
            {
                return null;
            }

            // TODO get image from file server
            var bytes = Convert.FromBase64String(image.ImagePath);
            return new MemoryStream(bytes);
        }

        public async Task<ImageResponseDto> AddImage(MemoryStream memoryStream)
        {
            byte[] bytes = memoryStream.ToArray();

            // TODO save to file server and save path
            string base64 = Convert.ToBase64String(bytes);

            var image = new Image
            {
                ImagePath = base64
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return _mapper.Map<ImageResponseDto>(image);
        }

        public async Task DeleteImage(string id)
        {
            var image = await _context.Images.FindAsync(id);

            if(image != null)
                _context.Images.Remove(image);

            await _context.SaveChangesAsync();
        }

    }
}
