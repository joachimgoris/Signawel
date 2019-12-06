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
    public class RegisterViewModelTests
    {
        private RegisterViewModel _registerViewModel;
        private Mock<IAuthenticationService> _authenticationMock;
        private Mock<INavigationService> _navigationService;

        [SetUp]
        public void Setup()
        {
            _authenticationMock = new Mock<IAuthenticationService>();
            _navigationService = new Mock<INavigationService>();

            _registerViewModel = new RegisterViewModel(_authenticationMock.Object, _navigationService.Object);
        }

        #region ButtonTappedCommand

        [Test]
        public async Task ButtonTappedCommand_ShouldNavigateAndClear_WhenRegisterIsSuccesful()
        {
            // Arrange
            _registerViewModel.Email = "test@example.com";
            _registerViewModel.Password = "String1234!";
            _registerViewModel.PasswordRepeat = "String1234!";

            _authenticationMock
                .Setup(_ => _.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new RegisterResponseDto());

            // Act
            await ((AsyncCommand)_registerViewModel.ButtonTappedCommand).ExecuteAsync();

            // Assert
            Assert.That(_registerViewModel.Email, Is.EqualTo(string.Empty));
            Assert.That(_registerViewModel.Password, Is.EqualTo(string.Empty));
            Assert.That(_registerViewModel.PasswordRepeat, Is.EqualTo(string.Empty));

            _navigationService.Verify(_ => _.PopAsync(), Times.Once);
        }

        #endregion
    }
}
