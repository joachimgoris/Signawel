using AutoMapper;
using Signawel.Dto;
using Signawel.Domain.BBox;
using Signawel.Dto.BBox;

namespace Signawel.Business.MapperProfiles
{
    public class BBoxProfile : Profile
    {

        public BBoxProfile()
        {
            CreateMap<BBox, BBoxResponseDto>();
            CreateMap<BBoxCreationRequestDto, BBox>();
            CreateMap<BBoxPutRequestDto, BBox>();
        }

    }
}
