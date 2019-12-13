
using Xamarin.Forms;

namespace Signawel.Mobile.Validation.Controls.Abstract
{
    public abstract class ValidationFormEntry : ContentView
    {
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor),
            typeof(Color), typeof(ValidationFormEntry), default(Color));
        public Color ErrorColor
        {
            get => (Color)GetValue(ErrorColorProperty);
            set => SetValue(ErrorColorProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor),
            typeof(Color), typeof(ValidationFormEntry), default(Color));
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(nameof(ErrorMessage),
            typeof(string), typeof(ValidationFormEntry), default(string));
        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid),
            typeof(bool), typeof(ValidationFormEntry), default(bool), BindingMode.OneWayToSource);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }
    }
}