using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Signawel.Business.Abstractions.Services;
using Signawel.Business.Services;
using Signawel.Data.Abstractions.Repositories;
using Signawel.Domain;
using Signawel.Dto.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using System;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class AuthenticationServiceTests
    {

        private AuthenticationService _service;
        private Mock<IAuthenticationRepository> _repository;
        private Mock<IUserService> _userService;
        private Mock<IJwtTokenFactory> _tokenFactory;
        private Mock<UserManager<User>> _userManager;
        private Mock<IMailService> _mailService;

        private const string IpAddress = "127.0.0.1";

        [SetUp]
        public void SetUp()
        {
            var _logger = new Mock<ILogger<AuthenticationService>>();
            _repository = new Mock<IAuthenticationRepository>();
            _userService = new Mock<IUserService>();
            _tokenFactory = new Mock<IJwtTokenFactory>();
            _userManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);
            _mailService = new Mock<IMailService>();


            _service = new AuthenticationService(_logger.Object, _repository.Object, _userService.Object, _tokenFactory.Object, _userManager.Object, _mailService.Object);
        }

        #region LoginEmail

        [Test]
        public async Task LoginEmailAsync_ShouldReturnNull_WhenUserNotFound()
        {
            // Arrange
            _userManager
                .Setup(_ => _.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _service.LoginEmailAsync(It.IsAny<string>(), It.IsAny<string>(), IpAddress);

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.NotFoundError), Is.True);

            _userManager.Verify(_ => _.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            _userManager.Verify(_ => _.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Never);
            _repository.Verify(_ => _.AddLoginRecordAsync(It.IsAny<string>(), It.IsAny<string>(), false), Times.Once);
            _repository.Verify(_ => _.AddLoginRecordAsync(It.IsAny<string>(), It.IsAny<string>(), true), Times.Never);
            _userService.Verify(_ => _.GetUserClaimsAsync(It.IsAny<string>(), true), Times.Never);
            _tokenFactory.Verify(_ => _.GenerateToken(It.IsAny<User>(), It.IsAny<IList<Claim>>()), Times.Never);
        }

        [Test]
        public async Task LoginEmailAsync_ShouldReturnNull_WhenPasswordIsWrong()
        {
            // Arrange
            _userManager
                .Setup(_ => _.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            _userManager
                .Setup(_ => _.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _service.LoginEmailAsync(It.IsAny<string>(), It.IsAny<string>(), IpAddress);

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.AuthenticationIncorrectCredentialsError), Is.True);

            _userManager.Verify(_ => _.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Never);
            _repository.Verify(_ => _.AddLoginRecordAsync(It.IsAny<string>(), It.IsAny<string>(), false), Times.Once);
            _repository.Verify(_ => _.AddLoginRecordAsync(It.IsAny<string>(), It.IsAny<string>(), true), Times.Never);
            _userService.Verify(_ => _.GetUserClaimsAsync(It.IsAny<string>(), true), Times.Never);
            _tokenFactory.Verify(_ => _.GenerateToken(It.IsAny<User>(), It.IsAny<IList<Claim>>()), Times.Never);
        }

        [Test]
        public async Task LoginEmailAsync_ShouldReturnInvalid_WhenEmailIsNotConfirmed()
        {
            // Arrange
            _userManager
                .Setup(_ => _.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            _userManager
                .Setup(_ => _.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _userManager
                .Setup(_ => _.IsEmailConfirmedAsync(It.IsAny<User>()))
                .ReturnsAsync(false);

            // Act
            var result = await _service.LoginEmailAsync(It.IsAny<string>(), It.IsAny<string>(), IpAddress);

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.EmailNotConfirmedError), Is.True);

            _userManager.Verify(_ => _.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Once);
            _repository.Verify(_ => _.AddLoginRecordAsync(It.IsAny<string>(), It.IsAny<string>(), false), Times.Once);
            _repository.Verify(_ => _.AddLoginRecordAsync(It.IsAny<string>(), It.IsAny<string>(), true), Times.Never);
            _userService.Verify(_ => _.GetUserClaimsAsync(It.IsAny<string>(), true), Times.Never);
            _tokenFactory.Verify(_ => _.GenerateToken(It.IsAny<User>(), It.IsAny<IList<Claim>>()), Times.Never);
        }

        [Test]
        public async Task LoginEmailAsync_ShouldReturnToken_WhenUserIsFound()
        {
            // Arrange
            _userManager
                .Setup(_ => _.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            _userManager
                .Setup(_ => _.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _userManager
                .Setup(_ => _.IsEmailConfirmedAsync(It.IsAny<User>()))
                .ReturnsAsync(true);

            _userService
                .Setup(_ => _.GetUserClaimsAsync(It.IsAny<string>(), true))
                .ReturnsAsync(new List<Claim>());

            _tokenFactory
                .Setup(_ => _.GenerateToken(It.IsAny<User>(), It.IsAny<IList<Claim>>()))
                .ReturnsAsync(new DataResult<TokenResponseDto>
                {
                    Entity = new TokenResponseDto()
                });

            // Act
            var result = await _service.LoginEmailAsync(It.IsAny<string>(), It.IsAny<string>(), IpAddress);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Entity, Is.TypeOf<TokenResponseDto>());

            _userManager.Verify(_ => _.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _repository.Verify(_ => _.AddLoginRecordAsync(It.IsAny<string>(), It.IsAny<string>(), false), Times.Never);
            _repository.Verify(_ => _.AddLoginRecordAsync(It.IsAny<string>(), It.IsAny<string>(), true), Times.Once);
            _userService.Verify(_ => _.GetUserClaimsAsync(It.IsAny<string>(), true), Times.Once);
            _tokenFactory.Verify(_ => _.GenerateToken(It.IsAny<User>(), It.IsAny<IList<Claim>>()), Times.Once);
        }

        #endregion

        #region Register

        [Test]
        public async Task RegisterAsync_ShouldReturnUserCreationError_WhenUserIsNotCreated()
        {
            // Arrange
            _userManager
                .Setup(_ => _.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(It.IsAny<IdentityError[]>()));

            // Act
            var result = await _service.RegisterAsync("NotNull", "NotNull");

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.UserCreationError), Is.True);

            _userManager.Verify(_ => _.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()), Times.Never);
            _mailService.Verify(_ => _.SendConfirmationEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task RegisterAsync_ShouldReturnMailError_WhenMailIsNotSent()
        {
            // Arrange
            _userManager
                .Setup(_ => _.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mailService
                .Setup(_ => _.SendConfirmationEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(DataResult.WithError(ErrorCodes.MailError, "testError"));

            // Act
            var result = await _service.RegisterAsync("NotNull", "NotNull");

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.MailError), Is.True);

            _userManager.Verify(_ => _.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()), Times.Once);
            _mailService.Verify(_ => _.SendConfirmationEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task RegisterAsync_ShouldReturnNewRegisterResponseDto_WhenNewUserIsRegistered()
        {
            // Arrange
            _userManager
                .Setup(_ => _.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManager
                .Setup(_ => _.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("NotNull");

            _mailService
                .Setup(_ => _.SendConfirmationEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(DataResult.Success);

            // Act
            var result = await _service.RegisterAsync("NotNull", "NotNull");

            // Assert
            Assert.That(result.Entity, Is.Not.Null);
            Assert.That(result.Entity, Is.TypeOf<RegisterResponseDto>());

            _userManager.Verify(_ => _.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region ConfirmEmail

        [Test]
        public async Task ConfirmEmailAsync_ShouldReturnNotFoundError_WhenUserIsNotFound()
        {
            // Arrange
            _userManager
                .Setup(_ => _.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _service.ConfirmEmailAsync(new EmailConfirmRequestDto());

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.NotFoundError), Is.True);

            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task ConfirmEmailAsync_ShouldReturnEmailNotConfirmedError_WhenEmailIsNotConfirmed()
        {
            // Arrange
            _userManager
                .Setup(_ => _.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            _userManager
                .Setup(_ => _.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            // Act
            var result = await _service.ConfirmEmailAsync(new EmailConfirmRequestDto
            {
                Token = Guid.NewGuid().ToString()
            });

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.EmailNotConfirmedError), Is.True);

            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ConfirmEmailAsync_ShouldReturnSuccess_WhenEmailIsConfirmed()
        {
            // Arrange
            _userManager
                .Setup(_ => _.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            _userManager
                .Setup(_ => _.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _service.ConfirmEmailAsync(new EmailConfirmRequestDto
            {
                Token = Guid.NewGuid().ToString()
            });

            // Assert
            Assert.That(result.Errors, Is.Empty);
            Assert.That(result.Succeeded, Is.True);

            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region RefreshJwtToken

        [Test]
        public async Task RefreshJwtTokenAsync_ShouldReturnPrincipalTokenError_WhenPrincipalIsNotReturned()
        {
            // Arrange
            _tokenFactory
                .Setup(_ => _.GetPrincipalFromToken(It.IsAny<string>()))
                .Returns(DataResult<ClaimsPrincipal>.WithError(ErrorCodes.PrincipalTokenError, "testError"));

            // Act
            var result = await _service.RefreshJwtTokenAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.PrincipalTokenError), Is.True);

            _tokenFactory.Verify(_ => _.GetPrincipalFromToken(It.IsAny<string>()), Times.Once);
            _repository.Verify(_ => _.GetRefreshTokenByTokenAsync(It.IsAny<string>()), Times.Never);
            _repository.Verify(_ => _.UpdateRefreshTokenAsync(It.IsAny<RefreshToken>()), Times.Never);
            _userManager.Verify(_ => _.FindByIdAsync(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region GenerateForgotPasswordToken

        [Test]
        public async Task GenerateForgotPasswordToken_ShouldReturnNotFoundError_WhenUserIsNotFound()
        {
            // Arrange
            _userManager.Setup(_ => _.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);
            var model = new ForgotPasswordTokenRequestDto
            {
                Email = "test@email.com"
            };

            // Act
            var result = await _service.GenerateForgotPasswordTokenAsync(model);

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.NotFoundError), Is.True);

            _userManager.Verify(_ => _.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.GeneratePasswordResetTokenAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task GenerateForgotPasswordToken_ShouldReturnForgotPasswordTokenError_WhenTokenIsNotGenerated()
        {
            // Arrange
            var user = new User();
            _userManager.Setup(_ => _.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _userManager.Setup(_ => _.GeneratePasswordResetTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("");
            var model = new ForgotPasswordTokenRequestDto
            {
                Email = "test@email.com"
            };
            
            // Act
            var result = await _service.GenerateForgotPasswordTokenAsync(model);

            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.ForgotPasswordTokenError), Is.True);

            _userManager.Verify(_ => _.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.GeneratePasswordResetTokenAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task GenerateForgotPasswordToken_ShouldReturnCorrectObject_WhenUserIsFoundAndTokenIsGenerated()
        {
            // Arrange
            var user = new User();
            _userManager.Setup(_ => _.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _userManager.Setup(_ => _.GeneratePasswordResetTokenAsync(It.IsAny<User>()))
                .ReturnsAsync(Guid.NewGuid().ToString());
            var model = new ForgotPasswordTokenRequestDto
            {
                Email = "test@email.com"
            };
            
            // Act
            var result = await _service.GenerateForgotPasswordTokenAsync(model);

            // Assert
            Assert.That(result.Errors, Is.Empty);
            Assert.That(result.Succeeded, Is.True);

            _userManager.Verify(_ => _.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _userManager.Verify(_ => _.GeneratePasswordResetTokenAsync(It.IsAny<User>()), Times.Once);
        }

        #endregion
    }
}
