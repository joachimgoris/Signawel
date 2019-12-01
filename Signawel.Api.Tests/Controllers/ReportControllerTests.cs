using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Api.Tests.Builders;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto.Reports;
using System;

namespace Signawel.Api.Tests.Controllers
{
    public class ReportControllerTests
    {
        private ReportController _reportController;
        private Mock<IReportService> _reportServiceMock;
        private Mock<IMailService> _mailServiceMock;

        [SetUp]
        public void Setup()
        {
            _reportServiceMock = new Mock<IReportService>();
            _mailServiceMock = new Mock<IMailService>();
            _reportController = new ReportController(_reportServiceMock.Object, _mailServiceMock.Object);
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

            var paginationResult = new ReportGetPaginationDto()
            {
                Reports = reports,
                Total = reports.Count
            };

            _reportServiceMock.Setup(service => service.GetAllReports()).Returns(reports.AsQueryable);

            // Act
            var result = _reportController.GetReports() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);

            var reportGetPaginationResult = result.Value as ReportGetPaginationDto;
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

            var reportGetPaginationResult = result.Value as ReportGetPaginationDto;
            Assert.That(reportGetPaginationResult, Is.Not.Null);
            Assert.That(reportGetPaginationResult.Reports.Count, Is.EqualTo(limit));

            _reportServiceMock.Verify(service => service.GetAllReports(), Times.Once);
        }
    }
}
