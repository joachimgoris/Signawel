using System.Collections.Generic;
using System.Threading.Tasks;
using Signawel.Dto;
using Signawel.Mobile.Models;

namespace Signawel.Mobile.Services.Abstract
{
    public interface IReportService
    {
        /// <summary>
        ///     Sends a request to the api to add a report to the database.
        /// </summary>
        /// <param name="reportData">
        ///     Utility class used to link pictures and report data together.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="Task{TResult}"/> containing an instance of <see cref="IList{T}"/> containing instances of <see cref="ErrorResponseDto"/>. OK response may contain errors if one or more images failed to be added to the database.
        /// </returns>
        Task<IList<ErrorResponseDto>> AddReport(ReportData reportData);
    }
}
