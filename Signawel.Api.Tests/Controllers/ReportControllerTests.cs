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

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class ReportControllerTests
    {
        private ReportController _reportController;
        private Mock<IReportService> _reportServiceMock;
        private Mock<IImageService> _imageServiceMock;
        private Mock<IMailService> _mailServiceMock;

        [SetUp]
        public void Setup()
        {
            _reportServiceMock = new Mock<IReportService>();
            _imageServiceMock = new Mock<IImageService>();
            _mailServiceMock = new Mock<IMailService>();
            _reportController = new ReportController(_reportServiceMock.Object, _imageServiceMock.Object, _mailServiceMock.Object);
        }

        [Test]
        public void GetAllReportsShouldReturnListOfReports()
        {
            // Arrange
            var reports = new List<ReportResponseDto>()
            {
                new ReportResponseDto(),
                new ReportResponseDto(),
                new ReportResponseDto()
            };

            var paginationResult = new ReportGetPaginationResponseDto()
            {
                Reports = reports,
                Total = reports.Count
            };

            _reportServiceMock.Setup(service => service.GetAllReports()).Returns(reports.AsQueryable);

            // Act
            var result = _reportController.GetReports() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);

            var reportGetPaginationResult = result.Value as ReportGetPaginationResponseDto;
            Assert.That(reportGetPaginationResult, Is.Not.Null);
            Assert.That(reportGetPaginationResult.Total, Is.EqualTo(reports.Count));
            Assert.That(reportGetPaginationResult.Reports, Is.EqualTo(reports));

            _reportServiceMock.Verify(service => service.GetAllReports(), Times.Once);
        }

        [Test]
        public void GetAllWithLimitShouldReturnLimitedReports()
        {
            var limit = 2;

            // Arrange
            var reports = new List<ReportResponseDto>()
            {
                new ReportResponseDto(),
                new ReportResponseDto(),
                new ReportResponseDto()
            };

            _reportServiceMock.Setup(service => service.GetAllReports()).Returns(reports.AsQueryable);

            // Act
            var result = _reportController.GetReports(limit: 2) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);

            var reportGetPaginationResult = result.Value as ReportGetPaginationResponseDto;
            Assert.That(reportGetPaginationResult, Is.Not.Null);
            Assert.That(reportGetPaginationResult.Reports.Count, Is.EqualTo(limit));

            _reportServiceMock.Verify(service => service.GetAllReports(), Times.Once);
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

        #region Helper

        private IFormFile GetImageFormFile()
        {
            var stream = new MemoryStream(File.ReadAllBytes(@"Assets/image.png"));
            return new FormFile(stream, 0, stream.Length, "image", "image.png");
        }

        #endregion

    }
}
