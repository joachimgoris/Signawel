using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto;
using Signawel.Dto.BlacklistEmail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class BlacklistEmailControllerTests
    {
        private BlacklistEmailController _controller;
        private Mock<IBlacklistEmailService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IBlacklistEmailService>();
            _controller = new BlacklistEmailController(_mockService.Object);
        }

        #region GetAllBlacklistEmails

        [Test]
        public async Task GetAllBlacklistEmails_ShouldReturnBadRequest_WhenEmailsCouldntBeRetrieved()
        {
            // Arrange
            _mockService.Setup(_ => _.GetBlacklistEmails())
                .Returns(DataResult<IQueryable<BlacklistEmailResponseDto>>.WithPublicError(ErrorCodes.BlacklistEmailCreationError, "test error"));

            // Act
            var result = await _controller.GetAllBlacklistEmails() as BadRequestObjectResult;


            // Assert
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());
        }

        #endregion

        #region AddBlacklistEmail

        [Test]
        public async Task AddBlacklistEmail_ShouldReturnBadRequest()
        {
            // Arrange
            _mockService.Setup(_ => _.AddBlacklistEmailAsync(It.IsAny<BlacklistEmailCreationRequestDto>()))
                .ReturnsAsync(DataResult<BlacklistEmailResponseDto>.WithPublicError(ErrorCodes.BlacklistEmailCreationError, "test error"));
            var dto = new BlacklistEmailCreationRequestDto
            {
                Email = "test@email.com"
            };

            // Act
            var result = await _controller.AddBlacklistEmail(dto) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());
        }

        [Test]
        public async Task AddBlacklistEmail_ShouldReturnSuccess()
        {
            // Arrange
            _mockService.Setup(_ => _.AddBlacklistEmailAsync(It.IsAny<BlacklistEmailCreationRequestDto>()))
                .ReturnsAsync(DataResult<BlacklistEmailResponseDto>.Success(new BlacklistEmailResponseDto
                {
                    Email = "test@email.com"
                }));
            var dto = new BlacklistEmailCreationRequestDto
            {
                Email = "test@email.com"
            };

            // Act
            var result = await _controller.AddBlacklistEmail(dto) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<BlacklistEmailResponseDto>());
        }

        #endregion

        #region DeleteBlacklistEmail

        [Test]
        public async Task DeleteBlacklistEmail_ShouldReturnBadRequest()
        {
            // Arrange
            _mockService.Setup(_ => _.RemoveBlacklistEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(DataResult.WithPublicError(ErrorCodes.BlacklistEmailCreationError, "test error"));

            // Act
            var result = await _controller.DeleteBlacklistEmail(null) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());
        }

        [Test]
        public async Task DeleteBlacklistEmail_ShouldReturnSuccess()
        {
            // Arrange
            _mockService.Setup(_ => _.RemoveBlacklistEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(DataResult.Success);

            // Act
            var result = await _controller.DeleteBlacklistEmail(null) as NoContentResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion
    }
}