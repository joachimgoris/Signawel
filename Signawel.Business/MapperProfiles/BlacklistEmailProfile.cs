using AutoMapper;
using Signawel.Domain;
using Signawel.Dto.BlacklistEmail;

namespace Signawel.Business.MapperProfiles
{
    public class BlacklistEmailProfile : Profile
    {
        public BlacklistEmailProfile()
        {
            CreateMap<BlacklistEmailCreationRequestDto, BlacklistEmail>();
            CreateMap<BlacklistEmail, BlacklistEmailResponseDto>();
        }
    }
}
