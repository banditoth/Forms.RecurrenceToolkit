using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace banditoth.Forms.RecurrenceToolkit.Converters
{
    public class NullToFalseConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value != null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
