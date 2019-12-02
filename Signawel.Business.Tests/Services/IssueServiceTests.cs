using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Domain.Reports;
using Signawel.Dto;

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

        [Test]
        public void GetDefaultIssues_ShouldReturnAListOfQueryableDefaultIssueResponseDtos()
        {
            _mapperMock.Setup(mapper =>
                mapper.ProjectTo<DefaultIssueResponseDto>(
                    It.IsAny<IQueryable<ReportDefaultIssue>>(),
                    null,
                    null))
                .Returns(Enumerable.Empty<DefaultIssueResponseDto>().AsQueryable);

            var result = _sutIssueService.GetDefaultIssues();

            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<IQueryable<DefaultIssueResponseDto>>(result);
        }
    }
}
