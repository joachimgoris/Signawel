using AutoMapper;
using Moq;
using NUnit.Framework;
using Signawel.Business.Services;
using Signawel.Data.Abstractions.Repositories;
using Signawel.Domain.Determination;
using Signawel.Dto.Determination;
using System.Threading.Tasks;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class DeterminationServiceTests
    {
        private DeterminationService _service;
        private Mock<IDeterminationRepository> _repository;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IDeterminationRepository>();
            _mapper = new Mock<IMapper>();

            _service = new DeterminationService(_repository.Object, _mapper.Object);
        }

        [Test]
        public async Task GetGraphAsync_ShouldReturnDeterminationGraphResponseDto()
        {
            // Arrange
            _repository
                .Setup(_ => _.GetGraphAsync())
                .ReturnsAsync(new DeterminationGraph());

            _mapper
                .Setup(m => m.Map<DeterminationGraphResponseDto>(It.IsAny<DeterminationGraph>()))
                .Returns(new DeterminationGraphResponseDto());

            // Act
            var result = await _service.GetGraphAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DeterminationGraphResponseDto>());

            _repository.Verify(_ => _.GetGraphAsync(), Times.Once);
        }

        [Test]
        public async Task SetGraphAsync_ShouldReturnDeterminationGraphResponseDto()
        {
            // Arrange
            _mapper
                .Setup(m => m.Map<DeterminationGraph>(It.IsAny<DeterminationGraphCreationRequestDto>()))
                .Returns(new DeterminationGraph());

            _repository
               .Setup(_ => _.GetGraphAsync())
               .ReturnsAsync(new DeterminationGraph());

            _mapper
                .Setup(m => m.Map<DeterminationGraphResponseDto>(It.IsAny<DeterminationGraph>()))
                .Returns(new DeterminationGraphResponseDto());

            // Act
            var result = await _service.SetGraphAsync(new DeterminationGraphCreationRequestDto());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DeterminationGraphResponseDto>());


        }
    }
}
