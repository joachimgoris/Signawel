using System;
using Signawel.Mobile.ViewModels;
using Signawel.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signawel.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new InteractiveSketchView
            {
                BindingContext = new InteractiveSketchViewModel()
            };
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
