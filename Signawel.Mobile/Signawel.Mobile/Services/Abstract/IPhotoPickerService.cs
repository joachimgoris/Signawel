using System.IO;
using System.Threading.Tasks;

namespace Signawel.Mobile.Services.Abstract
{
    public interface IPhotoPickerService
    {
        /// <summary>
        ///     Gets an image converted to a stream. Used to get images from local storage.
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="Task{TResult}"/> containing an instance of <see cref="Stream"/>.
        /// </returns>
        Task<Stream> GetImageStreamAsync();
    }
}
