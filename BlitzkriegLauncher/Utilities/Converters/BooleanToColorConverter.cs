using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BlitzkriegLauncher.Utilities.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isActive = (bool) value;

            if (isActive)
                return new SolidColorBrush(Colors.DarkGreen);
            return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}