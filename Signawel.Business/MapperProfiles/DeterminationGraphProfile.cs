using AutoMapper;
using Signawel.Dto.Determination;
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
