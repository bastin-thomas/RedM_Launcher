using SaintDenis_Launcher.Tools;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SaintDenis_Launcher.Converters
{
    class StateMachineToVisibilityInverted : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int valueInt) 
            {
                if (valueInt == 0) { return Visibility.Hidden; }
                if (valueInt == 1) {  return Visibility.Visible; }
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
