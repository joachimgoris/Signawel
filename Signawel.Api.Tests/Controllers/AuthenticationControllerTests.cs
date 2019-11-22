using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.API.Extensions;
using Signawel.Api.Tests.Builders;
using Signawel.Api.Tests.Builders.Dtos;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.Constants;
using Signawel.Dto.Authentication;
using Signawel.Domain.DataResults;

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
                .ReturnsAsync(
                    DataResult<TokenResponseDto>.WithPublicError(ErrorCodes.ParameterEmptyError, "test Error"));

            // Act
            var result = _authenticationController.Login(loginRequestDto).Result as BadRequestObjectResult;

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
            var tokenResponseDto = new DataResult<TokenResponseDto>
            {
                Entity =  new TokenResponseDto()
            };

            _authenticationServiceMock.Setup(service => service.LoginEmailAsync(It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((tokenResponseDto));

            // Act
            var result = _authenticationController.Login(loginRequestDto).Result as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(tokenResponseDto.Entity));
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
                It.IsAny<string>()))
                .ReturnsAsync(
                    DataResult<RegisterResponseDto>.WithError(ErrorCodes.ParameterEmptyError, "test Error"));

            // Act
            var result = _authenticationController.Register(registerRequestDto).Result as BadRequestObjectResult;

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
            var registerResponseDto = new DataResult<RegisterResponseDto>();

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

        [Test]
        public void RefreshTokenWithInvalidDtoShouldReturnBadRequest()
        {
            // Arrange
            var refreshRequestDto = new RefreshRequestDtoBuilder()
                .WithJwtToken().WithRefreshToken().Build();

            _authenticationServiceMock.Setup(service =>
                    service.RefreshJwtTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(DataResult<TokenResponseDto>.WithError(ErrorCodes.ParameterEmptyError, "TestError"));

            // Act
            var result = _authenticationController.RefreshToken(refreshRequestDto).Result as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            _authenticationServiceMock.Verify(service =>
                service.RefreshJwtTokenAsync(refreshRequestDto.JwtToken, refreshRequestDto.RefreshToken), Times.Once);
        }

        [Test]
        public void RefreshTokenWithValidDtoShouldReturnOkObject()
        {
            // Arrange
            var refreshRequestDto = new RefreshRequestDtoBuilder()
                .WithJwtToken().WithRefreshToken().Build();
            var tokenResponseDto = new DataResult<TokenResponseDto>
            {
                Entity = new TokenResponseDto()
            };

            _authenticationServiceMock.Setup(service =>
                    service.RefreshJwtTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(tokenResponseDto);

            // Act
            var result = _authenticationController.RefreshToken(refreshRequestDto).Result as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(tokenResponseDto.Entity));
            _authenticationServiceMock.Verify(service =>
                service.RefreshJwtTokenAsync(refreshRequestDto.JwtToken, refreshRequestDto.RefreshToken), Times.Once);
        }
    }
}
