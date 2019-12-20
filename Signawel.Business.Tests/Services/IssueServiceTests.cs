using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Domain.Constants;
using Signawel.Domain.Reports;
using Signawel.Dto;
using Signawel.Dto.DefaultIssue;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class IssueServiceTests
    {
        private SignawelDbContext _signawelDbContext;
        private Mock<IMapper> _mapperMock;
        private IssueService _sutIssueService;

        [SetUp]
        public void SetUp()
        {
            _signawelDbContext = SignawelDbContextBuilder.GetDatabaseContext();
            _mapperMock = new Mock<IMapper>();
            _sutIssueService = new IssueService(_signawelDbContext,
                _mapperMock.Object);
        }


        #region AddDefaultIsueAsync

        [Test]
        public async Task AddDefaultIsueAsync_ShouldReturnError_WhenCreationFailes()
        {
            // Arrange
            var dto = new DefaultIssueRequestDto
            {
                Name = Guid.NewGuid().ToString()
            };
            
            var reportIssue = new ReportDefaultIssue
            {
                Name = dto.Name
            };

            _mapperMock.Setup(_ => _.Map<ReportDefaultIssue>(It.IsAny<DefaultIssueResponseDto>()))
                .Returns((ReportDefaultIssue) null);

            // Act
            var result = await _sutIssueService.AddDefaultIsueAsync(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.HasError(ErrorCodes.DefaultIssueCreationError), Is.True);
        }

        [Test]
        public async Task AddDefaultIsueAsync_ShouldReturnDto_WhenCreationSuccess()
        {
            // Arrange
            var dto = new DefaultIssueRequestDto
            {
                Name = Guid.NewGuid().ToString()
            };
            
            var reportIssue = new ReportDefaultIssue
            {
                Name = dto.Name
            };

            var responseEntity = new DefaultIssueResponseDto
            {
                Id = reportIssue.Id,
                Name = reportIssue.Name,
                Type = reportIssue.Type
            };

            _mapperMock
                .Setup(_ => _.Map<ReportDefaultIssue>(It.IsAny<DefaultIssueRequestDto>()))
                .Returns(reportIssue);

            _mapperMock
                .Setup(_ => _.Map<DefaultIssueResponseDto>(It.IsAny<ReportDefaultIssue>()))
                .Returns(responseEntity);

            // Act
            var result = await _sutIssueService.AddDefaultIsueAsync(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Succeeded, Is.True);
        }

        #endregion AddDefaultIsueAsync

        #region DeleteDefaultIssueAsync

        [Test]
        public async Task DeleteDefaultIssueAsync_ShouldReturnNotFound_WhenIssueNotFound()
        {
            // Arrange

            // Act
            var result = await _sutIssueService.DeleteDefaultIssueAsync("");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.HasError(ErrorCodes.NotFoundError), Is.True);
        }

        [Test]
        public async Task DeleteDefaultIssueAsync_ShouldReturnSuccess_WhenIssueDeleted()
        {
            // Arrange
            var di = new ReportDefaultIssue
            {
                Name = ""
            };

            await _signawelDbContext.DefaultIssues.AddAsync(di);
            await _signawelDbContext.SaveChangesAsync();

            // Act
            var result = await _sutIssueService.DeleteDefaultIssueAsync(di.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Succeeded, Is.True);
        }

        #endregion DeleteDefaultIssueAsync
    }
}
