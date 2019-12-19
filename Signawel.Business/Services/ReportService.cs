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

namespace Signawel.Business.Services
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly SignawelDbContext _context;
        private readonly ILogger<ReportService> _logger;

        public ReportService(SignawelDbContext context, ILogger<ReportService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
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

        /// <inheritdoc cref="IReportService.GetAllReports"/>
        public IQueryable<ReportResponseDto> GetAllReports()
        {
            return _mapper.ProjectTo<ReportResponseDto>(_context.Reports);
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
