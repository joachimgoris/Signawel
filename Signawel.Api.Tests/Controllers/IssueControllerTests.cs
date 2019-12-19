using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.DefaultIssue;
using System.Threading.Tasks;
using Signawel.Dto;

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class IssueControllerTests
    {

        public IssuesController _issueController;
        private Mock<IIssueService> _issueServiceMock;

        [SetUp]
        public void Setup()
        {
            _issueServiceMock = new Mock<IIssueService>();
            _issueController = new IssuesController(_issueServiceMock.Object);
        }

        #region AddDefaultIssue

        [Test]
        public async Task AddDefaultIssue_ShouldReturnError_WhenCreationFailes()
        {
            // Arrange
            _issueServiceMock
                .Setup(_ => _.AddDefaultIsueAsync(It.IsAny<DefaultIssueRequestDto>()))
                .ReturnsAsync(DataResult<DefaultIssueResponseDto>.WithError("", ""));

            // Act
            var result = await _issueController.AddDefaultIssue(null);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task AddDefaultIssue_ShouldReturnOk_WhenCreated()
        {
            // Arrange
            _issueServiceMock
                .Setup(_ => _.AddDefaultIsueAsync(It.IsAny<DefaultIssueRequestDto>()))
                .ReturnsAsync(DataResult<DefaultIssueResponseDto>.Success(new DefaultIssueResponseDto()));

            // Act
            var result = await _issueController.AddDefaultIssue(null);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        #endregion AddDefaultIssue

        #region DeleteDefaultIssue

        [Test]
        public async Task DeleteDefaultIssue_ShouldReturnNotFound_WhenIssueNotFound()
        {
            // Arrange
            _issueServiceMock
                .Setup(_ => _.DeleteDefaultIssueAsync(It.IsAny<string>()))
                .ReturnsAsync(DataResult<DefaultIssueResponseDto>.WithError(ErrorCodes.NotFoundError, ""));

            // Act
            var result = await _issueController.DeleteDefaultIssue("");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task DeleteDefaultIssue_ShouldReturnBadRequest_WhenDeletionFailed()
        {
            // Arrange
            _issueServiceMock
                .Setup(_ => _.DeleteDefaultIssueAsync(It.IsAny<string>()))
                .ReturnsAsync(DataResult<DefaultIssueResponseDto>.WithError(ErrorCodes.DefaultIssueDeletionError, ""));

            // Act
            var result = await _issueController.DeleteDefaultIssue("");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeleteDefaultIssue_ShouldReturnNoContent_WhenDeletionSuccessful()
        {
            // Arrange
            _issueServiceMock
                .Setup(_ => _.DeleteDefaultIssueAsync(It.IsAny<string>()))
                .ReturnsAsync(DataResult<DefaultIssueResponseDto>.Success(new DefaultIssueResponseDto()));

            // Act
            var result = await _issueController.DeleteDefaultIssue("");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        #endregion DeleteDefaultIssue

    }
}
