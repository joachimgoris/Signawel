using AutoMapper;
using Microsoft.Extensions.Logging;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.Reports;
using System.Linq;
using System.Threading.Tasks;
using Signawel.Domain.Reports;
using System.Collections.Generic;
using Signawel.Domain;
using Microsoft.EntityFrameworkCore;

namespace Signawel.Business.Services
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IReportGroupService _reportGroupService;
        private readonly SignawelDbContext _context;
        private readonly ILogger<ReportService> _logger;

        public ReportService(SignawelDbContext context, ILogger<ReportService> logger, IMapper mapper, IReportGroupService reportGroupService)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _reportGroupService = reportGroupService;
        }

        #region AddReport

        /// <inheritdoc cref="IReportService.AddReportAsync(ReportCreationRequestDto)"/>
        public async Task<DataResult<ReportResponseDto>> AddReportAsync(ReportCreationRequestDto reportDto)
        {
            if (reportDto == null)
                return DataResult<ReportResponseDto>.WithError(ErrorCodes.ParameterEmptyError, "The given Dto is empty.", DataErrorVisibility.Public);

            var report = _mapper.Map<Report>(reportDto);

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            var reportResponse = _mapper.Map<ReportResponseDto>(report);
            return DataResult<ReportResponseDto>.WithEntityOrError(
                reportResponse, ErrorCodes.ReportCreationError, "Something went wrong when creating a report.");
        }

        /// <inheritdoc cref="IReportService.LinkImagesToReportAsync(string, string)"/>
        public async Task LinkImagesToReportAsync(string reportId, string imageId)
        {
            await _context.ReportImages.AddAsync(new ReportImage
            {
                ReportId = reportId,
                ImageId = imageId
            });

            await _context.SaveChangesAsync();
        }

        #endregion

        #region DeleteReport

        /// <inheritdoc cref="IReportService.DeleteReportAsync(string)"/>
        public async Task<DataResult> DeleteReportAsync(string reportId)
        {

            if (string.IsNullOrEmpty(reportId))
                return DataResult.WithError(ErrorCodes.ReportDeletionError, "The id of the report is empty.");

            var report = await _context.Reports.FindAsync(reportId);

            if (report == null)
                return DataResult.WithError(ErrorCodes.ReportDeletionError, "There was no report linked to the given id in the database.");

            _context.Remove(report);
            _context.Images.RemoveRange(_context.ReportImages.Where(reportImage => reportImage.ReportId == reportId).Select(e => e.Image).ToList());
            _context.ReportImages.RemoveRange(_context.ReportImages.Where(reportImage => reportImage.ReportId == reportId));

            await _context.SaveChangesAsync();

            return DataResult.Success;
        }

        #endregion

        #region GetAllReports

        /// <inheritdoc cref="IReportService.GetAllReports(string, int, int, string, IList{string})"/>
        public async Task<ReportGetPaginationResponseDto> GetAllReports(string search, int page, int limit, string username, IList<string> userRoles)
        {
            var reportsQuery = _context.Reports.AsQueryable();

            if(!userRoles.Contains(Role.Constants.Admin))
            {
                var reportGroups = await _reportGroupService.GetReportGroupsAsync("null", "null", username);
                
                if(!reportGroups.Succeeded)
                {
                    return new ReportGetPaginationResponseDto();
                }

                var cities = reportGroups.Entity.SelectMany(e => e.CityReportGroups).Select(crd => crd.Name).ToList();
                reportsQuery = reportsQuery.Where(r => r.Cities.Split(',').Any(c => cities.Contains(c)));
            }

            var result = new ReportGetPaginationResponseDto()
            {
                Total = reportsQuery.Count()
            };

            if (page < 0)
            {
                page = 0;
            }

            var reportResult = reportsQuery.Skip(page * (limit <= 0 ? 0 : limit));

            if (limit > 0)
                reportResult = reportResult.Take(limit);

            if (!string.IsNullOrEmpty(search))
                reportResult = reportResult.Where(x => x.SenderEmail.Contains(search) ||
                                                       x.Cities.Contains(search) || 
                                                       x.CreationTime.ToString().Contains(search));

            var reports = await reportResult.Include(r => r.Images).ToListAsync();

            result.Reports = _mapper.Map<IList<ReportResponseDto>>(reports);

            return result;
        }

        #endregion

        #region GetReport

        /// <inheritdoc cref="IReportService.GetReportAsync(string)"/>
        public async Task<DataResult<ReportResponseDto>> GetReportAsync(string reportId)
        {
            if (!string.IsNullOrEmpty(reportId))
                return DataResult<ReportResponseDto>.WithError(ErrorCodes.ReportGetError, "The given reportId is empty.");

            var report = await _context.Reports.FindAsync(reportId);

            return DataResult<ReportResponseDto>.WithEntityOrError(_mapper.Map<ReportResponseDto>(report), ErrorCodes.NotFoundError, "There was no report linked to the given id in the database.");
        }

        #endregion

        #region ModifyReport

        /// <inheritdoc cref="IReportService.ModifyReportAsync(ReportModifyRequestDto)"/>
        public async Task<DataResult<ReportResponseDto>> ModifyReportAsync(ReportModifyRequestDto reportDto)
        {
            if (reportDto == null)
                return DataResult<ReportResponseDto>.WithError(ErrorCodes.ReportModificationError, "The given Dto is empty.");

            Report oldReport = await _context.Reports.FindAsync(reportDto.Id);
            oldReport = _mapper.Map(reportDto, oldReport);

            await _context.SaveChangesAsync();

            return DataResult<ReportResponseDto>.WithEntityOrError(_mapper.Map<ReportResponseDto>(oldReport), ErrorCodes.ReportModificationError, "Something went wrong when updating the report.");
        }

        #endregion
    }
}
