﻿using Signawel.Mobile.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Signawel.Mobile.Bootstrap.Abstract;
using Xamarin.Forms;
using System.Threading.Tasks;
using Signawel.Mobile.Bootstrap;

namespace Signawel.Mobile.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ObservableCollection<MainMenuItem> MenuItems { get; set; }

        public string WelcomeText => "SIGNAWEL";

        public ICommand MenuItemTappedCommand => new AsyncCommand<object>(OnMenuItemTapped);

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

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

#pragma warning disable CS0618 // Type or member is obsolete
            await _navigationService.NavigateToAsync(type, parameters);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private void LoadMenuItems()
        {
            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Home",
                ViewModelType = typeof(MainViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Over",
                ViewModelType = typeof(AboutViewModel)
            });

            // TODO add login back in

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

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Temp-Report",
                ViewModelType = typeof(ReportViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Login",
                ViewModelType = typeof(LoginViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Register",
                ViewModelType = typeof(RegisterViewModel)
            });
        }

    }
}
