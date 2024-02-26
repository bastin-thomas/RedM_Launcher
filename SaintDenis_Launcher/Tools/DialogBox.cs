using Custom_Dialog.Dialogs.Alert;
using Custom_Dialog.Dialogs.Service;

namespace SaintDenis_Launcher.Tools
{
    internal static class DialogBox
    {
        private static readonly IDialogService _alertServices = new DialogService();

        public static void Information(String message, String title)
        {
            try
            {
                var dialog = new AlertDialogViewModel(title, message, Alerts.Information);
                var result = _alertServices.OpenDialog(dialog);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            //MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
        }

        public static void Warning(String message, String title)
        {
            try
            {
                var dialog = new AlertDialogViewModel(title, message, Alerts.Warning);
                var result = _alertServices.OpenDialog(dialog);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            //MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
        }

        public static void Error(String message, String title)
        {
            try
            {
                var dialog = new AlertDialogViewModel(title, message, Alerts.Error);
                var result = _alertServices.OpenDialog(dialog);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            //MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
        }
    }
}
