using Signawel.Domain.DataResults;
using Signawel.Dto.PriorityEmail;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IPriorityEmailService
    {
        /// <summary>
        ///     Check if a mail suffix is a priority 
        /// </summary>
        /// <param name="emailSuffix">
        ///     Suffix to check
        /// </param>
        /// <returns>
        ///     True if suffix is priority
        /// </returns>
        Task<bool> CheckPriorityEmailAsync(string emailSuffix);

        /// <summary>
        ///     Get all priority emails
        /// </summary>
        /// <returns>
        ///     Instance of <see cref="IQueryable{T}"/> of type <see cref="PriorityEmailResponseDto"/>
        /// </returns>
        IQueryable<PriorityEmailResponseDto> GetPriorityEmails();

        /// <summary>
        ///     Add a priority email suffix
        /// </summary>
        /// <param name="dto">
        ///     Instance of <see cref="PriorityEmailCreationRequestDto"/> containing details about the suffix to add.
        /// </param>
        /// <returns>
        ///     Instance of <see cref="DataResult{TEntity}"/> of type <see cref="PriorityEmailResponseDto"/> containing the newly added priorityemail or errors.
        /// </returns>
        Task<DataResult<PriorityEmailResponseDto>> AddPriorityEmailAsync(PriorityEmailCreationRequestDto dto);

        /// <summary>
        ///     Remove a priority email.
        /// </summary>
        /// <param name="id">
        ///     Id of the priority email to remove.
        /// </param>
        /// <returns>
        ///     Instance of <see cref="DataResult"/> with success or errors.
        /// </returns>
        Task<DataResult> RemovePriorityEmailAsync(string id);
    }
}
