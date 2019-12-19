using System.Linq;
using System.Threading.Tasks;
using Signawel.Domain.DataResults;
using Signawel.Dto.DefaultIssue;

namespace Signawel.Business.Abstractions.Services
{
    public interface IIssueService
    {
        /// <summary>
        ///     Requests all default issues from the database.
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IQueryable{T}"/> containing instances of <see cref="DefaultIssueResponseDto"/>. 
        /// </returns>
        IQueryable<DefaultIssueResponseDto> GetDefaultIssues();

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
