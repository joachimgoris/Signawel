using Xamarin.Forms;

namespace Signawel.Mobile
{


    public partial class MapPageView : ContentPage
    {
        public MapPageView()
        {
            InitializeComponent();
        }

        public bool FirstEdit { get; set; }

        private void SearchBar_Focused(object sender, FocusEventArgs e)
        {
            SearchBarFrame.Opacity = 1;
            SearchBarFrame.BorderColor = Color.Black;
            var searchBar = (SearchBar)sender;

            if (searchBar.Text == "")
            {
                MyLocationFrame.IsVisible = true;
            }
        }

        private void SearchBar_Unfocused(object sender, FocusEventArgs e)
        {
            SearchBarFrame.Opacity = 0.9;
            SearchBarFrame.BorderColor = Color.DarkGray;
            MyLocationFrame.IsVisible = false;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchBar = (SearchBar)sender;
            MyLocationFrame.IsVisible = false;

            if (searchBar.Text == "" && FirstEdit)
            {
                MyLocationFrame.IsVisible = true;
            }
            FirstEdit = true;
        }

        private void Picker_Focused(object sender, FocusEventArgs e)
        {
            var picker = (Picker)sender;
            picker.Title = "Zoekradius";
        }

        private void Picker_Unfocused(object sender, FocusEventArgs e)
        {
            var picker = (Picker)sender;
            picker.Title = null;
        }
    }
}
