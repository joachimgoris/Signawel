using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.Reports;
using Swashbuckle.AspNetCore.Annotations;

namespace Signawel.API.Controllers
{
    [ApiController]
    // TODO [JwtTokenAuthorize]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMailService _mailService;

        public ReportController(IReportService reportService,
            IMailService mailService)
        {
            _reportService = reportService;
            _mailService = mailService;
        }

        #region GetReport

        [HttpGet("{id}")]
        [SwaggerOperation("getReport")]
        [SwaggerResponse(StatusCodes.Status200OK, "Report found.", typeof(DataResult<ReportResponseDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Report not found.", typeof(IList<DataError>))]
        public async Task<IActionResult> GetReport(string id)
        {
            var result = await _reportService.GetReportAsync(id);

            if (result.HasError(ErrorCodes.NotFoundError))
                return NotFound();

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result.Entity);
        }

        #endregion

        #region AddReport

        [HttpPost]
        [SwaggerOperation("uploadReport")]
        [SwaggerResponse(StatusCodes.Status200OK, "Report created.", typeof(DataResult<ReportResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something went wrong.", typeof(IList<DataError>))]
        public async Task<ActionResult> AddReport([FromBody] ReportCreationRequestDto model)
        {
            var result = await _reportService.AddReportAsync(model);

            if (!result.Succeeded)
                return BadRequest(result);

            await _mailService.CreateReportEmailAsync(result.Entity);

            return Ok(result);
        }

        #endregion
    }
}