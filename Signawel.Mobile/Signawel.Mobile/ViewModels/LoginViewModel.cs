using Signawel.Mobile.Bootstrap;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Services.Abstract;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand ButtonTappedCommand => new AsyncCommand(ButtonTapped);

        public LoginViewModel(IAuthenticationService authenticationService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
        }

        private async Task ButtonTapped()
        {
            var tokenResponse = await _authenticationService.LoginAsync(Email, Password);

            if(tokenResponse != null)
            {
                Email = string.Empty;
                Password = string.Empty;
                await _navigationService.PopAsync();
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Aanmelden mislukt", "Foute email en wachtwoord combinatie.", "Ok");
                Password = string.Empty;
            }
        }
    }
}
