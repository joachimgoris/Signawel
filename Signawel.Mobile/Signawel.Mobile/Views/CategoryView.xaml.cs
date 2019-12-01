using Signawel.Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signawel.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryView : ContentPage
    {
        public CategoryView()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var categoryId = ((TappedEventArgs)e).Parameter.ToString();
            (this.BindingContext as CategoryViewModel).CategorySelectedCommand.Execute(categoryId);
        }
    }
}