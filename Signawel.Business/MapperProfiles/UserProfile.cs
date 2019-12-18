using AutoMapper;
using Signawel.Domain;
using Signawel.Dto.Authentication;

namespace Signawel.Business.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateRequestDto, User>();
            CreateMap<UserModifyRequestDto, User>();
            CreateMap<User, UserResponseDto>();
        }
    }
}
