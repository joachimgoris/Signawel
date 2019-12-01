using Signawel.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signawel.Mobile.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync();
    }
}
