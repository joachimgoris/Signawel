using Signawel.Domain.DataResults;
using Signawel.Dto.ReportGroup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IReportGroupService
    {
        #region Set
        /// <summary>
        ///     Adds a report to the database.
        /// </summary>
        /// <param name="reportGroupCreationRequest">
        ///     An instance of <see cref="ReportGroupCreationRequestDto"/> containing the data for a new <see cref="ReportGroup"/>.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="ReportGroupResponseDto"/> containing the data of the <see cref="ReportGroup"/> as it is in the database.
        /// </returns>
        Task<DataResult<ReportGroupResponseDto>> SetReportGroupAsync(ReportGroupCreationRequestDto reportGroupCreationRequest);
        #endregion

        #region GetById

        /// <summary>
        ///     Get a single <see cref="ReportGroup"/> from the database.
        /// </summary>
        /// <param name="id">
        ///     The id of the <see cref="ReportGroup"/>.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="ReportGroupResponseDto"/> containing all the data of the <see cref="ReportGroup"/> as it is in the database.
        /// </returns>
        Task<DataResult<ReportGroupResponseDto>> GetReportGroupByIdAsync(string id);
        #endregion

        #region GetByParams

        /// <summary>
        ///     Get a List of <see cref="ReportGroup"/> from the database.
        /// </summary>
        /// <param name="city">
        ///     The name of the <see cref="City"/> in <see cref="ReportGroup.CityReportGroup"/> .
        /// </param>
        /// <param name="email">
        ///     The address of the <see cref="Email"/> in <see cref="ReportGroup.EmailReportGroup"/> .
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing a list of <see cref="ReportGroupResponseDto"/> instances containing all the data of the <see cref="ReportGroup"/> as it is in the database.
        /// </returns>
        Task<DataResult<List<ReportGroupResponseDto>>> GetReportGroupsAsync(string city, string email);
        #endregion

        #region GetCities
        /// <summary>
        ///     Get a List of <see cref="City"/> from the database.
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing a List of <see cref="CityResponseDto"/> containing all the data of the <see cref="City"/> as it is in the database.
        /// </returns>
        Task<DataResult<List<CityResponseDto>>> GetAllCitiesAsync();
        #endregion

        #region Delete

        /// <summary>
        ///     Delete the <see cref="ReportGroup"/> from the database.
        /// </summary>
        /// <param name="id">
        ///     The id of the <see cref="ReportGroup"/> to be deleted.
        /// </param>
        Task<DataResult> DeleteReportGroupAsync(string id);
        #endregion

        #region Modify
        /// <summary>
        ///     Modifies the reportGroup in the database with the changes in the new reportGroup.
        /// </summary>
        /// <param name="reportGroup">
        ///     An instance of <see cref="ReportGroup"/> containing all the data to change a <see cref="ReportGroup"/> in the database.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing an instance of <see cref="ReportGroupResponseDto"/> with all the data of the modified <see cref="ReportGroup"/>.
        /// </returns>
        Task<DataResult<ReportGroupResponseDto>> ModifyReportGroupAsync(string id,ReportGroupCreationRequestDto reportGroup);
        #endregion
    }
}
