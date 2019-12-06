using System.Threading.Tasks;
using Signawel.Mobile.Bootstrap.Abstract;

namespace Signawel.Mobile.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public MenuViewModel MenuViewModel { get; set; }

        public MainViewModel(INavigationService navigationService, MenuViewModel menuViewModel)
        {
            this._navigationService = navigationService;
            MenuViewModel = menuViewModel;
        }

        public override async Task InitializeAsync(object data)
        {
            await Task.WhenAll(
                MenuViewModel.InitializeAsync(data),
                _navigationService.NavigateToAsync<HomeViewModel>()
            );
        }

    }
}
