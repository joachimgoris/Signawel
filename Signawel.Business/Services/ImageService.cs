using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain;
using System;
using System.IO;
using System.Threading.Tasks;
using Signawel.Domain.DataResults;

namespace Signawel.Business.Services
{
    /// <summary>
    ///     Service for managing the images in the application.
    /// </summary>
    public class ImageService : IImageService
    {
        private readonly SignawelDbContext _context;

        /// <summary>
        ///     Default contructor
        /// </summary>
        /// <param name="context">
        ///     Instance of <see cref="SignawelDbContext"/> provided by DI.
        /// </param>
        public ImageService(SignawelDbContext context)
        {
            this._context = context;
        }

        /// <inheritdoc/>
        public async Task<MemoryStream> GetImageAsync(string id)
        {
            var image = await _context.Images.FindAsync(id);

            if(image == null)
            {
                return null;
            }

            var bytes = Convert.FromBase64String(image.ImagePath);
            return new MemoryStream(bytes);
        }

        /// <inheritdoc/>
        public async Task<DataResult<string>> AddImage(MemoryStream memoryStream)
        {
            byte[] bytes = memoryStream.ToArray(); 

            string base64 = Convert.ToBase64String(bytes);

            var image = new Image
            {
                ImagePath = base64
            };

            try
            {
                _context.Images.Add(image);
                await _context.SaveChangesAsync();

                return DataResult<string>.Success(image.Id);
            } catch(Exception)
            {
                return DataResult<string>.WithError("FailedToSaveImage", "Failed to add image to database", DataErrorVisibility.Public);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteImage(string id)
        {
            var image = await _context.Images.FindAsync(id);

            if(image != null)
                _context.Images.Remove(image);

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<DataResult<string>> UpdateImage(string id, MemoryStream stream)
        {
            var image = await _context.Images.FindAsync(id);

            if(image == null)
            {
                return DataResult<string>.WithError("NoImageFound", $"No image found with id '{ id }'", DataErrorVisibility.Public);
            }

            byte[] bytes = stream.ToArray();
            string base64 = Convert.ToBase64String(bytes);

            image.ImagePath = base64;

            try
            {
                await _context.SaveChangesAsync();

                return DataResult<string>.Success(image.Id);
            } catch(Exception)
            {
                return DataResult<string>.WithError("FailedToSaveImage", "Faild to add image to database");
            }
        }
    }
}
