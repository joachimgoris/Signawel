using Signawel.Mobile.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Services.Abstract;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Signawel.Mobile.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDeterminationGraphService _determinationGraphService;

        public ObservableCollection<MainMenuItem> MenuItems { get; set; }

        public string WelcomeText => "SIGNAWEL";

        public ICommand MenuItemTappedCommand => new Command(async (object menuItemTappedEventArgs) => await OnMenuItemTapped(menuItemTappedEventArgs));

        public MenuViewModel(INavigationService navigationService, IDeterminationGraphService determinationGraphService)
        {
            _navigationService = navigationService;
            _determinationGraphService = determinationGraphService;

            MenuItems = new ObservableCollection<MainMenuItem>();
            LoadMenuItems();
        }

        private async Task OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as MainMenuItem);

            var type = menuItem?.ViewModelType;
            var parameters = menuItem?.ViewModelParameters;

            if(type == null)
                return;

            if(type == typeof(DeterminationGraphViewModel))
            {
                var graph = await _determinationGraphService.GetDeterminationGraph();
                parameters = graph.Start;
            }

            await _navigationService.NavigateToAsync(type, parameters);
        }

        private void LoadMenuItems()
        {
            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Home",
                ViewModelType = typeof(HomeViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "About",
                ViewModelType = typeof(AboutViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Login",
                ViewModelType = typeof(LoginViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Temp-Determination",
                ViewModelType = typeof(DeterminationGraphViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Temp-Map",
                ViewModelType = typeof(MapPageViewModel)
            });
        }

    }
}
