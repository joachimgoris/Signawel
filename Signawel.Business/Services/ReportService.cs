﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.Reports;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<DataResult<ReportCreationResponseDto>> AddReportAsync(ReportCreationRequestDto reportDto)
        {
            if (reportDto == null)
                return DataResult<ReportCreationResponseDto>.WithError(ErrorCodes.ReportCreationError, "The given Dto is empty.");

            var report = _mapper.Map<Report>(reportDto);

            await _context.Reports.AddAsync(report);

            await _context.SaveChangesAsync();

            var reportResponse = _mapper.Map<ReportCreationResponseDto>(report);

            return DataResult<ReportCreationResponseDto>.WithEntityOrError(
                reportResponse, ErrorCodes.ReportCreationError, "Something went wrong when creating a report.");
        }

        #endregion

        #region DeleteReport

        /// <inheritdoc cref="IReportService.DeleteReportAsync(string)"/>
        public async Task<DataResult> DeleteReportAsync(string reportId)
        {

            if (!string.IsNullOrEmpty(reportId))
                return DataResult.WithError(ErrorCodes.ReportDeletionError, "The id of the report is empty.");

            var report = await _context.Reports.FindAsync(reportId);

            if (report != null)
                return DataResult.WithError(ErrorCodes.ReportDeletionError, "There was no report linked to the given id in the database.");

            _context.Remove(report);

            await _context.SaveChangesAsync();

            return DataResult.Success;
        }

        #endregion

        #region GetAllReports

        /// <inheritdoc cref="IReportService.GetAllReports"/>
        public IQueryable<Report> GetAllReports()
        {
            return _context.Reports;
        }

        #endregion

        #region GetReport

        /// <inheritdoc cref="IReportService.GetReportAsync(string)"/>
        public async Task<DataResult<ReportGetResponseDto>> GetReportAsync(string reportId)
        {
            if (!string.IsNullOrEmpty(reportId))
                return DataResult<ReportGetResponseDto>.WithError(ErrorCodes.ReportGetError, "The given reportId is empty.");

            var report = await _context.Reports.FindAsync(reportId);

            return DataResult<ReportGetResponseDto>.WithEntityOrError(_mapper.Map<ReportGetResponseDto>(report), ErrorCodes.NotFoundError, "There was no report linked to the given id in the database.");
        }

        #endregion

        #region ModifyReport

        /// <inheritdoc cref="IReportService.ModifyReportAsync(ReportModifyRequestDto)"/>
        public async Task<DataResult<ReportModifyResponseDto>> ModifyReportAsync(ReportModifyRequestDto reportDto)
        {
            if (reportDto == null)
                return DataResult<ReportModifyResponseDto>.WithError(ErrorCodes.ReportModificationError, "The given Dto is empty.");

            Report oldReport = await _context.Reports.FindAsync(reportDto.Id);
            oldReport = _mapper.Map(reportDto, oldReport);

            await _context.SaveChangesAsync();

            return DataResult<ReportModifyResponseDto>.WithEntityOrError(_mapper.Map<ReportModifyResponseDto>(oldReport), ErrorCodes.ReportModificationError, "Something went wrong when updating the report.");
        }

        #endregion
    }
}
