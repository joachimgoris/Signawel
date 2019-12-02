using System;
using System.Threading.Tasks;
using Signawel.Mobile.ViewModels;

namespace Signawel.Mobile.Bootstrap.Abstract
{
    public interface INavigationService
    {

        Task InitializeAsync();

        [Obsolete("try not to use this method")]
        Task NavigateToAsync(Type viewModelType, object parameter = null);

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task PopAsync();
    }
}
