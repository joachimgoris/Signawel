using Signawel.Domain;
using Signawel.Domain.DataResults;
using Signawel.Dto.Reports;
using System.Linq;
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
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="ReportCreationResponseDto"/> containing the data of the <see cref="Report"/> as it is in the database.
        /// </returns>
        Task<DataResult<ReportCreationResponseDto>> AddReportAsync(ReportCreationRequestDto reportDto);

        #endregion

        #region Get

        /// <summary>
        ///     Get a single <see cref="Report"/> from the database.
        /// </summary>
        /// <param name="reportId">
        ///     The id of the <see cref="Report"/>.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="ReportGetResponseDto"/> containing all the data of the <see cref="Report"/> as it is in the database.
        /// </returns>
        Task<DataResult<ReportGetResponseDto>> GetReportAsync(string reportId);

        /// <summary>
        ///     Get all the reports currently in the database.
        /// </summary>
        /// <returns>
        ///     A list of all the reports in the database.
        /// </returns>
        IQueryable<Report> GetAllReports();

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
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="ReportModifyResponseDto"/> with all the data of the modified <see cref="Report"/>.
        /// </returns>
        Task<DataResult<ReportModifyResponseDto>> ModifyReportAsync(ReportModifyRequestDto reportDto);

        #endregion
    }
}
