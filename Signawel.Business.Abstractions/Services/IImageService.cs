using Signawel.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IImageService
    {

        Task<MemoryStream> GetImageAsync(string id);

        Task<ImageResponseDto> AddImage(MemoryStream fileStream);

        Task DeleteImage(string id);

    }
}
