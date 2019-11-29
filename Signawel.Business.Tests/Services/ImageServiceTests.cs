using Moq;
using NUnit.Framework;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Domain;
using System;
using System.IO;
using System.Threading.Tasks;
using Signawel.Domain.DataResults;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class ImageServiceTests
    {
        private ImageService _service;
        private SignawelDbContext _context;

        [OneTimeSetUp]
        public void SetUp()
        {
            _context = SignawelDbContextBuilder.GetDatabaseContext();

            _service = new ImageService(_context);
        }

        [Test]
        public async Task GetImageAsync_ShouldReturnNull_WhenTheIdDoesNotExist()
        {
            // Arrange
            

            // Act
            var result = await _service.GetImageAsync(It.IsAny<string>());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetImageAsync_ShouldReturnMemoryStream_WhenTheIdExists()
        {
            // Arrange
            var guid = Guid.NewGuid().ToString();
            var image = new Image
            {
                Id = guid,
                ImagePath = "test"
            };

            await _context.Images.AddAsync(image);

            // Act
            var result = await _service.GetImageAsync(guid);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<MemoryStream>());
        }

        [Test]
        public async Task AddImage_ShouldReturnImageResponseDto()
        {
            // Arrange
            
            // Act
            var result = await _service.AddImage(new MemoryStream(5));

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DataResult<string>>());
        }

        [Test]
        public async Task DeleteImage_DoesNothing_WhenTHeIdDoesNotExist()
        {
            // Arrange
            var guid = Guid.NewGuid().ToString();
            var image = new Image
            {
                Id = guid
            };
            await _context.Images.AddAsync(image);

            // Act
            await _service.DeleteImage(It.IsAny<string>());
            var result = await _context.Images.FindAsync(guid);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<Image>());
            Assert.That(result.Id, Is.EqualTo(guid));

        }

        [Test]
        public async Task DeleteImage_DeletesTheImage_WhenTHeIdExists()
        {
            //Arrange
            var guid = Guid.NewGuid().ToString();
            var image = new Image
            {
                Id = guid
            };
            await _context.Images.AddAsync(image);

            // Act
            await _service.DeleteImage(guid);
            var result = await _context.Images.FindAsync(guid);

            //Assert
            Assert.That(result, Is.Null);
        }
    }
}
