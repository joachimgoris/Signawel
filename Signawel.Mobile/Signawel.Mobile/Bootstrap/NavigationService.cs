using Signawel.Mobile.ViewModels;
using Signawel.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signawel.Mobile.Bootstrap.Abstract;
using Xamarin.Forms;

namespace Signawel.Mobile.Bootstrap
{
    public class NavigationService : INavigationService
    {

        private readonly IDependencyResolver _dependencyResolver;

        public NavigationService(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<MainViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return Navigate(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return Navigate(typeof(TViewModel), parameter);
        }

        [Obsolete("try not to use this method")]
        public Task NavigateToAsync(Type viewModelType, object parameter = null)
        {
            return Navigate(viewModelType, parameter);
        }

        protected virtual async Task Navigate(Type viewmodelType, object parameter)
        {
            var pageName = viewmodelType.Name.Replace("ViewModel", "View");
            var pageType = viewmodelType.Assembly.GetTypes().FirstOrDefault(c => c.Name == pageName);

            if (pageType == null)
            {
                throw new InvalidOperationException($"No page for { viewmodelType.Name }");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = _dependencyResolver.Resolve(viewmodelType) as ViewModelBase;
            page.BindingContext = viewModel;

            if(page is InteractiveSketchView interactiveSketchView)
            {
                interactiveSketchView.SetupLoadedEvent();
            }

            if(page is MainView)
            {
                Application.Current.MainPage = page;
            }
            else if(Application.Current.MainPage is MainView)
            {
                var mainPage = Application.Current.MainPage as MainView;

                if(mainPage.Detail is SignawelNavigationPage navigationPage)
                {
                    var currentPage = navigationPage.CurrentPage;

                    if(currentPage.GetType() != page.GetType() || page.GetType() == typeof(DeterminationGraphView))
                    {
                        await navigationPage.PushAsync(page);
                    }
                }
                else
                {
                    mainPage.Detail = new SignawelNavigationPage(page);
                }

                // close the menu
                mainPage.IsPresented = false;
            }
            else
            {
                if(Application.Current.MainPage is SignawelNavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(page);
                } else
                {
                    Application.Current.MainPage = new SignawelNavigationPage(page);
                }
            }
            
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }
    }
}
