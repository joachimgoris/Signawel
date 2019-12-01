using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.Reports;
using Swashbuckle.AspNetCore.Annotations;

namespace Signawel.API.Controllers
{
    [ApiController]
    [JwtTokenAuthorize]
    [Route("api/reports")]
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;
        private readonly IMailService _mailService;

        public ReportController(IReportService reportService,
            IMailService mailService)
        {
            _reportService = reportService;
            _mailService = mailService;
        }

        #region GetReports

        [HttpGet]
        [SwaggerOperation("getReports")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retrieved all reports.", typeof(DataResult<ReportResponseDto>))]
        public IActionResult GetReports([FromQuery] string search = null, [FromQuery] int page = 0, [FromQuery] int limit = 20)
        {
            var reports = _reportService.GetAllReports();

            var result = new ReportGetPaginationDto()
            {
                Total = reports.Count()
            };

            if (page < 0)
            {
                page = 0;
            }

            var reportResult = reports.Skip(page * (limit <= 0 ? 0 : limit));

            if (limit > 0)
                reportResult = reportResult.Take(limit);

            if (!string.IsNullOrEmpty(search))
                reportResult = reportResult.Where(x => x.SenderEmail.Contains(search) ||
                                                       x.Description.Contains(search) ||
                                                       x.CreationTime.ToString().Contains(search));

            result.Reports = reportResult.ToList();

            return Ok(result);
        }

        #endregion

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