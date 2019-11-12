using AutoMapper;
using Signawel.Domain;
using Signawel.Dto.Determination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Business.MapperProfiles
{
    public class DeterminationNodeProfile : Profile
    {
        public DeterminationNodeProfile()
        {
            CreateMap<DeterminationNode, DeterminationNodeResponseDto>();
            CreateMap<DeterminationNodeCreatingRequestDto, DeterminationNode>();
        }
    }
}
