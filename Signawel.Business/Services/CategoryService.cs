using AutoMapper;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Signawel.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SignawelDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(SignawelDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        #region GetAllCategories

        /// <inheritdoc cref="ICategoryService.GetCategories()"/>
        public IList<CategoryResponseDto> GetAllCategories()
        {
            return _mapper.Map<IList<CategoryResponseDto>>(_context.Categories.OrderBy(category => category.OrderId));
        }

        #endregion
    }
}
