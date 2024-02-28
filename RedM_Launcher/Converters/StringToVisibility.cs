using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RedM_Launcher.Converters
{
    class StringToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string isVisible)
            {
                return isVisible.Equals("") ? Visibility.Visible : Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
