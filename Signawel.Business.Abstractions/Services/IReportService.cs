using Signawel.Domain;
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
        /// <param name="report">
        ///     The report to add to the datebase.
        /// </param>
        /// <returns>
        ///     The report as it is in the database.
        /// </returns>
        Task<Report> AddReportAsync(Report report);

        #endregion

        #region Get

        /// <summary>
        ///     Get a single report from the database.
        /// </summary>
        /// <param name="reportId">
        ///     The id of the report.
        /// </param>
        /// <returns>
        ///     The single report as it is in the database.
        /// </returns>
        Task<Report> GetReportAsync(string reportId);

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
        ///     Delete the report from the database.
        /// </summary>
        /// <param name="reportId">
        ///     The id of the report to be deleted.
        /// </param>
        Task DeleteReportAsync(string reportId);

        #endregion

        #region Modify

        /// <summary>
        ///     Modifies the report in the database with the changes in the new report.
        /// </summary>
        /// <param name="report">
        ///     The report containing the changes.
        /// </param>
        /// <returns>
        ///     The modified report as it is in the database.
        /// </returns>
        Task<Report> ModifyReportAsync(Report report);

        #endregion
    }
}
