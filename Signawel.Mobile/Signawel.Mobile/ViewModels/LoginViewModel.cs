using Signawel.Mobile.Bootstrap;
using Signawel.Mobile.Bootstrap.Abstract;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Signawel.Mobile.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;

        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public ICommand ButtonTappedCommand => new AsyncCommand<object>(ButtonTapped);

        public LoginViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        private async Task ButtonTapped(object buttonTappedEventArgs)
        {
            var tokenResponse = await _authenticationService.LoginAsync(Email, Password);
            Token = tokenResponse.Token;
        }
    }
}
