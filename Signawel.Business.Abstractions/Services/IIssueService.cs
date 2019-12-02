using System.Linq;
using Signawel.Dto;

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
    }
}
