using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto;
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
        private readonly IImageService _imageService;
        private readonly IMailService _mailService;
        private readonly IReportGroupService _reportGroupService;
        private readonly UserManager<User> _userManager;

        public ReportController(IReportService reportService,
            IImageService imageService,
            IMailService mailService,
            IReportGroupService reportGroupService,
            UserManager<User> userManager)
        {
            _reportService = reportService;
            _imageService = imageService;
            _mailService = mailService;
            _reportGroupService = reportGroupService;
            _userManager = userManager;
        }

        #region GetReports

        [HttpGet]
        [JwtTokenAuthorize(Roles = Role.Constants.Admin + "," + Role.Constants.Instance)]
        [SwaggerOperation("getReports")]
        [SwaggerResponse(StatusCodes.Status200OK, "Reports overview", typeof(DataResult<ReportResponseDto>))]
        public async Task<IActionResult> GetReportsAsync([FromQuery] string search = null, [FromQuery] int page = 0, [FromQuery] int limit = 20)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByNameAsync(userId);
            

            var allReports = _reportService.GetAllReports();
            var reports = new List<ReportResponseDto>();

            if (await _userManager.IsInRoleAsync(user, Role.Constants.Instance))
            { 
                var reportGroups = await _reportGroupService.GetReportGroupsAsync("null", "null",user.UserName);
                var cities = new List<string>();
                foreach(var report in reportGroups.Entity)
                {
                    foreach(var city in report.CityReportGroups.Select(crg => crg.Name))
                    {
                        cities.Add(city);
                    }
                }

                foreach(var report in allReports)
                {
                    var duplicates = report.Cities.Split(',').Intersect(cities);
                    if (duplicates.Count() > 0)
                    {
                        reports.Add(report);
                    }
                }
                
            }

            if (await _userManager.IsInRoleAsync(user, Role.Constants.Admin))
            {
                reports = allReports.ToList();
            }




                var result = new ReportGetPaginationResponseDto()
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
        [JwtTokenAuthorize(Roles = Role.Constants.Instance)]
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

        #region DeleteReport

        [HttpDelete("{id}")]
        [JwtTokenAuthorize(Roles = Role.Constants.Admin)]
        [SwaggerOperation("deleteReport")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Deleted report successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something went wrong")]
        public async Task<IActionResult> DeleteReport(string id)
        {
            var result = await _reportService.DeleteReportAsync(id);

            if (!result.Succeeded)
                return BadRequest(result);

            return NoContent();
        }

        #endregion

        #region AddReport

        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation("uploadReport")]
        [SwaggerResponse(StatusCodes.Status200OK, "Report created.", typeof(DataResult<ReportResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Something went wrong.", typeof(IList<ErrorResponseDto>))]
        public async Task<IActionResult> AddReport([FromJson] ReportCreationRequestDto value, IList<IFormFile> files)
        {
            var addReportResult = await _reportService.AddReportAsync(value);
            
            if (addReportResult.Succeeded)
            {
                var imageUploadResult = new DataResult();
                foreach (var formFile in files)
                {
                    using (var copyMemoryStream = new MemoryStream())
                    using (var changeFormatMemoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(copyMemoryStream);
                        try
                        {
                            var bitmap = System.Drawing.Image.FromStream(copyMemoryStream);

                            bitmap.Save(changeFormatMemoryStream, ImageFormat.Png);
                            bitmap.Dispose();

                            var result = await _imageService.AddImage(changeFormatMemoryStream);

                            if (result.Succeeded)
                            {
                                await _reportService.LinkImagesToReportAsync(addReportResult.Entity.Id, result.Entity);
                            }
                            else
                            {
                                imageUploadResult.AddError(ErrorCodes.FailedToSaveImage, "Image could not be saved to database");
                            }
                        }
                        catch (Exception)
                        {
                            imageUploadResult.AddError(ErrorCodes.InvalidFileFormat, "Uploaded file was not of valid format");
                        }
                    }
                }

                var errors = imageUploadResult.Errors.Select(e => new ErrorResponseDto
                {
                    Code = e.Code,
                    Value = e.Value
                }).ToList();

                await _mailService.CreateReportEmailAsync(value);
                return Ok(errors);
            }

            return BadRequest(addReportResult.Errors.Select(error => new ErrorResponseDto
            {
                Code = error.Code,
                Value = error.Value
            }).ToList());
        }

        #endregion
    }
}