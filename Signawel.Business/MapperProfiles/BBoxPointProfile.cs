using AutoMapper;
using Signawel.Domain;
using Signawel.Dto;
using System;
using System.Collections.Generic;
using System.Text;
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
