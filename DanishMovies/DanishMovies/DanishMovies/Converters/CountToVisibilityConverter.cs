using System;
using System.Globalization;
using Xamarin.Forms;

namespace DanishMovies.Converters
{
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value > 0
                ? SeparatorVisibility.Default 
                : SeparatorVisibility.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
