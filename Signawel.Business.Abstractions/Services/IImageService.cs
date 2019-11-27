using Signawel.Domain.DataResults;
using System.IO;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IImageService
    {
        /// <summary>
        ///     Get an image from the database.
        /// </summary>
        /// <param name="id">
        ///     Id of the image to get from the database.
        /// </param>
        /// <returns>
        ///     <see cref="MemoryStream"/> of the image contents, or null if no image was found.
        /// </returns>
        Task<MemoryStream> GetImageAsync(string id);

        /// <summary>
        ///     Add an image to the database.
        /// </summary>
        /// <param name="memoryStream">
        ///     Memory steam of the image
        /// </param>
        /// <returns>
        ///     <see cref="DataResult{TEntity}"/> with id of the added image or errors.
        /// </returns>
        Task<DataResult<string>> AddImage(MemoryStream memoryStream);

        /// <summary>
        ///     Delete an image from the database.
        /// </summary>
        /// <param name="id">
        ///     Id of the image to delete.
        /// </param>
        Task<DataResult<string>> UpdateImage(string id, MemoryStream stream);

        /// <summary>
        ///     Change an image, but keep the id
        /// </summary>
        /// <param name="id">
        ///     Id of the image to change the content of.
        /// </param>
        /// <param name="stream">
        ///     <see cref="MemoryStream"/> of the new image to replace the old image with.
        /// </param>
        /// <returns>
        ///     <see cref="DataResult{TEntity}"/> containg the id of the image or errors.
        /// </returns>
        Task DeleteImage(string id);

    }
}
