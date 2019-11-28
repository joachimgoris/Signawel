
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signawel.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignawelNavigationPage : NavigationPage
    {
        public SignawelNavigationPage()
        {
            InitializeComponent();
        }

        public SignawelNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}