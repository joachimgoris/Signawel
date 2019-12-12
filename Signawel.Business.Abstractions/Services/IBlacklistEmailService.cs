using Signawel.Domain.DataResults;
using Signawel.Dto.BlacklistEmail;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IBlacklistEmailService
    {
        /// <summary>
        ///     Check if an email is blacklisted.
        /// </summary>
        /// <param name="emailSuffix">
        ///     Email to check.
        /// </param>
        /// <returns>
        ///     Success if email is blacklisted.
        /// </returns>
        Task<DataResult> CheckBlacklistEmailAsync(string email);

        /// <summary>
        ///     Get all blacklisted emails.
        /// </summary>
        /// <returns>
        ///     Instance of <see cref="DataResult{TEntity}"/> with an instance of <see cref="IQueryable{T}"/> of type <see cref="BlacklistEmailResponseDto"/>.
        /// </returns>
        DataResult<IQueryable<BlacklistEmailResponseDto>> GetBlacklistEmails();

        /// <summary>
        ///     Add a blacklist email.
        /// </summary>
        /// <param name="dto">
        ///     Instance of <see cref="BlacklistEmailCreationRequestDto"/> containing details about the email to add.
        /// </param>
        /// <returns>
        ///     Instance of <see cref="DataResult{TEntity}"/> of type <see cref="BlacklistEmailResponseDto"/> containing the newly added blacklist email or errors.  
        /// </returns>
        Task<DataResult<BlacklistEmailResponseDto>> AddBlacklistEmailAsync(BlacklistEmailCreationRequestDto dto);

        /// <summary>
        ///     Remove a blacklisted email.
        /// </summary>
        /// <param name="id">
        ///     Id of the blacklisted email to remove.
        /// </param>
        /// <returns>
        ///     Instance of <see cref="DataResult"/> with success or errors.
        /// </returns>
        Task<DataResult> RemoveBlacklistEmailAsync(string id);
    }
}
