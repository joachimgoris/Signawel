using System.Collections.Generic;
using System.Threading.Tasks;
using Signawel.Domain.DataResults;
using Signawel.Dto;
using Signawel.Dto.DefaultIssue;

namespace Signawel.Business.Abstractions.Services
{
    public interface IIssueService
    {
        /// <summary>
        ///     Requests all default issues from the database.
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IList{T}"/> containing instances of <see cref="DefaultIssueResponseDto"/>. 
        /// </returns>
        Task<IList<DefaultIssueResponseDto>> GetDefaultIssues();

        /// <summary>
        ///     Get a default issue from the database
        /// </summary>
        /// <param name="id">
        ///     Id of the issue to get
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{TEntity}"/> containing details about the operation.
        /// </returns>
        Task<DataResult<DefaultIssueResponseDto>> GetDefaultIssue(string id);

        /// <summary>
        ///     Add a new default issue.
        /// </summary>
        /// <param name="dto">
        ///     Instance of <see cref="DefaultIssueRequestDto"/> contains details about the new default issue to add.
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DataResult{T}"/> containing instance of <see cref="DefaultIssueResponseDto"/>
        /// </returns>
        Task<DataResult<DefaultIssueResponseDto>> AddDefaultIsueAsync(DefaultIssueRequestDto dto); 

        /// <summary>
        ///     Delete a default issue
        /// </summary>
        /// <param name="id">
        ///     Id of the default issue to delete
        /// </param>
        /// <returns>
        ///     Instance of <see cref="DataResult"/> containg error that have occured of <see cref="DataResult.Succeeded"/> = true if successful.
        /// </returns>
        Task<DataResult> DeleteDefaultIssueAsync(string id);
    }
}
