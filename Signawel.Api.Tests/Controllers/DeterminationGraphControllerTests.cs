using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Api.Tests.Builders.Dtos;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto.Determination;

namespace Signawel.Api.Tests.Controllers
{
    public class DeterminationGraphControllerTests
    {
        private DeterminationGraphController _determinationGraphController;
        private Mock<IDeterminationService> _determinationServiceMock;

        [SetUp]
        public void Setup()
        {
            _determinationServiceMock = new Mock<IDeterminationService>();
            _determinationGraphController = new DeterminationGraphController(_determinationServiceMock.Object);
        }

        [Test]
        public void GetDeterminationGraphShouldReturnOkObject()
        {
            // Arrange
            var determinationGraphResponseDto = new DeterminationGraphResponseDto();

            _determinationServiceMock.Setup(service => service.GetGraphAsync())
                .ReturnsAsync(determinationGraphResponseDto);

            // Act
            var result = _determinationGraphController.GetDeterminationGraph().Result as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(determinationGraphResponseDto));
            _determinationServiceMock.Verify(service => service.GetGraphAsync(), Times.Once);
        }

        [Test]
        public void SetDeterminationGraphWithValidDtoShouldReturnOkObject()
        {
            // Arrange
            var determinationGraphRequestDto = new DeterminationGraphCreationRequestDtoBuilder().WithStart().Build();
            var determinationGraphResponseDto = new DeterminationGraphResponseDto();

            _determinationServiceMock.Setup(service => service.SetGraphAsync(It.IsAny<DeterminationGraphCreationRequestDto>()))
                .ReturnsAsync(determinationGraphResponseDto);

            // Act
            var result = _determinationGraphController.SetDeterminationGraph(determinationGraphRequestDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            _determinationServiceMock.Verify(service => 
                service.SetGraphAsync(determinationGraphRequestDto), Times.Once);
        }
    }
}
