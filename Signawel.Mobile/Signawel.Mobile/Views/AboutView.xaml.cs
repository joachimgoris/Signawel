using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signawel.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutView : ContentPage
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private async void Signawel_OnClicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("http://veiligheidscomite.limburg.be/signawel"));
        }

        private async void Pvl_OnClicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("http://www.limburg.be/pvl"));
        }

        private async void Pxl_OnClicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("https://www.pxl.be/Contact.html"));
        }
    }
}