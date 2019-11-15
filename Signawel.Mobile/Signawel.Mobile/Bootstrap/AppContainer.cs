using Microsoft.Extensions.DependencyInjection;
using Signawel.Mobile.ViewModels;
using System;
using Signawel.Mobile.Bootstrap.Abstract;

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
            services.AddScoped<HomeViewModel>();
            services.AddScoped<InteractiveSketchViewModel>();
            services.AddScoped<LoginViewModel>();

            // General
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
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
