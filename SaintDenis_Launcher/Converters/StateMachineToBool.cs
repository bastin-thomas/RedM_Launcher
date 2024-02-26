using System.Globalization;
using System.Windows.Data;

namespace SaintDenis_Launcher.Converters
{
    class StateMachineToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int valueInt)
            {
                if (valueInt == 0) { return true; }
                if (valueInt == 1) { return false; }
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
