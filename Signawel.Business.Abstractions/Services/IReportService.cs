﻿using Signawel.Domain.DataResults;
using Signawel.Dto.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IReportService
    {
        #region Add

        /// <summary>
        ///     Adds a report to the database.
        /// </summary>
        /// <param name="reportDto">
        ///     An instance of <see cref="ReportCreationRequestDto"/> containing the data for a new <see cref="Report"/>.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="ReportResponseDto"/> containing the data of the <see cref="Report"/> as it is in the database.
        /// </returns>
        Task<DataResult<ReportResponseDto>> AddReportAsync(ReportCreationRequestDto reportDto);

        #endregion

        #region Get

        /// <summary>
        ///     Get a single <see cref="Report"/> from the database.
        /// </summary>
        /// <param name="reportId">
        ///     The id of the <see cref="Report"/>.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="ReportResponseDto"/> containing all the data of the <see cref="Report"/> as it is in the database.
        /// </returns>
        Task<DataResult<ReportResponseDto>> GetReportAsync(string reportId);

        /// <summary>
        ///     Get all the reports currently in the database.
        /// </summary>
        /// <returns>
        ///     A list of all the reports in the database.
        /// </returns>
        Task<ReportGetPaginationResponseDto> GetAllReports(string search, int page, int limit, string username, IList<string> userRoles);

        #endregion

        #region Delete

        /// <summary>
        ///     Delete the <see cref="Report"/> from the database.
        /// </summary>
        /// <param name="reportId">
        ///     The id of the <see cref="Report"/> to be deleted.
        /// </param>
        Task<DataResult> DeleteReportAsync(string reportId);

        #endregion

        #region Modify

        /// <summary>
        ///     Modifies the report in the database with the changes in the new report.
        /// </summary>
        /// <param name="reportDto">
        ///     An instance of <see cref="ReportModifyRequestDto"/> containing all the data to change a <see cref="Report"/> in the database.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="ReportResponseDto"/> with all the data of the modified <see cref="Report"/>.
        /// </returns>
        Task<DataResult<ReportResponseDto>> ModifyReportAsync(ReportModifyRequestDto reportDto);

        #endregion

        #region Link
        /// <summary>
        ///     Link images to the added report.
        /// </summary>
        /// <param name="reportId">
        ///     The id of the report the image should be linked to.
        /// </param>
        /// <param name="imageId">
        ///     The id of the image to be linked.
        /// </param>
        /// <returns>
        ///     A result indicating if the image was linked to the report.
        /// </returns>
        Task LinkImagesToReportAsync(string reportId, string imageId);

        #endregion
    }
}
