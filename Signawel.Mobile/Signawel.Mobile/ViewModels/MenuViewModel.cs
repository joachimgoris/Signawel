using Signawel.Mobile.Bootstrap;
using Signawel.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Signawel.Mobile.Bootstrap.Abstract;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ObservableCollection<MainMenuItem> MenuItems { get; set; }

        public string WelcomeText => "SIGNAWEL";

        public ICommand MenuItemTappedCommand => new Command(OnMenuItemTapped);

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            MenuItems = new ObservableCollection<MainMenuItem>();
            LoadMenuItems();
        }

        private void OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as MainMenuItem);

            var type = menuItem?.ViewModelType;
            _navigationService.NavigateToAsync(type);
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
                MenuText = "Temp-Interactive",
                ViewModelType = typeof(InteractiveSketchViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Login",
                ViewModelType = typeof(LoginViewModel)
            });
        }

    }
}
