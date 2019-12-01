using NUnit.Framework;
using Signawel.Business.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain;
using Signawel.Domain.Configuration;
using Signawel.Domain.Constants;
using Signawel.Dto.Mail;
using Signawel.Dto.Reports;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class MailServiceTests
    {
        private MailService _service;
        private IOptions<MailConfiguration> _mailConfiguration;
        private Mock<IPriorityEmailService> _mockPriorityEmailService;
        

        [SetUp]
        public void SetUp()
        {
            var config = new MailConfiguration
            {
                Host = "smtp.gmail.com",
                Password = "Test123-123",
                Port = 587,
                Sender = "signawelTestEmail@gmail.com",
                SenderName = "Signawel Test Email",
                FrontEndUrl = "https://localhost:5001/api"
            };
            _mockPriorityEmailService = new Mock<IPriorityEmailService>();
            _mailConfiguration = Options.Create(config);
            _service = new MailService(_mailConfiguration, _mockPriorityEmailService.Object);
        }

        #region  SendMail

        [Test]
        public async Task SendMail_ShouldReturnParameterEmptyError_WhenMailDtoIsEmpty()
        {
            // Arrange
            
            
            // Act
            var result = await _service.SendMail(new SendMailDto());

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.ParameterEmptyError), Is.True);
        }

        [Test]
        public async Task SendMail_ShouldReturnSuccess()
        {
            // Arrange
            var model = new SendMailDto
            {
                Body = "test",
                DestinationAddress = "test@test.com",
                Subject = "test"
            };
            
            // Act
            var result = await _service.SendMail(model);

            // Assert
            Assert.That(result.Succeeded, Is.True);
        }

        #endregion

        #region SendConfirmationEmail

        [Test]
        public async Task SendConfirmationEmail_ShouldReturnErrorsFromSendMail_WhenModelIsInvalid()
        {
            // Arrange

            // Act
            var result = await _service.SendConfirmationEmailAsync(new User(), Guid.NewGuid().ToString());

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.ParameterEmptyError), Is.True);
        }

        [Test]
        public async Task SendConfirmationEmail_ShouldReturnSuccess()
        {
            // Arrange
            var user = new User
            {
                Email = "test@test.com"
            };

            // Act
            var result = await _service.SendConfirmationEmailAsync(user, Guid.NewGuid().ToString());

            // Assert
            Assert.That(result.Succeeded, Is.True);
        }

        #endregion

        #region CreateReportEmailAsync

        [Test]
        public async Task CreateReportEmail_ShouldReturnSuccess()
        {
            // Arrange
            var model = new ReportResponseDto
            {
                UserEmail = "test@test.com",
                Cities = new List<string>
                {
                    "testCity"
                },
                IssueLink = new List<ReportIssue>
                {
                    new ReportIssue
                    {
                        Issue = new DefaultIssue
                        {
                            Name = "testIssue"
                        }
                    }
                }
            };
            
            // Act
            var result = await _service.CreateReportEmailAsync(model);

            // Assert
            Assert.That(result.Succeeded, Is.True);
        }

        #endregion
    }
}
