using System;
using System.Globalization;
using Xamarin.Forms;

namespace Signawel.Mobile.Converter
{
    public class AnswerSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (value is SelectedItemChangedEventArgs eventArgs)
            {
                return eventArgs.SelectedItem;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object
            parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
