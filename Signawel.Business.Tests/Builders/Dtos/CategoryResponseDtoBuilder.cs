using Signawel.Domain;
using Signawel.Dto;

namespace Signawel.Business.Tests.Builders.Dtos
{
    public class CategoryResponseDtoBuilder
    {
        private readonly CategoryResponseDto _categoryResponseDto;

        public CategoryResponseDtoBuilder()
        {
            _categoryResponseDto = new CategoryResponseDto();
        }

        public CategoryResponseDtoBuilder FromCategory(Category category)
        {
            _categoryResponseDto.Id = category.Id;
            _categoryResponseDto.Name = category.Name;
            _categoryResponseDto.ImagePath = category.ImagePath;
            _categoryResponseDto.OrderId = category.OrderId;
            return this;
        }

        public CategoryResponseDto Build()
        {
            return _categoryResponseDto;
        }
    }
}
