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
using Microsoft.AspNetCore.WebUtilities;
using Signawel.Domain.DataResults;
using Signawel.Domain.Constants;

namespace Signawel.Business.Services
{
    /// <inheritdoc cref="IMailService"/>
    public class MailService : IMailService
    {
        private readonly MailConfiguration _configuration;
        private readonly IPriorityEmailService _priorityEmailService;

        public MailService(IOptions<MailConfiguration> mailConfiguration, IPriorityEmailService priorityEmailService)
        {
            _configuration = mailConfiguration.Value;
            _priorityEmailService = priorityEmailService;
        }

        #region SendConfirmationEmail

        /// <inheritdoc cref="IMailService.SendConfirmationEmailAsync(User, string)"/>
        public async Task<DataResult> SendConfirmationEmailAsync(User user, string token)
        {
            var tokenString = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            string frontEndUrl = _configuration.FrontEndUrl;
            var callbackUri = new Uri(frontEndUrl + $"authentication/confirmemail?userId={user.Id}&token={tokenString}");

            var mailDto = new SendMailDto
            {
                DestinationAddress = user.Email,
                Subject = "Please confirm your registration.",
                Body = callbackUri.AbsoluteUri
            };

            var mailResult = await SendMail(mailDto);
            if (!mailResult.Succeeded)
                return DataResult.WithErrorsFromDataResult(mailResult);

            return DataResult.Success;
        }

        #endregion

        #region Sendmail

        /// <inheritdoc cref="IMailService.SendMail(SendMailDto)"/>
        public async Task<DataResult> SendMail(SendMailDto sendMailDto)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient
                {
                    Host = _configuration.Host,
                    Port = _configuration.Port,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_configuration.Sender, _configuration.Password)
                };

                MailMessage message = new MailMessage(_configuration.Sender, sendMailDto.DestinationAddress)
                {
                    Body = sendMailDto.Body,
                    IsBodyHtml = true,
                    Subject = sendMailDto.Subject
                };

                await smtpClient.SendMailAsync(message);
                return DataResult.Success;
            }
            catch (ArgumentNullException)
            {
                return DataResult.WithPublicError(ErrorCodes.ParameterEmptyError, "Message or sender address is null.");
            }
            catch (InvalidOperationException)
            {
                return DataResult.WithPublicError(ErrorCodes.InvalidOperationError, "The client has a Send call in progress or the DeliveryMethod is invalid.");
            }
            catch (SmtpException) 
            {
                return DataResult.WithPublicError(ErrorCodes.MailError, "An SmtpException occurred.");
            }
            catch (Exception)
            {
                return DataResult.WithPublicError(ErrorCodes.MailError, "Something went wrong during the mail process.");
            }
        }

        #endregion
        
        #region CreateReportEmail

        /// <inheritdoc cref="IMailService.CreateReportEmailAsync(ReportResponseDto)"/>
        public async Task<DataResult> CreateReportEmailAsync(ReportResponseDto report)
        {
            string mailSuffix = report.UserEmail.Split('@')[1];
            bool priority = await _priorityEmailService.CheckPriorityEmailAsync(mailSuffix);

            string subject = (priority ? "!P! " : "") + $"Foutmelding wegenwerken nr : {report.Id}";

            #region Email Body

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

            foreach (string city in report.Cities)
            {
                mailBody.Append(city + ", ");
            }

            #endregion Email Body

            SendMailDto email = new SendMailDto
            {
                Body = mailBody.ToString(),
                DestinationAddress = report.UserEmail,  //TODO needs to be changed later to the email connected to the cities
                Subject = subject
            };

            var mailResult = await SendMail(email);

            if (!mailResult.Succeeded)
                return DataResult.WithErrorsFromDataResult(mailResult);

            return DataResult.Success;
        }

        #endregion
    }
}