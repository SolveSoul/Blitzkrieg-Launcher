using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BlitzkriegLauncher.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isActive = (bool)value;

            if (isActive)
                return new SolidColorBrush(Colors.LightGreen);
            else
                return new SolidColorBrush(Colors.LightSalmon);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
