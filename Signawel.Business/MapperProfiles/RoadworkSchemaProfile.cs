using AutoMapper;
using Signawel.Domain;
using Signawel.Dto;
using System;
using System.Collections.Generic;
using System.Text;

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
