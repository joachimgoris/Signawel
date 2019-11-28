using Signawel.Domain;
using Signawel.Domain.DataResults;
using Signawel.Dto.Mail;
using Signawel.Dto.Reports;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    /// <summary>
    ///     The service to use for sending emails.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        ///     Sends a mail with the configured smtp client.
        /// </summary>
        /// <param name="sendMailDto">
        ///     An instance of <see cref="SendMailDto"/>.
        /// </param>
        /// <returns>
        ///     True or false depending on succes of the method.
        /// </returns>
        Task<DataResult> SendMail(SendMailDto sendMailDto);

        /// <summary>
        ///     Sends the confirmation email with the given token for the given user.
        /// </summary>
        /// <param name="user">
        ///     An instance of <see cref="User"/>.
        /// </param>
        /// <param name="token">
        ///     A string for the confirmation token.
        /// </param>
        /// <returns>
        ///     True or false depending on succes of the method.
        /// </returns>
        Task<DataResult> SendConfirmationEmailAsync(User user, string token);

        /// <summary>
        ///     Creates an email for a report and sends it.
        /// </summary>
        /// <param name="report">
        ///     An instance of <see cref="ReportResponseDto"/> containing all the data for a report email.
        /// </param>
        Task<DataResult> CreateReportEmailAsync(ReportResponseDto report);
    }
}
