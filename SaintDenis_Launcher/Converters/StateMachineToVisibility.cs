using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SaintDenis_Launcher.Converters
{
    class StateMachineToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int valueInt)
            {
                if (valueInt == 0) { return Visibility.Visible; }
                if (valueInt == 1) { return Visibility.Hidden; }
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
