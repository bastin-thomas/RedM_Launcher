using System.Globalization;
using System.Windows.Data;

namespace SaintDenis_Launcher.Converters
{
    class StateMachineToBoolInverted : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int valueInt)
            {
                if (valueInt == 0) { return false; }
                if (valueInt == 1) { return true; }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
