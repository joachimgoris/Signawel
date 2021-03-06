﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.Authentication;
using Signawel.Dto.ReportGroup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class ReportGroupControllerTests
    {

        private ReportGroupController _sutReportGroupController;
        private Mock<IReportGroupService> _iReportGroupServiceMock;
        private Mock<IUserService> _iUserServiceMock;

        [SetUp]
        public void SetUp()
        {
            _iReportGroupServiceMock = new Mock<IReportGroupService>();
            _iUserServiceMock = new Mock<IUserService>();
            _sutReportGroupController = new ReportGroupController(_iReportGroupServiceMock.Object,_iUserServiceMock.Object);
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
            _iReportGroupServiceMock.Setup(repo => repo.GetReportGroupsAsync("null","null","null")).ReturnsAsync(new DataResult<List<ReportGroupResponseDto>>
            {
                Entity = new List<ReportGroupResponseDto>()
            });

            var response = await _sutReportGroupController.GetReportGroupsByParameters("null","null","null") as OkObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.GetReportGroupsAsync("null","null","null"), Times.Once);
            Assert.That(response?.Value, Is.TypeOf<List<ReportGroupResponseDto>>());
        }

        [Test]
        public async Task GetReportGroupById_ShouldReturnNotFound_WhenReportGroupIdIsNotFoundAsync()
        {
            _iReportGroupServiceMock.Setup(repo => repo.GetReportGroupByIdAsync(It.IsAny<string>())).ReturnsAsync(DataResult<ReportGroupResponseDto>.WithPublicError(ErrorCodes.NotFoundError, "Report group not found."));

            var response = await _sutReportGroupController.GetReportGroupById(It.IsAny<string>()) as NotFoundObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.GetReportGroupByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.That(response, Is.TypeOf<NotFoundObjectResult>());
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
            Assert.That(response, Is.TypeOf<BadRequestObjectResult>());
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
            Assert.That(response, Is.TypeOf<NotFoundObjectResult>());
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

        [Test]
        public async Task ModifyReportGroup_ShouldReturnBadRequest_WhenReportGroupIsEmpty()
        {
            _iReportGroupServiceMock.Setup(repo => repo.ModifyReportGroupAsync(It.IsAny<string>(),(ReportGroupCreationRequestDto)null)).ReturnsAsync(DataResult<ReportGroupResponseDto>.WithPublicError(ErrorCodes.InvalidOperationError, "The given Dto is empty."));

            var response = await _sutReportGroupController.ModifyReportGroup(null, (ReportGroupCreationRequestDto) null) as BadRequestObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.ModifyReportGroupAsync(It.IsAny<string>(), (ReportGroupCreationRequestDto)null), Times.Once);
            Assert.That(response, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task ModifyReportGroup_ShouldReturnReportGroupResponseDto_WhenReportGroupIsNotEmpty()
        {
            _iReportGroupServiceMock.Setup(repo => repo.ModifyReportGroupAsync(It.IsAny<string>(), It.IsAny<ReportGroupCreationRequestDto>())).ReturnsAsync(new DataResult<ReportGroupResponseDto>
            {
                Entity = new ReportGroupResponseDto()
            });

            var response = await _sutReportGroupController.ModifyReportGroup(null, new ReportGroupCreationRequestDto()) as OkObjectResult;

            _iReportGroupServiceMock.Verify(repo => repo.ModifyReportGroupAsync(It.IsAny<string>(), It.IsAny<ReportGroupCreationRequestDto>() ), Times.Once);
            Assert.That(response?.Value, Is.TypeOf<ReportGroupResponseDto>());
        }

        [Test]
        public async Task GetUsers_ShouldReturnListOfUserResponseDtos()
        {
            var result = new DataResult<ICollection<UserResponseDto>>
            {
                Entity = new Collection<UserResponseDto>()
            };

            _iUserServiceMock.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(result);


            var response = await _sutReportGroupController.GetUsers("") as OkObjectResult;

            _iUserServiceMock.Verify(repo => repo.GetAllUsersAsync(), Times.Once);
            Assert.That(response?.Value, Is.TypeOf<List<UserResponseDto>>());
        }
    }
}
