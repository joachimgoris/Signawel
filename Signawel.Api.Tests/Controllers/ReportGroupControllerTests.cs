using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.ReportGroup;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class ReportGroupControllerTests
    {

        private ReportGroupController _sutReportGroupController;
        private Mock<IReportGroupService> _iReportGroupServiceMock;

        [SetUp]
        public void SetUp()
        {
            _iReportGroupServiceMock = new Mock<IReportGroupService>();
            _sutReportGroupController = new ReportGroupController(_iReportGroupServiceMock.Object);
        }

        [Test]
        public async Task GetAllCities_ShouldReturnAllCitiesAsync()
        {
            _iReportGroupServiceMock.Setup(repo => repo.GetAllCitiesAsync()).ReturnsAsync(new DataResult<List<CityResponseDto>>
            {
                Entity = new List<CityResponseDto>()
            });

            var response = await _sutReportGroupController.GetAllCities() as OkObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.GetAllCitiesAsync(), Times.Once);
            Assert.That(response?.Value, Is.TypeOf<List<CityResponseDto>>());
        }

        [Test]
        public async Task GetReportGroupsByParameters_ShouldReturnReportGroupsAsync()
        {
            _iReportGroupServiceMock.Setup(repo => repo.GetReportGroupsAsync("null","null")).ReturnsAsync(new DataResult<List<ReportGroupResponseDto>>
            {
                Entity = new List<ReportGroupResponseDto>()
            });

            var response = await _sutReportGroupController.GetReportGroupsByParameters("null","null") as OkObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.GetReportGroupsAsync("null","null"), Times.Once);
            Assert.That(response?.Value, Is.TypeOf<List<ReportGroupResponseDto>>());
        }

        [Test]
        public async Task GetReportGroupById_ShouldReturnNotFound_WhenReportGroupIdIsNotFoundAsync()
        {
            _iReportGroupServiceMock.Setup(repo => repo.GetReportGroupByIdAsync(It.IsAny<string>())).ReturnsAsync(DataResult<ReportGroupResponseDto>.WithPublicError(ErrorCodes.NotFoundError, "Report group not found."));

            var response = await _sutReportGroupController.GetReportGroupById(It.IsAny<string>()) as NotFoundObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.GetReportGroupByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.That(response?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task GetReportGroupById_ShouldReturnReportGroup_WhenReportGroupIsFoundAsync()
        {
            var id = Guid.NewGuid().ToString();
            _iReportGroupServiceMock.Setup(repo => repo.GetReportGroupByIdAsync(id)).ReturnsAsync(new DataResult<ReportGroupResponseDto>
            {
                Entity = new ReportGroupResponseDto()
            });

            var response = await _sutReportGroupController.GetReportGroupById(id) as OkObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.GetReportGroupByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.That(response?.Value, Is.TypeOf<ReportGroupResponseDto>());
        }

        [Test]
        public async Task UploadReportGroup_ShouldReturnBadRequest_WhenReportGroupAlreadyExistsAsync()
        {
            var reportGroup = new ReportGroupCreationRequestDto();
            _iReportGroupServiceMock.Setup(repo => repo.SetReportGroupAsync(reportGroup)).ReturnsAsync(DataResult<ReportGroupResponseDto>.WithPublicError(ErrorCodes.InvalidOperationError, "Report group already existing."));

            var response = await _sutReportGroupController.AddReportGroup(reportGroup) as BadRequestObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.SetReportGroupAsync(reportGroup), Times.Once);
            Assert.That(response?.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task UploadReportGroup_ShouldReturnReportGroupResponseDto_WhenReportGroupIsNewAsync()
        {
            var reportGroup = new ReportGroupCreationRequestDto();
            _iReportGroupServiceMock.Setup(repo => repo.SetReportGroupAsync(reportGroup)).ReturnsAsync(new DataResult<ReportGroupResponseDto>
            {
                Entity = new ReportGroupResponseDto()
            });

            var response = await _sutReportGroupController.AddReportGroup(reportGroup) as OkObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.SetReportGroupAsync(reportGroup), Times.Once);
            Assert.That(response?.Value, Is.TypeOf<ReportGroupResponseDto>());
        }

        [Test]
        public async Task DeleteReportGroup_ShouldReturnNotFound_WhenIdDoesNotExistsAsync()
        {
            _iReportGroupServiceMock.Setup(repo => repo.DeleteReportGroupAsync(It.IsAny<string>())).ReturnsAsync(DataResult<ReportGroupResponseDto>.WithPublicError(ErrorCodes.NotFoundError,
                    "Report group not found."));

            var response = await _sutReportGroupController.DeleteReportGroup(It.IsAny<string>()) as NotFoundObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.DeleteReportGroupAsync(It.IsAny<string>()), Times.Once);
            Assert.That(response?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task DeleteReportGroup_ShouldReturnReportGroupResponseDto_WhenIdExistsAsync()
        {
            _iReportGroupServiceMock.Setup(repo => repo.DeleteReportGroupAsync(It.IsAny<string>())).ReturnsAsync(new DataResult<ReportGroupResponseDto>
            {
                Entity = new ReportGroupResponseDto()
            });

            var response = await _sutReportGroupController.DeleteReportGroup(It.IsAny<string>()) as OkObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.DeleteReportGroupAsync(It.IsAny<string>()), Times.Once);
            Assert.That(response?.Value, Is.TypeOf<DataResult<ReportGroupResponseDto>>());
        }
    }
}
