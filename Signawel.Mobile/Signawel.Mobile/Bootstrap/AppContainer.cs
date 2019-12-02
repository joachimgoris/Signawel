using Microsoft.Extensions.DependencyInjection;
using Signawel.Mobile.ViewModels;
using System;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Services;
using Signawel.Mobile.Services.Abstract;
using Signawel.MobileData;
using Xamarin.Forms;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Signawel.Mobile.Bootstrap
{
    public class AppContainer : IDependencyResolver
    {

        private IServiceProvider _serviceProvider;
        private static AppContainer _instance;

        public static AppContainer Instance => _instance ?? (_instance = new AppContainer());

        public AppContainer()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            var services = new ServiceCollection();

            // ViewModels
            services.AddScoped<MainViewModel>();
            services.AddScoped<MenuViewModel>();
            services.AddScoped<CategoryViewModel>();
            services.AddScoped<LoginViewModel>();
            services.AddScoped<AboutViewModel>();
            services.AddScoped<InteractiveSketchViewModel>();
            services.AddScoped<MapPageViewModel>();
            services.AddScoped<ListViewRoadWorksPageViewModel>();

            services.AddTransient<DeterminationGraphViewModel>();
            services.AddTransient<LoginViewModel>();

            services.AddSingleton<AboutViewModel>();
            
            // Services
            services.AddScoped<IDeterminationGraphService, DeterminationGraphService>();
            services.AddScoped<IDeterminationSchemaService, DeterminationSchemaService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // General
            services.AddDbContext<SignawelMobileContext>(options => {
                string databaseName = "SignawelMobile";
                string databasePath;

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        SQLitePCL.Batteries_V2.Init();
                        databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName);
                        break;
                    case Device.Android:
                        databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                        break;
                    default:
                        throw new NotImplementedException("Platform not supported");
                }

                options.UseSqlite($"Filename={databasePath}");
            });
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<IDependencyResolver>(c => Instance);

            _serviceProvider = services.BuildServiceProvider();
        }

        public object Resolve(Type type)
        {
            return _serviceProvider.GetService(type);
        }

        public TType Resolve<TType>()
        {
            return _serviceProvider.GetService<TType>();
        }

    }
}
