using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Dto.RoadworkSchema;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class RoadworkSchemaServiceTests
    {
        private RoadworkSchemaService _service;
        private SignawelDbContext _context;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _context = SignawelDbContextBuilder.GetDatabaseContext();
            _mapper = new Mock<IMapper>();
            
            _service = new RoadworkSchemaService(_context, _mapper.Object);
        }

        #region CreateRoadworkSchema

        [Test]
        public async Task CreateRoadworkSchema_ShouldReturnSuccess_WhenEverythingIsCorrect()
        {
            // Arrange
            _mapper.Setup(_ => _.Map<RoadworkSchema>(It.IsAny<RoadworkSchemaCreationRequestDto>()))
                .Returns(new RoadworkSchema());

            _mapper.Setup(_ => _.Map<RoadworkSchemaResponseDto>(It.IsAny<RoadworkSchema>()))
                .Returns(new RoadworkSchemaResponseDto());

            // Act
            var result = await _service.CreateRoadworkSchema(new RoadworkSchemaCreationRequestDto(), "test");
            
            // Assert
            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public async Task CreateRoadworkSchema_ShouldReturnParameterEmptyError_WhenParametersAreEmpty()
        {
            // Arrange
            
            
            // Act
            var result = await 
                _service.CreateRoadworkSchema(It.IsAny<RoadworkSchemaCreationRequestDto>(), It.IsAny<string>());

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.ParameterEmptyError), Is.True);
        }

        #endregion

        #region DeleteRoadworkSchema

        [Test]
        public async Task DeleteRoadworkSchema_ShouldReturnParameterEmptyError_WhenIdIsNull()
        {
            // Arrange
            
            
            // Act
            var result = await _service.DeleteRoadworkSchema(It.IsAny<string>());

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.ParameterEmptyError), Is.True);
        }

        [Test]
        public async Task DeleteRoadworkSchema_ShouldReturnNotFoundError_WhenRoadworkIsNotFound()
        {
            // Arrange
            
            
            // Act
            var result = await _service.DeleteRoadworkSchema(Guid.NewGuid().ToString());
            
            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.NotFoundError), Is.True);
        }
        #endregion
    }
}