using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Domain;
using System.Threading.Tasks;

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

        #region AddReportAsync

        [Test]
        public async Task AddReportAsyncShouldReturnCorrectReportWhenValidReportIsGiven()
        {
            // Arrange


            // Act
            var result = await _service.AddReportAsync(new Report());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<Report>());
        }

        [Test]
        public async Task AddReportAsyncShouldReturnNullWhenInvalidReportIsGiven()
        {
            // Arrange


            // Act
            var result = await _service.AddReportAsync(null);

            // Assert
            Assert.That(result, Is.Null);
            Assert.That(result, Is.Not.TypeOf<Report>());
        }

        #endregion

        #region DeleteReportAsync



        #endregion
    }
}