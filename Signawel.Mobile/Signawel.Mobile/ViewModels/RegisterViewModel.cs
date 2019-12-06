using Signawel.Mobile.Bootstrap;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Services.Abstract;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordRepeat { get; set; }

        public ICommand ButtonTappedCommand => new AsyncCommand(ButtonTapped);

        public RegisterViewModel(IAuthenticationService authenticationService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
        }

        private async Task ButtonTapped()
        {
            if(string.Equals(Password, PasswordRepeat))
            {
                var response = await _authenticationService.RegisterAsync(Email, Password, PasswordRepeat);

                if (response != null)
                {
                    Email = string.Empty;
                    Password = string.Empty;
                    PasswordRepeat = string.Empty;
                    await _navigationService.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Registreren mislukt.", "Er is iets misgegaan.", "Ok.");
              }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Registreren mislukt.", "Wachtwoord is niet hetzelfde als de herhaling.", "Ok.");
            }
            

            
        }
    }
}