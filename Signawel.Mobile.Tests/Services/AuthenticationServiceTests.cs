using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Signawel.Dto.Authentication;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Services;
using Signawel.Mobile.Tests.Builders;
using Signawel.Mobile.ViewModels;
using Signawel.MobileData;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Signawel.Mobile.Tests.Services
{
    [TestFixture]
    public class AuthenticationServiceTests
    {

        private AuthenticationService _authenticationService;
        private Mock<IHttpService> _httpService;
        private SignawelMobileContext _context;
        private Mock<INavigationService> _navigationService;

        [SetUp]
        public void Setup()
        {
            _httpService = new Mock<IHttpService>();
            _context = SignawelMobileContextBuilder.GetDatabaseContext();
            _navigationService = new Mock<INavigationService>();

            _authenticationService = new AuthenticationService(_httpService.Object, _context, _navigationService.Object);
        }

        #region LoginAsync

        [Test]
        public async Task LoginAsync_ShouldReturnNull_WhenEmailEmpty()
        {
            // Arrange
            var email = "";
            var password = "Password";

            // Act
            var result = await _authenticationService.LoginAsync(email, password);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task LoginAsync_ShouldReturnNull_WhenPasswordEmpty()
        {
            // Arrange
            var email = "a@email.com";
            var password = "";

            // Act
            var result = await _authenticationService.LoginAsync(email, password);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task LoginAsync_ShouldReturnNull_WhenEmailAndPasswordEmpty()
        {
            // Arrange
            var email = "";
            var password = "";

            // Act
            var result = await _authenticationService.LoginAsync(email, password);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task LoginAsync_ShouldReturnNull_WhenHttpResponseIsError()
        {
            // Arrange
            var email = "a@email.com";
            var password = "Password";

            _httpService
                .Setup(_ => _.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized
                });

            // Act
            var result = await _authenticationService.LoginAsync(email, password);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task LoginAsync_ShouldReturnTokenResponse_WhenLoginSuccessfull()
        {
            // Arrange
            var email = "a@email.com";
            var password = "Password";

            var response = new TokenResponseDto
            {
                Token = Guid.NewGuid().ToString(),
                RefreshToken = Guid.NewGuid().ToString()
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(response));

            _httpService
                .Setup(_ => _.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = stringContent
                });

            // Act
            var result = await _authenticationService.LoginAsync(email, password);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<TokenResponseDto>());
        }

        #endregion LoginAsync

        #region RegisterAsync

        [Test]
        public async Task RegisterAsync_ShouldReturnNull_WhenEmailIsEmpty()
        {
            // Arrange
            var email = "";
            var password = "Password";
            var passwordRepeat = "Password";

            // Act
            var result = await _authenticationService.RegisterAsync(email, password, passwordRepeat);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RegisterAsync_ShouldReturnNull_WhenPasswordIsEmpty()
        {
            // Arrange
            var email = "a@email.com";
            var password = "";
            var passwordRepeat = "Password";

            // Act
            var result = await _authenticationService.RegisterAsync(email, password, passwordRepeat);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RegisterAsync_ShouldReturnNull_WhenPasswordRepeatIsEmpty()
        {
            // Arrange
            var email = "a@email.com";
            var password = "Password";
            var passwordRepeat = "";

            // Act
            var result = await _authenticationService.RegisterAsync(email, password, passwordRepeat);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RegisterAsync_ShouldReturnNull_WhenHttpResponseIsError()
        {
            // Arrange
            var email = "a@email.com";
            var password = "Password";
            var passwordRepeat = "Password";

            _httpService
                .Setup(_ => _.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            // Act
            var result = await _authenticationService.RegisterAsync(email, password, passwordRepeat);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RegisterAsync_ShouldReturnRegisterResponseDto_WhenRegisterSuccessfull()
        {
            // Arrange
            var email = "a@email.com";
            var password = "Password";
            var passwordRepeat = "Password";

            var response = new RegisterResponseDto
            {
                Email = "a@email.com",
                UserName = "a"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(response));

            _httpService
                .Setup(_ => _.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = stringContent
                });

            // Act
            var result = await _authenticationService.RegisterAsync(email, password, passwordRepeat);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<RegisterResponseDto>());
        }

        #endregion RegisterAsync

        #region IsAuthenticatedAsync

        [Test]
        public async Task IsAuthenticatedAsync_ShouldReturnFalse_WhenNoTokenInTheDatabase()
        {
            // Arrange

            // Act
            var result = await _authenticationService.IsAuthenticatedAsync();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task IsAuthenticatedAsync_ShouldReturnTrue_WhenTokenInTheDatabase()
        {
            // Arrange
            await _context.DbToken.AddAsync(new DbToken
            {
                Token = Guid.NewGuid().ToString(),
                RefreshToken = Guid.NewGuid().ToString()
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _authenticationService.IsAuthenticatedAsync();

            // Assert
            Assert.That(result, Is.True);
        }

        #endregion IsAuthenticatedAsync

        #region Logout

        [Test]
        public async Task Logout_ShouldRemoveTokenFromDatabase_WhenTokenInDatabase()
        {
            // Arrange
            await _context.DbToken.AddAsync(new DbToken
            {
                Token = Guid.NewGuid().ToString(),
                RefreshToken = Guid.NewGuid().ToString()
            });

            await _context.SaveChangesAsync();

            // Act
            await _authenticationService.Logout();

            // Assert
            Assert.That(_context.DbToken.FirstOrDefault(), Is.Null);

            _navigationService.Verify(_ => _.NavigateToAsync<MainViewModel>(), Times.Once);
        }

        [Test]
        public async Task Logout_ShouldDonothing_WhenNoTokenInDatabase()
        {
            // Arrange

            // Act
            await _authenticationService.Logout();

            // Assert
            _navigationService.Verify(_ => _.NavigateToAsync<MainViewModel>(), Times.Never);
        }

        #endregion Logout

    }
}
