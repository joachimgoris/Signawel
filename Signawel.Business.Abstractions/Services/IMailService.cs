using Signawel.Dto.Mail;
using Signawel.Dto.Reports;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IMailService
    {
        bool SendMail(SendMailDto sendMailDto);

        Task CreateEmailAsync(ReportCreationResponseDto report);
    }
}
