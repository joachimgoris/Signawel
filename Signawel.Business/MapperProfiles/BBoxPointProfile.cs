using AutoMapper;
using Signawel.Dto;
using Signawel.Domain.BBox;
using Signawel.Dto.BBox;

namespace Signawel.Business.MapperProfiles
{
    public class BBoxPointProfile : Profile
    {

        public BBoxPointProfile()
        {
            CreateMap<BBoxPoint, BBoxPointResponseDto>();
            CreateMap<BBoxPointCreationRequestDto, BBoxPoint>();
            CreateMap<BBoxPointPutRequestDto, BBoxPoint>();
        }

    }
}
