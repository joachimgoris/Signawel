using Microsoft.AspNetCore.Mvc;
using Signawel.Domain.DataResults;
using Signawel.Dto;
using System.Linq;

namespace Signawel.API.Controllers
{
    /// <summary>
    ///     Custom controller implementation for improvind error data results
    /// </summary>
    public abstract class BaseController : ControllerBase
    {

        public BadRequestObjectResult BadRequest(DataResult dataResult)
        {
            var errorList = dataResult.Errors.Where(error => error.Visibility == DataErrorVisibility.Public).Select(error => new ErrorResponseDto
            {
                Code = error.Code,
                Value = error.Value
            }).ToList();

            return BadRequest(errorList);
        }

        public NotFoundObjectResult NotFound(DataResult dataResult)
        {
             var errorList = dataResult.Errors.Where(error => error.Visibility == DataErrorVisibility.Public).Select(error => new ErrorResponseDto
            {
                Code = error.Code,
                Value = error.Value
            }).ToList();

            return NotFound(errorList);
        }
    }
}
