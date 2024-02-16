using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SaintDenis_Launcher.Tools
{
    internal static class DialogBox
    {
        public static void Information(String message, String title) 
        {
            MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
        }
        public static void Warning(String message, String title)
        {
            MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
        }

        public static void Error(String message, String title)
        {
            MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
        }
    }
}
