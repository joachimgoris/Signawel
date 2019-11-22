using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto;
using Signawel.Dto.RoadworkSchema;
using System.Threading.Tasks;

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class RoadworkSchemaControllerTests
    {
        private RoadworkSchemaController _controller;
        private Mock<IRoadworkSchemaService> _mockRoadworkSchemaService;

        [SetUp]
        public void Setup()
        {
            _mockRoadworkSchemaService = new Mock<IRoadworkSchemaService>();
            _controller = new RoadworkSchemaController(_mockRoadworkSchemaService.Object);
        }

        #region GetRoadworkSchema

        [Test]
        public async Task GetRoadworkSchemaShouldReturnOkWhenValidIdIsGiven()
        {
            // Arrange
            _mockRoadworkSchemaService.Setup(service => service.GetRoadworkSchema(It.IsAny<string>()))
                .ReturnsAsync(new RoadworkSchemaResponseDto());

            // Act
            var result = await _controller.GetRoadworkSchema(It.IsAny<string>()) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(result.Value, Is.TypeOf<RoadworkSchemaResponseDto>());
        }

        [Test]
        public async Task GetRoadworkSchemaShouldReturnNotFoundWhenInvalidIdIsGiven()
        {
            // Arrange
            _mockRoadworkSchemaService.Setup(service => service.GetRoadworkSchema(It.IsAny<string>()))
                .ReturnsAsync((RoadworkSchemaResponseDto)null);

            // Act
            var result = await _controller.GetRoadworkSchema(It.IsAny<string>());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        #endregion

        #region AddRoadworkSchema

        [Test]
        public async Task AddRoadWorkSchemaShouldReturnOkWhenValidDtoIsGiven()
        {
            // Arrange
            _mockRoadworkSchemaService.Setup(service => service.CreateRoadworkSchema(It.IsAny<RoadworkSchemaCreationRequestDto>()))
                .ReturnsAsync(new RoadworkSchemaResponseDto());

            // Act
            var result = await _controller.AddRoadworkSchema(It.IsAny<RoadworkSchemaCreationRequestDto>()) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(result.Value, Is.TypeOf<RoadworkSchemaResponseDto>());

            _mockRoadworkSchemaService.Verify(service => service.CreateRoadworkSchema(It.IsAny<RoadworkSchemaCreationRequestDto>()), Times.Once);
        }

        [Test]
        public async Task AddRoadworkSchemaShouldReturnBadRequestWhenInvalidDtoIsGiven()
        {
            // Arrange
            

            // Act
            var result = await _controller.AddRoadworkSchema(It.IsAny<RoadworkSchemaCreationRequestDto>());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<BadRequestResult>());

            _mockRoadworkSchemaService.Verify(service => service.CreateRoadworkSchema(It.IsAny<RoadworkSchemaCreationRequestDto>()), Times.Once);
        }

        #endregion

        #region PutRoadworkSchema

        [Test]
        public async Task PutRoadworkSchemaShouldReturnOkWhenValidDtoIsGiven()
        {
            // Arrange
            _mockRoadworkSchemaService.Setup(s => s.PutRoadworkSchema(It.IsAny<string>(), It.IsAny<RoadworkSchemaPutRequestDto>())).ReturnsAsync(new RoadworkSchemaResponseDto());

            // Act
            var result = await _controller.PutRoadworkSchema(It.IsAny<string>(), It.IsAny<RoadworkSchemaPutRequestDto>()) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(result.Value, Is.TypeOf<RoadworkSchemaResponseDto>());

            _mockRoadworkSchemaService.Verify(service => service.PutRoadworkSchema(It.IsAny<string>(), It.IsAny<RoadworkSchemaPutRequestDto>()), Times.Once);
        }

        [Test]
        public async Task PutRoadworkSchemaShouldReturnBadRequestWhenInvalidDtoIsGiven()
        {
            // Arrange


            // Act
            var result = await _controller.PutRoadworkSchema(It.IsAny<string>(), It.IsAny<RoadworkSchemaPutRequestDto>());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<BadRequestResult>());

            _mockRoadworkSchemaService.Verify(service => service.PutRoadworkSchema(It.IsAny<string>(), It.IsAny<RoadworkSchemaPutRequestDto>()), Times.Once);
        }

        #endregion

        #region DeleteRoadworkSchema

        [Test]
        public async Task DeleteRoadworkSchemaShouldReturnNoContentWhenValidIdIsGiven()
        {
            // Arrange
            _mockRoadworkSchemaService.Setup(service => service.DeleteRoadworkSchema(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteRoadworkSchema(It.IsAny<string>());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<NoContentResult>());

            _mockRoadworkSchemaService.Verify(service => service.DeleteRoadworkSchema(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task DeleteRoadworkSchemaShouldReturnBadRequestWhenInvalidIdIsGiven()
        {
            // Arrange


            // Act
            var result = await _controller.DeleteRoadworkSchema(It.IsAny<string>());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<BadRequestResult>());

            _mockRoadworkSchemaService.Verify(service => service.DeleteRoadworkSchema(It.IsAny<string>()), Times.Once);
        }

        #endregion
    }
}
