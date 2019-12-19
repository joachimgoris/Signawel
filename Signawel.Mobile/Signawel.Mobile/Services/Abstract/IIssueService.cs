using System.Collections.Generic;
using System.Threading.Tasks;
using Signawel.Domain.Reports;
using Signawel.Dto;

namespace Signawel.Mobile.Services.Abstract
{
    public interface IIssueService
    {
        /// <summary>
        ///     Gets all default issues from the api.
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="Task{TResult}"/> containing an instance of <see cref="IList{T}"/> containing instances of <see cref="DefaultIssueResponseDto"/>
        /// </returns>
        Task<IList<ReportDefaultIssue>> GetAllDefaultIssues();
    }
}
