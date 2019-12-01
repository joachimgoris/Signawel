using AutoMapper;
using Signawel.Domain;
using Signawel.Dto;

namespace Signawel.Business.MapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponseDto>();
        }
    }
}
