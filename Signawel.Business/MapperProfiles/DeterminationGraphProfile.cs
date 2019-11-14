using AutoMapper;
using Signawel.Domain;
using Signawel.Dto.Determination;
using System;
using System.Collections.Generic;
using System.Text;
using Signawel.Domain.Determination;

namespace Signawel.Business.MapperProfiles
{
    public class DeterminationGraphProfile : Profile
    {
        public DeterminationGraphProfile()
        {
            CreateMap<DeterminationGraph, DeterminationGraphResponseDto>();
            CreateMap<DeterminationGraphCreationRequestDto, DeterminationGraph>();
        }

    }
}
