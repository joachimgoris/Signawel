using AutoMapper;
using Moq;
using NUnit.Framework;
using Signawel.Business.Builders;
using Signawel.Business.Services;
using Signawel.Business.Tests.Builders.Dtos;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Signawel.Business.Tests.Services
{
    public class CategoryServiceTests
    {
        private CategoryService _categoryService;
        private SignawelDbContext _context;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _context = SignawelDbContextBuilder.GetDatabaseContext();

            _categoryService = new CategoryService(_context, _mockMapper.Object);
        }

        [Test]
        public void GetAllCategoriesShouldReturnCategories()
        {
            // Arrange
            var categories = new List<Category>()
            {
                new CategoryBuilder().Build(),
                new CategoryBuilder().Build(),
                new CategoryBuilder().Build(),
                new CategoryBuilder().Build()
            };

            _context.Categories.AddRange(categories);
            _context.SaveChanges();

            var categoryResponseDtos = new List<CategoryResponseDto>();
            categories.ForEach(category => categoryResponseDtos.Add(new CategoryResponseDtoBuilder().FromCategory(category).Build()));

            _mockMapper
                .Setup(mapper => mapper.Map<IList<CategoryResponseDto>>(It.IsAny<IOrderedQueryable<Category>>()))
                .Returns(categoryResponseDtos);

            // Act
            var result = _categoryService.GetAllCategories();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(categories.Count));
            CollectionAssert.AreEquivalent(result, categoryResponseDtos);
        }
    }
}
