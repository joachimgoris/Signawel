using System;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto.Mail;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Signawel.Domain.Configuration;
using Signawel.Domain;
using System.Text;
using Signawel.Dto.Reports;
using System.Threading.Tasks;

namespace Signawel.Business.Services
{
    public class MailService : IMailService
    {
        private IOptions<MailConfiguration> _options;
        private readonly IPriorityEmailService _priorityEmailService;
        public MailService(IOptions<MailConfiguration> options,
            IPriorityEmailService priorityEmailService)
        {
            this._options = options;
            this._priorityEmailService = priorityEmailService;
        }
        public bool SendMail(SendMailDto sendMailDto)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = _options.Value.Host;
                client.Port = _options.Value.Port;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_options.Value.Sender, _options.Value.Password);
                MailMessage message = new MailMessage(_options.Value.Sender, sendMailDto.destinationAddress);
                message.Body = sendMailDto.Body;
                message.IsBodyHtml = true;
                message.Subject = sendMailDto.Subject;

                string userState = "Signawel test message";
                client.SendAsync(message, userState);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task CreateEmailAsync(ReportCreationResponseDto report)
        {
            string mailSuffix = report.UserEmail.Split('@')[1];
            bool priority = await _priorityEmailService.CheckPriorityEmailAsync(mailSuffix);


            string subject = "";
            StringBuilder mailBody = new StringBuilder("<h4>Probleem geraporteerd aan wegenwerk met GipodId: </h4>");
            mailBody.Append(report.RoadWorkId);
            mailBody.Append("<br><br>");
            mailBody.Append("<h4>Fout/fouten:</h4><br><ul>");

            if (report.IssueLink != null)
            {
                foreach (ReportIssue issue in report.IssueLink)
                {
                    mailBody.Append("<li>" + issue.Issue.Name + "</li>");
                    mailBody.Append("\n\t");
                }
            }
            mailBody.Append("</ul><br><br>");
            mailBody.Append("<h4>Beschrijving probleem: </h4>" + report.CustomMessage + "<br>");
            mailBody.Append("------------------------------------------<br>");
            mailBody.Append(report.Description + "<br>");

            foreach (string city in report.Cities)
            {
                mailBody.Append(city + ", ");
            }

            if (priority)
            {
                subject = "!P! ";
            }

            subject += "Foutmelding wegenwerken nr : " + report.Id;

            SendMailDto email = new SendMailDto();
            email.Body = mailBody.ToString();
            email.destinationAddress = report.UserEmail;  //TODO needs to be changed later to the email connected to the cities
            email.Subject = subject;

            SendMail(email);
        }
    }
}