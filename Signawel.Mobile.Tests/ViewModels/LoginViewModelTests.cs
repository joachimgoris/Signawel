using Moq;
using NUnit.Framework;
using Signawel.Dto.Authentication;
using Signawel.Mobile.Bootstrap;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Services.Abstract;
using Signawel.Mobile.ViewModels;
using System.Threading.Tasks;

namespace Signawel.Mobile.Tests.ViewModels
{
    [TestFixture]
    public class LoginViewModelTests
    {

        private LoginViewModel _loginViewModel;
        private Mock<IAuthenticationService> _authenticationMock;
        private Mock<INavigationService> _navigationService;

        [SetUp]
        public void Setup()
        {
            _authenticationMock = new Mock<IAuthenticationService>();
            _navigationService = new Mock<INavigationService>();

            _loginViewModel = new LoginViewModel(_authenticationMock.Object, _navigationService.Object);
        }

        #region ButtonTappedCommand

        [Test]
        public async Task ButtonTappedCommand_ShouldNavigateAndClear_WhenLoginSuccessfull()
        {
            // Arrange
            _loginViewModel.Email = "a@email.com";
            _loginViewModel.Password = "Password@001";

            _authenticationMock
                .Setup(_ => _.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new TokenResponseDto());

            // Act
            await ((AsyncCommand)_loginViewModel.ButtonTappedCommand).ExecuteAsync();

            // Assert
            Assert.That(_loginViewModel.Email, Is.EqualTo(string.Empty));
            Assert.That(_loginViewModel.Password, Is.EqualTo(string.Empty));
            _navigationService.Verify(_ => _.PopAsync(), Times.Once);
        }

        #endregion ButtonTappedCommand

    }
}
