using AutoMapper;
using Signawel.Dto.Determination;
using Signawel.Domain.Determination;

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
