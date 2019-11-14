using AutoMapper;
using Signawel.Domain;
using Signawel.Dto.Determination;
using System;
using System.Collections.Generic;
using System.Text;
using Signawel.Domain.Determination;

namespace Signawel.Business.MapperProfiles
{
    public class DeterminationAnswerProfile : Profile
    {

        public DeterminationAnswerProfile()
        {
            CreateMap<DeterminationAnswer, DeterminationAnswerResponseDto>();
            CreateMap<DeterminationAnswerCreationRequestDto, DeterminationAnswer>();
        }

    }
}
