using Signawel.Dto;
using System.Collections.Generic;

namespace Signawel.Business.Abstractions.Services
{
    public interface ICategoryService
    {
        IList<CategoryResponseDto> GetAllCategories();
    }
}
