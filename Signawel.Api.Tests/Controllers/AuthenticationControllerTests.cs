using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.API.Extensions;
using Signawel.Api.Tests.Builders;
using Signawel.Api.Tests.Builders.Dtos;
using Signawel.Business.Abstractions.Services;
using Signawel.Dto.Authentication;

namespace Signawel.Api.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private AuthenticationController _authenticationController;
        private Mock<IAuthenticationService> _authenticationServiceMock;
        private string ClientIp { get; set; }

        [SetUp]
        public void Setup()
        {
            _authenticationServiceMock = new Mock<IAuthenticationService>();
            _authenticationController = new AuthenticationController(_authenticationServiceMock.Object)
            {
                ControllerContext = new ControllerContextBuilder().WithUser().WithClientIp().Build()
            };
            ClientIp = _authenticationController.ControllerContext.HttpContext
                .GetRemoteIpAddress(true).ToString();
        }

        [Test]
        public void LoginWithUnknownEmailAndPasswordShouldReturnBadRequest()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDtoBuilder().WithEmail().WithPassword().Build();

            _authenticationServiceMock.Setup(service => service.LoginEmailAsync(It.IsAny<string>(), 
                    It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult<TokenResponseDto>(null));

            // Act
            var result = _authenticationController.Login(loginRequestDto).Result as BadRequestResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            _authenticationServiceMock.Verify(service => service.LoginEmailAsync(loginRequestDto.Email, 
                loginRequestDto.Password, ClientIp), Times.Once);
        }

        [Test]
        public void LoginWithKnownEmailAndPasswordShouldReturnOk()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDtoBuilder().WithEmail().WithPassword().Build();
            var tokenResponseDto = new TokenResponseDto();

            _authenticationServiceMock.Setup(service => service.LoginEmailAsync(It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((tokenResponseDto));

            // Act
            var result = _authenticationController.Login(loginRequestDto).Result as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(tokenResponseDto));
            _authenticationServiceMock.Verify(service => service.LoginEmailAsync(loginRequestDto.Email, 
                loginRequestDto.Password, ClientIp), Times.Once);
        }

        [Test]
        public void RegisterWithInvalidUserShouldReturnBadRequest()
        {
            // Arrange
            var registerRequestDto = new RegisterRequestDtoBuilder().WithEmail()
                .WithPassword().WithPasswordRepeat().Build();

            _authenticationServiceMock.Setup(service => service.RegisterAsync(It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync((RegisterResponseDto)null);

            // Act
            var result = _authenticationController.Register(registerRequestDto).Result as BadRequestResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            _authenticationServiceMock.Verify(service =>
                service.RegisterAsync(registerRequestDto.Email, registerRequestDto.Password), Times.Once);
        }

        [Test]
        public void RegisterWithValidUserShouldReturnNoContent()
        {
            // Arrange
            var registerRequestDto = new RegisterRequestDtoBuilder().WithEmail()
                .WithPassword().WithPasswordRepeat().Build();
            var registerResponseDto = new RegisterResponseDto();

            _authenticationServiceMock.Setup(service => 
                service.RegisterAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(registerResponseDto);

            // Act
            var result = _authenticationController.Register(registerRequestDto).Result as NoContentResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            _authenticationServiceMock.Verify(service => 
                service.RegisterAsync(registerRequestDto.Email, registerRequestDto.Password), Times.Once);
        }
    }
}
