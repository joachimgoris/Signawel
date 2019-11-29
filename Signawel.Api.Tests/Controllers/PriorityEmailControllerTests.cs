

using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto;
using Signawel.Dto.PriorityEmail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class PriorityEmailControllerTests
    {

        private PriorityEmailController _controller;
        private Mock<IPriorityEmailService> _serviceMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IPriorityEmailService>();
            _controller = new PriorityEmailController(_serviceMock.Object);
        }

        [Test]
        public async Task AddPriorityEmail_ShouldReturnBadRequestIfAnErrorOccured()
        {
            // Arrange
            _serviceMock
                .Setup(_ => _.AddPriorityEmailAsync(It.IsAny<PriorityEmailCreationRequestDto>()))
                .ReturnsAsync(DataResult<PriorityEmailReponseDto>.WithPublicError(ErrorCodes.PriorityEmailCreationError, "creation failed"));

            // Act
            var result = await _controller.AddPriorityEmail(new PriorityEmailCreationRequestDto()) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());
            Assert.That(result.Value, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task AddPriorityEmail_ShouldReturnOk_IfPriorityEmailAdded()
        {
            // Arrange
            _serviceMock
                .Setup(_ => _.AddPriorityEmailAsync(It.IsAny<PriorityEmailCreationRequestDto>()))
                .ReturnsAsync(DataResult<PriorityEmailReponseDto>.Success(new PriorityEmailReponseDto()));

            // Act
            var result = await _controller.AddPriorityEmail(new PriorityEmailCreationRequestDto()) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<PriorityEmailReponseDto>());
        }

        [Test]
        public async Task DeletePriorityEmail_ShouldReturnBadRequest_IfPriorityEmailFailedToBeDeleted()
        {
            // Arrange
            _serviceMock
                .Setup(_ => _.RemovePriorityEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(DataResult.WithPublicError(ErrorCodes.PriorityEmailDeletionError, "Failed to delete"));

            // Act
            var result = await _controller.DeletePriorityEmail(Guid.NewGuid().ToString()) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());
            Assert.That(result.Value, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task DeletePriorityEmail_ShouldReturnOk_IfPriorityEmailDeleted()
        {
            // Arrange
            _serviceMock
                .Setup(_ => _.RemovePriorityEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(DataResult.Success);

            // Act
            var result = await _controller.DeletePriorityEmail(Guid.NewGuid().ToString()) as NoContentResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

    }
}
