using AutoMapper;
using Signawel.Domain;
using Signawel.Dto;
using System;
using System.Collections.Generic;
using System.Text;

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
