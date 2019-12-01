using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Domain;
using System.Threading.Tasks;
using Signawel.Domain.Constants;
using Signawel.Dto.Reports;
using Signawel.Domain.Reports;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class ReportServiceTests
    {
        private ReportService _service;
        private SignawelDbContext _context;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<ReportService>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _context = SignawelDbContextBuilder.GetDatabaseContext();
            _mockLogger = new Mock<ILogger<ReportService>>();

            _service = new ReportService(_context, _mockLogger.Object, _mockMapper.Object);
        }

        #region AddReport

        [Test]
        public async Task AddReport_ShouldReturnParameterEmptyError_WhenReportDtoIsEmpty()
        {
            // Arrange
            

            // Act
            var result = await _service.AddReportAsync(It.IsAny<ReportCreationRequestDto>());

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.ParameterEmptyError), Is.True);
        }

        [Test]
        public async Task AddReport_ShouldReturnCorrectObject_WhenReportIsAdded()
        {
            // Arrange
            var model = new ReportCreationRequestDto
            {
                SenderEmail = "test@test.com"
            };
            _mockMapper.Setup(_ => _.Map<Report>(It.IsAny<ReportCreationRequestDto>()))
                .Returns(new Report());
            _mockMapper.Setup(_ => _.Map<ReportResponseDto>(It.IsAny<Report>()))
                .Returns(new ReportResponseDto());

            // Act
            var result = await _service.AddReportAsync(model);

            // Assert
            Assert.That(result.Errors, Is.Empty);
            Assert.That(result.Succeeded, Is.True);
        }

        #endregion

        #region DeleteReportAsync



        #endregion
    }
}