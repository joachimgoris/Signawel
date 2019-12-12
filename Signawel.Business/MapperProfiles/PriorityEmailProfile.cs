using AutoMapper;
using Signawel.Domain;
using Signawel.Dto.PriorityEmail;

namespace Signawel.Business.MapperProfiles
{
    public class PriorityEmailProfile : Profile
    {

        public PriorityEmailProfile()
        {
            CreateMap<PriorityEmailCreationRequestDto, PriorityEmail>();
            CreateMap<PriorityEmail, PriorityEmailResponseDto>();
        }

    }
}
