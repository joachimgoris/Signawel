using AutoMapper;
using Signawel.Domain;
using Signawel.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Business.MapperProfiles
{
    public class BBoxPointProfile : Profile
    {

        public BBoxPointProfile()
        {
            CreateMap<BBoxPoint, BBoxPointResponseDto>();
            CreateMap<BBoxPointCreationRequestDto, BBoxPoint>();
        }

    }
}
