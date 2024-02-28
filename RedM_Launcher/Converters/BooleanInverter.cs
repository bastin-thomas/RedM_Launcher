using System.Globalization;
using System.Windows.Data;

namespace RedM_Launcher.Converters
{
    class BooleanInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool valueBoolean)
            {
                return !valueBoolean;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
