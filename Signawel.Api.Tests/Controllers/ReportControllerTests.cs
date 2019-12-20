using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Api.Tests.Builders;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto;
using Signawel.Dto.Reports;
using Microsoft.AspNetCore.Identity;
using Signawel.Domain;

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class ReportControllerTests
    {
        private ReportController _reportController;
        private Mock<IReportService> _reportServiceMock;
        private Mock<IImageService> _imageServiceMock;
        private Mock<IMailService> _mailServiceMock;
        private Mock<UserManager<User>> _userManager;

        [SetUp]
        public void Setup()
        {
            _reportServiceMock = new Mock<IReportService>();
            _imageServiceMock = new Mock<IImageService>();
            _mailServiceMock = new Mock<IMailService>();
            _userManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);

            _reportController = new ReportController(_reportServiceMock.Object, _imageServiceMock.Object, _mailServiceMock.Object, _userManager.Object);
        }

        [Test]
        public async Task GetAllReportsShouldReturnListOfLimitedReports_WhenTheUserIsAInstance()
        {
            // Arrange
            var report1 = new ReportResponseDto
            {
                Cities = "Hasselt"
            };

            var report2 = new ReportResponseDto
            {
                Cities = "Beringen"
            };

            var reports = new List<ReportResponseDto>()
            {
                report1,
                report2
            };

            var paginationResult = new ReportGetPaginationResponseDto()
            {
                Reports = reports,
                Total = reports.Count
            };

            var user = new User
            {
                UserName = "Tibo"
            };

            _reportServiceMock
                .Setup(service => service.GetAllReports(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IList<string>>()))
                .ReturnsAsync(paginationResult);

            _userManager
                .Setup(_ => _.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act
            var result = await _reportController.GetReportsAsync() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);

            var reportGetPaginationResult = result.Value as ReportGetPaginationResponseDto;
            Assert.That(reportGetPaginationResult, Is.Not.Null);

            _reportServiceMock.Verify(service => service.GetAllReports(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IList<string>>()), Times.Once);
        }

        [Test]
        public async Task GetAllReportsShouldReturnListOfAllReports_WhenTheUserIsAAdmin()
        {
            // Arrange
            var report1 = new ReportResponseDto();
            report1.Cities = "Hasselt";

            var report2 = new ReportResponseDto();
            report2.Cities = "Beringen";

            var reports = new List<ReportResponseDto>()
            {
                report1,
                report2
            };

            var paginationResult = new ReportGetPaginationResponseDto()
            {
                Reports = reports,
                Total = reports.Count
            };

            var user = new User();
            user.UserName = "John";

            _userManager.Setup(manager => manager.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);

            _reportServiceMock
                .Setup(service => service.GetAllReports(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IList<string>>()))
                .ReturnsAsync(paginationResult);

            // Act
            var result = await _reportController.GetReportsAsync() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);

            var reportGetPaginationResult = result.Value as ReportGetPaginationResponseDto;
            Assert.That(reportGetPaginationResult, Is.Not.Null);
            Assert.That(reportGetPaginationResult.Total, Is.EqualTo(2));
            Assert.That(reportGetPaginationResult.Reports, Is.EqualTo(reports));

            _reportServiceMock.Verify(service => service.GetAllReports(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IList<string>>()), Times.Once);
        }

        [Test]
        public async Task GetAllWithLimitShouldReturnLimitedReports()
        {
            // Arrange
            var limit = 2;

            var report1 = new ReportResponseDto();
            report1.Cities = "Hasselt";

            var report2 = new ReportResponseDto();
            report2.Cities = "Beringen";

            var reports = new List<ReportResponseDto>()
            {
                report1,
                report2
            };

            var paginationResult = new ReportGetPaginationResponseDto()
            {
                Reports = reports,
                Total = reports.Count
            };

            var user = new User
            {
                UserName = "John"
            };

            _userManager
                .Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _reportServiceMock
                .Setup(service => service.GetAllReports(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IList<string>>()))
                .ReturnsAsync(paginationResult);

            _userManager
                .Setup(manager => manager.IsInRoleAsync(It.IsAny<User>(), Role.Constants.Instance))
                .ReturnsAsync(false);

            _userManager
                .Setup(manager => manager.IsInRoleAsync(It.IsAny<User>(), Role.Constants.Admin))
                .ReturnsAsync(true);


            // Act
            var result = await _reportController.GetReportsAsync(limit: 1) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);

            var reportGetPaginationResult = result.Value as ReportGetPaginationResponseDto;
            Assert.That(reportGetPaginationResult, Is.Not.Null);
            Assert.That(reportGetPaginationResult.Total, Is.EqualTo(limit));

            _reportServiceMock.Verify(service => service.GetAllReports(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IList<string>>()), Times.Once);
        }


        [Test]
        public async Task AddReport_ShouldReturnBadRequest_WhenReportIsNull()
        {
            var fileList = new List<IFormFile>();

            _reportServiceMock.Setup(service => service.AddReportAsync(It.IsAny<ReportCreationRequestDto>()))
                .ReturnsAsync(DataResult<ReportResponseDto>.WithError(ErrorCodes.ReportCreationError,
                    "The given Dto is empty.", DataErrorVisibility.Public));

            var result = await _reportController.AddReport(null, fileList) as BadRequestObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errorList = (IList<ErrorResponseDto>)result.Value;

            Assert.That(errorList.Any(error => error.Code.Equals(ErrorCodes.ReportCreationError)), Is.True);
        }

        [Test]
        public async Task AddReport_ShouldReturnOkWithFailedToSaveImage_WhenImageIsNotAddedToDatabase()
        {
            var report = new ReportCreationRequestDto();
            var fileList = new List<IFormFile>
            {
                GetImageFormFile()
            };

            _reportServiceMock.Setup(service => service.AddReportAsync(It.IsAny<ReportCreationRequestDto>()))
                .ReturnsAsync(DataResult<ReportResponseDto>.WithEntityOrError(
                new ReportResponseDto(), ErrorCodes.ReportCreationError, "Something went wrong when creating a report."));

            _imageServiceMock
                .Setup(service => service.AddImage(It.IsAny<MemoryStream>()))
                .ReturnsAsync(DataResult<string>.WithError("FailedToSaveImage", "Failed to add image to database", DataErrorVisibility.Public));

            var result = await _reportController.AddReport(report, fileList) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errors = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errors.Any(error => error.Code.Equals("FailedToSaveImage")), Is.True);
        }

        [Test]
        public async Task AddReport_ShouldReturnOkWithEmptyErrorList_WhenEverythingIsSuccessful()
        {
            var report = new ReportCreationRequestDto();
            var fileList = new List<IFormFile>
            {
                GetImageFormFile()
            };

            _reportServiceMock.Setup(service => service.AddReportAsync(It.IsAny<ReportCreationRequestDto>()))
                .ReturnsAsync(DataResult<ReportResponseDto>.WithEntityOrError(
                    new ReportResponseDto(), ErrorCodes.ReportCreationError, "Something went wrong when creating a report."));

            _reportServiceMock.Setup(service => 
                service.LinkImagesToReportAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(1));

            _imageServiceMock
                .Setup(service => service.AddImage(It.IsAny<MemoryStream>()))
                .ReturnsAsync(DataResult<string>.Success(Guid.NewGuid().ToString()));

            var result = await _reportController.AddReport(report, fileList) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errors = (IList<ErrorResponseDto>) result.Value;

            CollectionAssert.IsEmpty(errors);
        }

        [Test]
        public async Task AddReport_ShouldReturnOkResultWithMultipleErrors_WhenMultipleImagesFailToBeAddedToDatabase()
        {
            var report = new ReportCreationRequestDto();
            var fileList = new List<IFormFile>
            {
                GetImageFormFile(),
                GetImageFormFile(),
                GetImageFormFile()
            };

            _reportServiceMock.Setup(service => service.AddReportAsync(It.IsAny<ReportCreationRequestDto>()))
                .ReturnsAsync(DataResult<ReportResponseDto>.WithEntityOrError(
                    new ReportResponseDto(), ErrorCodes.ReportCreationError, "Something went wrong when creating a report."));

            _imageServiceMock
                .Setup(service => service.AddImage(It.IsAny<MemoryStream>()))
                .ReturnsAsync(DataResult<string>.WithError("FailedToSaveImage", "Failed to add image to database", DataErrorVisibility.Public));

            var result = await _reportController.AddReport(report, fileList) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errors = (IList<ErrorResponseDto>) result.Value;

            CollectionAssert.IsNotEmpty(errors);
            Assert.That(errors.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task AddReport_ShouldReturnOkResultWithInvalidFileFormat_WhenPassedFileWithWrongFormat()
        {
            var report = new ReportCreationRequestDto();
            var formList = new List<IFormFile>
            {
                new FormFileBuilder()
                    .WithContent("test")
                    .WithFileName("test.test")
                    .Build()
            };

            _reportServiceMock.Setup(service => service.AddReportAsync(It.IsAny<ReportCreationRequestDto>()))
                .ReturnsAsync(DataResult<ReportResponseDto>.WithEntityOrError(
                    new ReportResponseDto(), ErrorCodes.ReportCreationError, "Something went wrong when creating a report."));

            var result = await _reportController.AddReport(report, formList) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errors = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errors.Any(error => error.Code.Equals("InvalidFileFormat")), Is.True);
        }

        [Test]
        public async Task DeleteReportShouldReturnNoContentWhenValidIdIsGiven()
        {
            // Arrange

            var reportId = Guid.NewGuid().ToString();

            _reportServiceMock.Setup(service => service.DeleteReportAsync(It.IsAny<string>()))
                .ReturnsAsync(DataResult.Success);

            // Act
            var result = await _reportController.DeleteReport(reportId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<NoContentResult>());

            _reportServiceMock.Verify(service => service.DeleteReportAsync(reportId), Times.Once);
        }

        #region Helper

        private IFormFile GetImageFormFile()
        {
            var stream = new MemoryStream(File.ReadAllBytes(@"Assets/image.png"));
            return new FormFile(stream, 0, stream.Length, "image", "image.png");
        }

        #endregion

    }
}
