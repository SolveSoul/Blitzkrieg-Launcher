using System;
using System.Globalization;
using System.Windows.Data;

namespace BlitzkriegLauncher.Utilities.Converters
{
    public class DescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var description = (string) value;

            if (description == String.Empty || value == null)
                return "No description available";
            return description;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}