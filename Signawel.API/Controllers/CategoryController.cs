using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.DataResults;
using Signawel.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace Signawel.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        [SwaggerOperation("getCategories")]
        [SwaggerResponse(StatusCodes.Status200OK, "Categories successfully retrieved.", typeof(DataResult<CategoryResponseDto>))]
        public IActionResult GetCategories()
        {
            return Ok(_categoryService.GetAllCategories());
        }
    }
}
