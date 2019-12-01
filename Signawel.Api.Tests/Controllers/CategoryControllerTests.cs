using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto;
using System.Collections.Generic;

namespace Signawel.Api.Tests.Controllers
{
    public class CategoryControllerTests
    {
        private CategoryController _categoryController;
        private Mock<ICategoryService> _categoryServiceMock;

        [SetUp]
        public void Setup()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_categoryServiceMock.Object);
        }

        [Test]
        public void GetCategoriesShouldReturnListOfCategories()
        {
            // Arrange
            IList<CategoryResponseDto> categories = new List<CategoryResponseDto>()
            {
                new CategoryResponseDto(),
                new CategoryResponseDto(),
                new CategoryResponseDto()
            };

            _categoryServiceMock.Setup(service => service.GetAllCategories()).Returns(categories);

            // Act
            var result = _categoryController.GetCategories() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(categories));

            _categoryServiceMock.Verify(service => service.GetAllCategories(), Times.Once);
        }
    }
}
