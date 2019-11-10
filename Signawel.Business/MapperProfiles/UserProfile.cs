using AutoMapper;
using Signawel.Domain;
using Signawel.Domain.Authentication.Models;

namespace Signawel.Business.MapperProfiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetUserDto>();
        }
    }
}
