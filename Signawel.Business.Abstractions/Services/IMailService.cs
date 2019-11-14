using System.Threading.Tasks;
using Signawel.Dto.Mail;

namespace Signawel.Business.Abstractions.Services
{
    public interface IMailService
    {
        bool SendMail(SendMailDto sendMailDto);
    }
}
