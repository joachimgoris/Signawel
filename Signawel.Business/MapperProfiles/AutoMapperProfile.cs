using AutoMapper;

namespace Signawel.Business.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            new UserProfile();
        }
    }
}
