using AutoMapper;
using Moq;
using NUnit.Framework;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Domain.DataResults;
using Signawel.Domain.ReportGroups;
using Signawel.Dto.ReportGroup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class ReportGroupServiceTests
    {
        private ReportGroupService _service;
        private SignawelDbContext _context;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _context = SignawelDbContextBuilder.GetDatabaseContext();

            _service = new ReportGroupService(_context, _mockMapper.Object);
        }


        [Test]
        public async Task SetReportGroupShouldReturnErrorWhenDuplicateReportGroupIsGiven()
        {
            // Arrange
            var report = new ReportGroup();
            report.CityReportGroups = new List<CityReportGroup>();
            report.EmailReportGroups = new List<EmailReportGroup>();

            _context.ReportGroups.Add(report);
            _context.SaveChanges();

            _mockMapper
                .Setup(m => m.Map<City>(It.IsAny<CityCreationRequestDto>()))
                .Returns(new City());

            _mockMapper
                .Setup(m => m.Map<Email>(It.IsAny<EmailCreationRequestDto>()))
                .Returns(new Email());

            var list = new List<ReportGroupResponseDto>();
            var reportGroupResponse = new ReportGroupResponseDto();
            reportGroupResponse.CityReportGroups = new List<CityResponseDto>();
            reportGroupResponse.EmailReportGroups = new List<EmailResponseDto>();

            list.Add(reportGroupResponse);

            _mockMapper
                .Setup(m => m.Map<List<ReportGroupResponseDto>>(It.IsAny<List<ReportGroup>>()))
                .Returns(list);



            var reportGroup = new ReportGroupCreationRequestDto();
            reportGroup.CityReportGroups = new List<CityCreationRequestDto>();
            reportGroup.EmailReportGroups = new List<EmailCreationRequestDto>();

            // Act
            var result = await _service.SetReportGroupAsync(reportGroup);

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
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

            var reportGroup = new ReportGroupCreationRequestDto();
            reportGroup.CityReportGroups = new List<CityCreationRequestDto>();
            reportGroup.EmailReportGroups = new List<EmailCreationRequestDto>();

            // Act
            var result = await _service.SetReportGroupAsync(reportGroup);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DataResult<ReportGroupResponseDto>>());
        }

        [Test]
        public async Task GetReportGroupByIdShouldReturnErrorWhenTheIdDoesnNotExistAsync()
        {
            // Arrange

            // Act
            var result = await _service.GetReportGroupByIdAsync(It.IsAny<string>());

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
        }

        [Test]
        public async Task GetReportGroupByIdShouldReturnReportGroupWhenTheIdDoesExistAsync()
        {
            // Arrange

            var report = new ReportGroup();

            _context.ReportGroups.Add(report);
            _context.SaveChanges();

            _mockMapper
                .Setup(m => m.Map<ReportGroupResponseDto>(It.IsAny<ReportGroup>()))
                .Returns(new ReportGroupResponseDto());

            // Act
            var result = await _service.GetReportGroupByIdAsync(report.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DataResult<ReportGroupResponseDto>>());
        }

        [Test]
        public async Task GetReportGroupsShouldReturnReportGroupResponseDtoListAsync()
        {
            // Arrange
            var report = new ReportGroup();

            _context.ReportGroups.Add(report);
            _context.SaveChanges();

            _mockMapper
                .Setup(m => m.Map<List<ReportGroupResponseDto>>(It.IsAny<List<ReportGroup>>()))
                .Returns(new List<ReportGroupResponseDto>());

            // Act
            var result = await _service.GetReportGroupsAsync("null", "null");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DataResult<List<ReportGroupResponseDto>>>());
        }

        [Test]
        public async Task GetReportGroups_ShouldReturnReportGroupResponseDtoListWithCorrectEmails_WhenEmailParameterIsGivenAsync()
        {
            // Arrange
            List<ReportGroupResponseDto> data = new List<ReportGroupResponseDto>();
            var report = new ReportGroupResponseDto();
            var report2 = new ReportGroupResponseDto();
            var email = new EmailResponseDto();
            var email2 = new EmailResponseDto();
            email.EmailAddress = "test@test.com";
            email2.EmailAddress = "what@gmail.com";
            report.EmailReportGroups = new List<EmailResponseDto>();
            report2.EmailReportGroups = new List<EmailResponseDto>();
            report.EmailReportGroups.Add(email);
            report2.EmailReportGroups.Add(email2);
            data.Add(report);
            data.Add(report2);

            _mockMapper
                .Setup(m => m.Map<List<ReportGroupResponseDto>>(It.IsAny<List<ReportGroup>>()))
                .Returns(data);

            // Act
            var result = await _service.GetReportGroupsAsync("null", "test");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DataResult<List<ReportGroupResponseDto>>>());
            Assert.That(result.Entity.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetReportGroups_ShouldReturnReportGroupResponseDtoListWithCorrectCities_WhenCityParameterIsGivenAsync()
        {
            // Arrange
            List<ReportGroupResponseDto> data = new List<ReportGroupResponseDto>();
            var report = new ReportGroupResponseDto();
            var report2 = new ReportGroupResponseDto();
            var city = new CityResponseDto();
            var city2 = new CityResponseDto();
            city.Name = "hasselt";
            city2.Name = "beringen";
            report.CityReportGroups = new List<CityResponseDto>();
            report2.CityReportGroups = new List<CityResponseDto>();
            report.CityReportGroups.Add(city);
            report2.CityReportGroups.Add(city2);
            data.Add(report);
            data.Add(report2);

            _mockMapper
                .Setup(m => m.Map<List<ReportGroupResponseDto>>(It.IsAny<List<ReportGroup>>()))
                .Returns(data);

            // Act
            var result = await _service.GetReportGroupsAsync("ha", "null");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DataResult<List<ReportGroupResponseDto>>>());
            Assert.That(result.Entity.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllCitiesShouldReturnCityResponseDtoListAsync()
        {
            // Arrange
            var city = new City();

            _context.Cities.Add(city);
            _context.SaveChanges();

            _mockMapper
                .Setup(m => m.Map<CityResponseDto>(It.IsAny<City>()))
                .Returns(new CityResponseDto());

            // Act
            var result = await _service.GetAllCitiesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public async Task DeleteReportGroup_ShouldReturnNull_WhenTheIdDoesNotExistAsync()
        {
            // Arrange
            var report = new ReportGroup();

            _context.ReportGroups.Add(report);
            _context.SaveChanges();


            // Act
            var result = await _service.DeleteReportGroupAsync(It.IsAny<string>());

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
        }

        [Test]
        public async Task DeleteReportGroup_ShouldReturnReportGroupResponseDto_WhenTheIdDoesExistAsync()
        {
            // Arrange
            var report = new ReportGroup();

            _context.ReportGroups.Add(report);
            _context.SaveChanges();

            _mockMapper
                .Setup(m => m.Map<ReportGroupResponseDto>(It.IsAny<ReportGroup>()))
                .Returns(new ReportGroupResponseDto());


            // Act
            var result = await _service.DeleteReportGroupAsync(report.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Succeeded, Is.True);
        }

    }
}
