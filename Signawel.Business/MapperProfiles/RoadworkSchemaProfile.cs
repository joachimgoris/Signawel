using AutoMapper;
using Signawel.Domain;
using Signawel.Dto;
using Signawel.Dto.RoadworkSchema;

namespace Signawel.Business.MapperProfiles
{
    public class RoadworkSchemaProfile : Profile
    {

        public RoadworkSchemaProfile()
        {
            CreateMap<RoadworkSchema, RoadworkSchemaResponseDto>();
            CreateMap<RoadworkSchemaCreationRequestDto, RoadworkSchema>();
            CreateMap<RoadworkSchemaPutRequestDto, RoadworkSchema>();
        }

    }
}
