using AutoMapper;
using Signawel.Domain;
using Signawel.Dto;
using System;
using System.Collections.Generic;
using System.Text;

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
