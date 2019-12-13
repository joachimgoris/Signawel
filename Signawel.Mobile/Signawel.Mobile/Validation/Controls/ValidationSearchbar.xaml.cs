using System.Windows.Input;
using Signawel.Mobile.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signawel.Mobile.Validation.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidationSearchBar
    {
        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder),
            typeof(string), typeof(ValidationSearchBar));
        public string PlaceHolder
        {
            get => (string)GetValue(PlaceHolderProperty);
            set => SetValue(PlaceHolderProperty, value);
        }

        public static readonly BindableProperty SearchButtonCommandProperty = BindableProperty.Create(nameof(SearchButtonCommand),
            typeof(Command), typeof(ValidationSearchBar));
        public ICommand SearchButtonCommand
        {
            get => (Command)GetValue(SearchButtonCommandProperty);
            set => SetValue(SearchButtonCommandProperty, value);
        }

        public static readonly BindableProperty SearchButtonImageSourceProperty = BindableProperty.Create(nameof(SearchButtonImageSource),
            typeof(ImageSource), typeof(ValidationSearchBar));
        public ImageSource SearchButtonImageSource
        {
            get => (ImageSource)GetValue(SearchButtonImageSourceProperty);
            set => SetValue(SearchButtonImageSourceProperty, value);
        }

        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(nameof(EntryText),
            typeof(string), typeof(ValidationSearchBar), default(string));
        public string EntryText
        {
            get => (string)GetValue(EntryTextProperty);
            set => SetValue(EntryTextProperty, value);
        }

        public static readonly BindableProperty NavigatedProperty = BindableProperty.Create(nameof(Navigated),
            typeof(bool), typeof(ValidationSearchBar));
        public bool Navigated
        {
            get => (bool)GetValue(NavigatedProperty);
            set => SetValue(NavigatedProperty, value);
        }

        public static readonly BindableProperty SearchedElementProperty = BindableProperty.Create(nameof(SearchedElement),
            typeof(object), typeof(ValidationSearchBar));
        public object SearchedElement
        {
            get => GetValue(SearchedElementProperty);
            set => SetValue(SearchedElementProperty, value);
        }

        public ValidationSearchBar()
        {
            InitializeComponent();

            ValidationSearchBarLabel.TextColor = ErrorColor;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == EntryTextProperty.PropertyName)
            {
                SearchBarEntry.Text = EntryText;
            }
            else if (propertyName == PlaceHolderProperty.PropertyName)
            {
                SearchBarEntry.Placeholder = PlaceHolder;
            }
            else if (propertyName == ErrorMessageProperty.PropertyName)
            {
                ValidationSearchBarLabel.Text = ErrorMessage;
            }
            else if (propertyName == ErrorColorProperty.PropertyName)
            {
                ValidationSearchBarLabel.TextColor = ErrorColor;
            }
            else if (propertyName == TextColorProperty.PropertyName)
            {
                SearchBarEntry.TextColor = TextColor;
            } 
            else if (propertyName == SearchButtonCommandProperty.PropertyName)
            {
                ValidationSearchButton.Command = SearchButtonCommand;
            } 
            else if (propertyName == SearchButtonImageSourceProperty.PropertyName)
            {
                ValidationSearchButton.Source = SearchButtonImageSource;
            } 
            else if (propertyName == NavigatedProperty.PropertyName 
                     || propertyName == SearchedElementProperty.PropertyName)
            {
                if (Navigated && SearchedElement != null)
                {
                    IsValid = true;
                    ValidationSearchBarLabel.IsVisible = false;
                    SearchBarEntry.TextColor = TextColor;
                }
                else
                {
                    IsValid = false;
                    ValidationSearchBarLabel.Text = TextConstants.NoRoadworkSelected;
                    ValidationSearchBarLabel.IsVisible = true;
                    SearchBarEntry.TextColor = ErrorColor;
                }
            }
        }
    }
}