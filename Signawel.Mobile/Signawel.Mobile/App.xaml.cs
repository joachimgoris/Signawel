using Signawel.Mobile.Bootstrap;
using Signawel.Mobile.Bootstrap.Abstract;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Signawel.Mobile
{
    public partial class App : Application
    {
        private readonly IDependencyResolver _dependencyResolver;

        public App()
        {
            _dependencyResolver = AppContainer.Instance;

            InitializeComponent();
            InitializeNavigation();
        }

        private async Task InitializeNavigation()
        {
            var navigationService = _dependencyResolver.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
