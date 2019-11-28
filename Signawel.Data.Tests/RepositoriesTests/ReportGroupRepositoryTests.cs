using AutoMapper;
using Moq;
using NUnit.Framework;
using Signawel.Data.Repositories;
using Signawel.Domain.ReportGroups;
using Signawel.Dto.ReportGroup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signawel.Data.Tests.RepositoriesTests
{
    [TestFixture]
    public class ReportGroupRepositoryTests
    {
        private ReportGroupRepository _repo;
        private SignawelDbContext _context;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _context = SignawelDbContextBuilder.GetDatabaseContext();

            _repo = new ReportGroupRepository(_context, _mockMapper.Object);
        }


        [Test]
        public async Task SetReportGroupShouldReturnNullWhenDuplicateReportGroupIsGiven()
        {
            // Arrange
            var report = new ReportGroup();

            _context.ReportGroups.Add(report);
            _context.SaveChanges();

            _mockMapper
                .Setup(m => m.Map<City>(It.IsAny<CityCreationRequestDto>()))
                .Returns(new City());

            _mockMapper
                .Setup(m => m.Map<Email>(It.IsAny<EmailCreationRequestDto>()))
                .Returns(new Email());

            _mockMapper
                .Setup(m => m.Map<List<ReportGroupResponseDto>>(It.IsAny<List<ReportGroup>>()))
                .Returns(new List<ReportGroupResponseDto>());

            // Act
            var result = await _repo.SetReportGroup(new ReportGroupCreationRequestDto());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SetReportGroupShouldReturnReportGroupResponseDtoWhenReportGroupIsGiven()
        {
            // Arrange
            _mockMapper
                .Setup(m => m.Map<City>(It.IsAny<CityCreationRequestDto>()))
                .Returns(new City());

            _mockMapper
                .Setup(m => m.Map<Email>(It.IsAny<EmailCreationRequestDto>()))
                .Returns(new Email());

            _mockMapper
                .Setup(m => m.Map<List<ReportGroupResponseDto>>(It.IsAny<List<ReportGroup>>()))
                .Returns(new List<ReportGroupResponseDto>());

            _mockMapper
                .Setup(m => m.Map<ReportGroupResponseDto>(It.IsAny<ReportGroup>()))
                .Returns(new ReportGroupResponseDto());

            // Act
            var result = await _repo.SetReportGroup(new ReportGroupCreationRequestDto());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ReportGroupResponseDto>());
        }

        [Test]
        public void GetReportGroupByIdShouldReturnNullWhenTheIdDoesnNotExist()
        {
            // Arrange

            // Act
            var result = _repo.GetReportGroupById(It.IsAny<string>());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetReportGroupByIdShouldReturnReportGroupWhenTheIdDoesExist()
        {
            // Arrange

            var report = new ReportGroup();

            _context.ReportGroups.Add(report);
            _context.SaveChanges();

            _mockMapper
                .Setup(m => m.Map<ReportGroupResponseDto>(It.IsAny<ReportGroup>()))
                .Returns(new ReportGroupResponseDto());

            // Act
            var result = _repo.GetReportGroupById(report.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ReportGroupResponseDto>());
        }

        [Test]
        public void GetReportGroupsShouldReturnReportGroupResponseDtoList()
        {
            // Arrange
            var report = new ReportGroup();

            _context.ReportGroups.Add(report);
            _context.SaveChanges();

            _mockMapper
                .Setup(m => m.Map<List<ReportGroupResponseDto>>(It.IsAny<List<ReportGroup>>()))
                .Returns(new List<ReportGroupResponseDto>());

            // Act
            var result = _repo.GetReportGroups("null","null");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<ReportGroupResponseDto>>());
        }

        [Test]
        public void GetReportGroups_ShouldReturnReportGroupResponseDtoListWithCorrectEmails_WhenEmailParameterIsGiven()
        {
            // Arrange
            List<ReportGroupResponseDto> data = new List<ReportGroupResponseDto>();
            var report = new ReportGroupResponseDto();
            var report2 = new ReportGroupResponseDto();
            var email = new EmailResponseDto();
            var email2 = new EmailResponseDto();
            email.EmailAddress = "test@test.com";
            email2.EmailAddress = "what@gmail.com";
            report.EmailReportGroups.Add(email);
            report2.EmailReportGroups.Add(email2);
            data.Add(report);
            data.Add(report2);

            _mockMapper
                .Setup(m => m.Map<List<ReportGroupResponseDto>>(It.IsAny<List<ReportGroup>>()))
                .Returns(data);

            // Act
            var result = _repo.GetReportGroups("null", "test");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<ReportGroupResponseDto>>());
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetReportGroups_ShouldReturnReportGroupResponseDtoListWithCorrectCities_WhenCityParameterIsGiven()
        {
            // Arrange
            List<ReportGroupResponseDto> data = new List<ReportGroupResponseDto>();
            var report = new ReportGroupResponseDto();
            var report2 = new ReportGroupResponseDto();
            var city = new CityResponseDto();
            var city2 = new CityResponseDto();
            city.Name = "hasselt";
            city2.Name = "beringen";
            report.CityReportGroups.Add(city);
            report2.CityReportGroups.Add(city2);
            data.Add(report);
            data.Add(report2);

            _mockMapper
                .Setup(m => m.Map<List<ReportGroupResponseDto>>(It.IsAny<List<ReportGroup>>()))
                .Returns(data);

            // Act
            var result = _repo.GetReportGroups("ha", "null");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<ReportGroupResponseDto>>());
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllCitiesShouldReturnCityResponseDtoList()
        {
            // Arrange
            var city = new City();

            _context.Cities.Add(city);
            _context.SaveChanges();

            _mockMapper
                .Setup(m => m.Map<CityResponseDto>(It.IsAny<City>()))
                .Returns(new CityResponseDto());

            // Act
            var result = _repo.GetAllCities();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<CityResponseDto>>());
        }

        [Test]
        public void DeleteReportGroup_ShouldReturnNull_WhenTheIdDoesNotExist()
        {
            // Arrange
            var report = new ReportGroup();

            _context.ReportGroups.Add(report);
            _context.SaveChanges();


            // Act
            var result = _repo.DeleteReportGroup(It.IsAny<string>());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void DeleteReportGroup_ShouldReturnReportGroupResponseDto_WhenTheIdDoesExist()
        {
            // Arrange
            var report = new ReportGroup();

            _context.ReportGroups.Add(report);
            _context.SaveChanges();

            _mockMapper
                .Setup(m => m.Map<ReportGroupResponseDto>(It.IsAny<ReportGroup>()))
                .Returns(new ReportGroupResponseDto());


            // Act
            var result = _repo.DeleteReportGroup(report.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ReportGroupResponseDto>());
        }

    }
}
