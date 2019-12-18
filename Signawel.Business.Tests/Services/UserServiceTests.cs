using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Signawel.Business.MapperProfiles;
using Signawel.Business.Services;
using Signawel.Business.Tests.Helpers.TestAsyncEnumerable;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Dto.Authentication;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _service;
        private Mock<UserManager<User>> _userManager;
        private Mock<RoleManager<Role>> _roleManager;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var logger = new Mock<ILogger<UserService>>();
            var store = new Mock<IUserStore<User>>();
            _userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var roleStore = new Mock<IRoleStore<Role>>();
            _roleManager = new Mock<RoleManager<Role>>(roleStore.Object, null, null, null, null);
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new UserProfile())));

            _service = new UserService(logger.Object, _mapper, _userManager.Object, _roleManager.Object);
        }

        #region GetUserAsync

        [Test]
        public async Task GetUserAsync_ShouldReturnParameterEmptyError_WhenIdIsEmpty()
        {
            // Arrange
            
            
            // Act
            var result = await _service.GetUserAsync(null);

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.ParameterEmptyError), Is.True);
            
            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetUserAsync_ShouldReturnNotFound_WhenNoUserIsFound()
        {
            // Arrange
            _userManager.Setup(_ => _.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((User) null);
            
            // Act
            var result = await _service.GetUserAsync("someId");

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.NotFoundError), Is.True);
            
            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetUserAsync_ShouldReturnSuccess()
        {
            // Arrange
            _userManager.Setup(_ => _.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());
            
            // Act
            var result = await _service.GetUserAsync("someId");
            
            // Assert
            Assert.That(result.Errors, Is.Empty);
            Assert.That(result.Entity, Is.InstanceOf<UserResponseDto>());
            
            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Once);
        }
        
        #endregion
        
        #region GetAllUsersAsync

        [Test]
        public void GetAllUsersAsync_ShouldReturnSuccess()
        {
            // Arrange
            _userManager.Setup(_ => _.Users)
                .Returns(new TestAsyncEnumerable<User>(new List<User>()));
            
            // Act
            var result = _service.GetAllUsersAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Succeeded, Is.True);
        }
        
        #endregion
        
        #region ModifyUserAsync

        [Test]
        public async Task ModifyUserAsync_ShouldReturnNotFound_WhenUserIsNotFound()
        {
            // Arrange
            _userManager.Setup(_ => _.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);
            
            // Act
            var result = await _service.ModifyUserAsync("someId", null);
            
            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.NotFoundError), Is.True);
            
            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task ModifyUserAsync_ShouldReturnSuccess()
        {
            // Arrange
            _userManager.Setup(_ => _.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            _userManager.Setup(_ => _.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);
                
            // Act
            var result = await _service.ModifyUserAsync("someId", new UserModifyRequestDto
            {
                Email = "someEmail@email.com",
                FirstName = "test",
                LastName = "test",
                UserName = "test"
            });
            
            // Arrange
            Assert.That(result.Succeeded, Is.True);
            
            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Once);
        }
        
        #endregion
        
        #region CreateUserAsync

        [Test]
        public async Task CreateUserAsync_ShouldReturnSuccess()
        {
            // Arrange
            _userManager.Setup(_ => _.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);
            
            // Act
            var result = await _service.CreateUserAsync(new UserCreateRequestDto
            {
                Email = "test@email.com",
                FirstName = "test",
                LastName = "test"
            });
            
            // Assert
            Assert.That(result.Succeeded, Is.True);
            
            _userManager.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Once());
        }
        
        #endregion

        #region DeleteUserAsync

        [Test]
        public async Task DeleteUserAsync_ShouldReturnNotFound_WhenUserIsNotFound()
        {
            // Arrange
            _userManager.Setup(_ => _.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((User) null);
            
            // Act
            var result = await _service.DeleteUserAsync("someId");
            
            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.NotFoundError), Is.True);
            
            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.DeleteAsync(It.IsAny<User>()), Times.Never);
        }
        
        [Test]
        public async Task DeleteUserAsync_ShouldReturnError_WhenDeletionFailed()
        {
            // Arrange
            _userManager.Setup(_ => _.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());
            _userManager.Setup(_ => _.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Failed());
            
            // Act
            var result = await _service.DeleteUserAsync("someId");
            
            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.InvalidOperationError), Is.True);
            
            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.DeleteAsync(It.IsAny<User>()), Times.Once);
        }

        #endregion
    }    
}
