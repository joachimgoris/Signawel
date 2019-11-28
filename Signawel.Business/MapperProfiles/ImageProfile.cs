using AutoMapper;
using Signawel.Domain;
using Signawel.Dto;

namespace Signawel.Business.MapperProfiles
{
    public class ImageProfile : Profile
    {

        public ImageProfile()
        {
            CreateMap<Image, ImageResponseDto>();
        }

    }
}
