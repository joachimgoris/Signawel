using System.ComponentModel.DataAnnotations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signawel.Mobile.Validation.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidationEmailEntry
    {
        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(nameof(EntryText),
            typeof(string), typeof(ValidationEmailEntry), default(string), BindingMode.TwoWay);
        public string EntryText
        {
            get => (string)GetValue(EntryTextProperty);
            set => SetValue(EntryTextProperty, value);
        }

        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder),
            typeof(string), typeof(ValidationEmailEntry), default(string));
        public string PlaceHolder
        {
            get => (string)GetValue(PlaceHolderProperty);
            set => SetValue(PlaceHolderProperty, value);
        }

        public ValidationEmailEntry()
        {
            InitializeComponent();

            #region Initialize values

            ValidationEntryEntry.TextColor = TextColor;
            ValidationEntryEntry.Text = EntryText;
            ValidationEntryEntry.Placeholder = PlaceHolder;

            ValidationEntryLabel.Text = ErrorMessage;
            ValidationEntryLabel.TextColor = ErrorColor;
            ValidationEntryLabel.IsVisible = false;

            #endregion

            ValidationEntryEntry.TextChanged += OnTextChanged;
        }

        public void OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            EntryText = textChangedEventArgs.NewTextValue;
            ValidateEmail();
        }

        private void ValidateEmail()
        {
            if (new EmailAddressAttribute().IsValid(EntryText))
            {
                ValidationEntryEntry.TextColor = TextColor;
                ValidationEntryLabel.IsVisible = false;
                IsValid = true;
            }
            else
            {
                ValidationEntryEntry.TextColor = ErrorColor;
                ValidationEntryLabel.IsVisible = true;
                IsValid = false;
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == EntryTextProperty.PropertyName)
            {
                ValidationEntryEntry.Text = EntryText;
            } 
            else if (propertyName == PlaceHolderProperty.PropertyName)
            {
                ValidationEntryEntry.Placeholder = PlaceHolder;
            } 
            else if (propertyName == ErrorMessageProperty.PropertyName)
            {
                ValidationEntryLabel.Text = ErrorMessage;
            } 
            else if (propertyName == ErrorColorProperty.PropertyName)
            {
                ValidationEntryLabel.TextColor = ErrorColor;
            } 
            else if (propertyName == TextColorProperty.PropertyName)
            {
                ValidationEntryEntry.TextColor = TextColor;
            }
        }
    }
}