using AutoMapper;
using Signawel.Dto.Determination;
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
