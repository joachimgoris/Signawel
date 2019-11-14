using System;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto.Mail;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Signawel.Business.Services
{
    public class MailService : IMailService
    {
        public bool SendMail(SendMailDto sendMailDto)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                // Todo put these settings in appsettings
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("testing.goofydev@gmail.com", "LLzJ(Vzx)}M)9!WD");
                MailMessage message = new MailMessage("testing.goofydev@gmail.com", sendMailDto.destinationAddress,
                    "Signawel test report", sendMailDto.Body);

                string userState = "Signawel test message";
                client.SendAsync(message, userState);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}