using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.Api.Tests.Builders;
using Signawel.API.Controllers;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.DataResults;
using Signawel.Dto;
using Signawel.Dto.RoadworkSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class RoadworkSchemaControllerTests
    {
        private RoadworkSchemaController _controller;
        private Mock<IRoadworkSchemaService> _mockRoadworkSchemaService;
        private Mock<IImageService> _mockImageService;

        [SetUp]
        public void Setup()
        {
            _mockRoadworkSchemaService = new Mock<IRoadworkSchemaService>();
            _mockImageService = new Mock<IImageService>();
            _controller = new RoadworkSchemaController(_mockRoadworkSchemaService.Object, _mockImageService.Object);
        }

        #region GetRoadworkSchema

        [Test]
        public async Task GetRoadworkSchemaShouldReturnOkWhenValidIdIsGiven()
        {
            // Arrange
            _mockRoadworkSchemaService.Setup(service => service.GetRoadworkSchema(It.IsAny<string>()))
                .ReturnsAsync(DataResult<RoadworkSchemaResponseDto>.Success(new RoadworkSchemaResponseDto()));

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
                .ReturnsAsync(DataResult<RoadworkSchemaResponseDto>.WithError("", ""));

            // Act
            var result = await _controller.GetRoadworkSchema(It.IsAny<string>()) as NotFoundObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());
        }

        #endregion

        #region AddRoadworkSchema

        [Test]
        public async Task AddRoadworkSchema_ShouldReturnBadRequest_WhenNoImageIsPresent()
        {
            // Arrange
            var dto = new RoadworkSchemaCreationRequestDto();
            var fileList = new List<IFormFile>();
            
            // Act
            var result = await _controller.AddRoadworkSchema(dto, fileList) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errorList = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errorList.Any(error => error.Code.Equals("ImageRequired")), Is.True);
        }

        [Test]
        public async Task AddRoadworkSchema_ShouldReturnBadRequest_WhenImageSizeIsNull()
        {
            // Arrange
            var dto = new RoadworkSchemaCreationRequestDto();
            var fileList = new List<IFormFile>() {
                new FormFileBuilder()
                    .Empty()
                    .WithFileName("test.png")
                    .Build()
            };
            
            // Act
            var result = await _controller.AddRoadworkSchema(dto, fileList) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errorList = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errorList.Any(error => error.Code.Equals("ImageRequired")), Is.True);
        }

        [Test]
        public async Task AddRoadworkSchema_ShouldReturnBadRequest_FileIsNotImageFormat()
        {
            // Arrange
            var dto = new RoadworkSchemaCreationRequestDto();
            var fileList = new List<IFormFile>() {
                new FormFileBuilder()
                    .WithContent("This is a test file")
                    .WithFileName("test.test")
                    .Build()
            };
            
            // Act
            var result = await _controller.AddRoadworkSchema(dto, fileList) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errorList = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errorList.Any(error => error.Code.Equals("InvalidFileFormat")), Is.True);
        }

        [Test]
        public async Task AddRoadworkSchema_ShouldReturnBadRequest_WhenFailedToAddImageToDatabase()
        {
            // Arrange
            var dto = new RoadworkSchemaCreationRequestDto();

            var fileList = new List<IFormFile>() {
                GetImageFormFile()
            };

            _mockImageService
                .Setup(service => service.AddImage(It.IsAny<MemoryStream>()))
                .ReturnsAsync(DataResult<string>.WithError("FailedToSaveImage", "Failed to add image to database", DataErrorVisibility.Public));
            
            // Act
            var result = await _controller.AddRoadworkSchema(dto, fileList) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errorList = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errorList.Any(error => error.Code.Equals("FailedToSaveImage")), Is.True);
        }

        [Test]
        public async Task AddRoadworkSchema_ShouldReturnBadRequest_WhenFailedToCreateRoadworkSchema()
        {
            // Arrange
            var dto = new RoadworkSchemaCreationRequestDto();

            var fileList = new List<IFormFile>() {
                GetImageFormFile()
            };

            _mockImageService
                .Setup(service => service.AddImage(It.IsAny<MemoryStream>()))
                .ReturnsAsync(DataResult<string>.Success(Guid.NewGuid().ToString()));

            _mockRoadworkSchemaService
                .Setup(service => service.CreateRoadworkSchema(It.IsAny<RoadworkSchemaCreationRequestDto>(), It.IsAny<string>()))
                .ReturnsAsync(DataResult<RoadworkSchemaResponseDto>.WithError("RoadworkSchemaCreation", "Failed to create roadworkschema.", DataErrorVisibility.Public));
            
            // Act
            var result = await _controller.AddRoadworkSchema(dto, fileList) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errorList = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errorList.Any(error => error.Code.Equals("RoadworkSchemaCreation")), Is.True);

            _mockImageService.Verify(service => service.DeleteImage(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task AddRoadworkSchema_ShouldReturnOk_WhenEverythingIsCorrect()
        {
            // Arrange
            var dto = new RoadworkSchemaCreationRequestDto();

            var fileList = new List<IFormFile>() {
                GetImageFormFile()
            };

            _mockImageService
                .Setup(service => service.AddImage(It.IsAny<MemoryStream>()))
                .ReturnsAsync(DataResult<string>.Success(Guid.NewGuid().ToString()));

            _mockRoadworkSchemaService
                .Setup(service => service.CreateRoadworkSchema(It.IsAny<RoadworkSchemaCreationRequestDto>(), It.IsAny<string>()))
                .ReturnsAsync(DataResult<RoadworkSchemaResponseDto>.Success(new RoadworkSchemaResponseDto()));
            
            // Act
            var result = await _controller.AddRoadworkSchema(dto, fileList) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<RoadworkSchemaResponseDto>());

            _mockImageService.Verify(service => service.DeleteImage(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region PutRoadworkSchema
        
        // with file length 0
        // with file invalid format
        // with file failed to save
        // with file update failed
        // with file update success

        [Test]
        public async Task PutRoadworkSchema_ShouldReturnBadRequest_WhenFileSizeIsNull()
        {
            // Arrange
            var dto = new RoadworkSchemaPutRequestDto();
            var fileList = new List<IFormFile>() {
                new FormFileBuilder().Empty().Build()
            };
            
            // Act
            var result = await _controller.PutRoadworkSchema(Guid.NewGuid().ToString(), dto, fileList) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errorList = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errorList.Any(error => error.Code.Equals("FileInvalid")), Is.True);
        }

        [Test]
        public async Task PutRoadworkSchema_ShouldReturnBadRequest_WhenFileFormatIsInvalid()
        {
            // Arrange
            var dto = new RoadworkSchemaPutRequestDto();
            var fileList = new List<IFormFile>() {
                new FormFileBuilder()
                    .WithContent("This is a test file")
                    .WithFileName("test.test")
                    .Build()
            };
            
            // Act
            var result = await _controller.PutRoadworkSchema(Guid.NewGuid().ToString(), dto, fileList) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errorList = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errorList.Any(error => error.Code.Equals("InvalidFileFormat")), Is.True);
        }

        [Test]
        public async Task PutRoadworkSchema_ShouldReturnBadRequest_WhenImageFailedToUpdate()
        {
            // Arrange
            var dto = new RoadworkSchemaPutRequestDto();
            var fileList = new List<IFormFile>() {
                GetImageFormFile()
            };

            _mockImageService
                .Setup(service => service.UpdateImage(It.IsAny<string>(), It.IsAny<MemoryStream>()))
                .ReturnsAsync(DataResult<string>.WithError("NoImageFound", "", DataErrorVisibility.Public));
            
            // Act
            var result = await _controller.PutRoadworkSchema(Guid.NewGuid().ToString(), dto, fileList) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errorList = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errorList.Any(error => error.Code.Equals("NoImageFound")), Is.True);
        }

        [Test]
        public async Task PutRoadworkSchema_ShouldReturnBadRequest_WhenSchemaFailedToUpdate()
        {
            // Arrange
            var dto = new RoadworkSchemaPutRequestDto();
            var fileList = new List<IFormFile>() {
                GetImageFormFile()
            };

            _mockImageService
                .Setup(service => service.UpdateImage(It.IsAny<string>(), It.IsAny<MemoryStream>()))
                .ReturnsAsync(DataResult<string>.Success(""));

            _mockRoadworkSchemaService
                .Setup(service => service.PutRoadworkSchema(It.IsAny<string>(), It.IsAny<RoadworkSchemaPutRequestDto>()))
                .ReturnsAsync(DataResult<RoadworkSchemaResponseDto>.WithError("RoadworkSchemaNotFound", "", DataErrorVisibility.Public));
            
            // Act
            var result = await _controller.PutRoadworkSchema(Guid.NewGuid().ToString(), dto, fileList) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            var errorList = (IList<ErrorResponseDto>) result.Value;

            Assert.That(errorList.Any(error => error.Code.Equals("RoadworkSchemaNotFound")), Is.True);
        }

        [Test]
        public async Task PutRoadworkSchema_ShouldReturnOk_WhenSchemaUpdated()
        {
            // Arrange
            var dto = new RoadworkSchemaPutRequestDto();
            var fileList = new List<IFormFile>() {
                GetImageFormFile()
            };

            _mockImageService
                .Setup(service => service.UpdateImage(It.IsAny<string>(), It.IsAny<MemoryStream>()))
                .ReturnsAsync(DataResult<string>.Success(""));

            _mockRoadworkSchemaService
                .Setup(service => service.PutRoadworkSchema(It.IsAny<string>(), It.IsAny<RoadworkSchemaPutRequestDto>()))
                .ReturnsAsync(DataResult<RoadworkSchemaResponseDto>.Success(new RoadworkSchemaResponseDto()));
            
            // Act
            var result = await _controller.PutRoadworkSchema(Guid.NewGuid().ToString(), dto, fileList) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<RoadworkSchemaResponseDto>());
        }

        #endregion

        #region DeleteRoadworkSchema

        [Test]
        public async Task DeleteRoadworkSchemaShouldReturnNoContentWhenValidIdIsGiven()
        {
            // Arrange
            _mockRoadworkSchemaService.Setup(service => service.DeleteRoadworkSchema(It.IsAny<string>()))
                .ReturnsAsync(DataResult.Success);

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
            _mockRoadworkSchemaService.Setup(service => service.DeleteRoadworkSchema(It.IsAny<string>()))
                .ReturnsAsync(DataResult.WithError("NotFound", ""));

            // Act
            var result = await _controller.DeleteRoadworkSchema(It.IsAny<string>()) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<ErrorResponseDto>>());

            _mockRoadworkSchemaService.Verify(service => service.DeleteRoadworkSchema(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region Helpers

        private IList<IFormFile> GetFormFileList()
        {
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return new List<IFormFile> {
                fileMock.Object
            };
        }

        private IFormFile GetImageFormFile()
        {
            var stream = new MemoryStream(File.ReadAllBytes(@"Assets/image.png"));
            return new FormFile(stream, 0, stream.Length, "image", "image.png");
        }

        #endregion Helpers
    }
}
