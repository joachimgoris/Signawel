using Signawel.Mobile.Bootstrap.Abstract;
using System.Windows.Input;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;

        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public ICommand ButtonTappedCommand => new Command(ButtonTapped);

        public LoginViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        private async void ButtonTapped(object buttonTappedEventArgs)
        {
            var tokenResponse = await _authenticationService.LoginAsync(Email, Password);
            Token = tokenResponse.Token;
        }
    }
}
