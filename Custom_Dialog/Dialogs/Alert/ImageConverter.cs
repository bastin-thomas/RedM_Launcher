using System.Windows.Data;

namespace Custom_Dialog.Dialogs.Alert
{
    class ImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = (Alerts)values[0];

            switch (type) 
            {
                case Alerts.Information:
                    return values[1];
                case Alerts.Warning:
                    return values[2];
                case Alerts.Error:
                default:
                    return values[3];
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}