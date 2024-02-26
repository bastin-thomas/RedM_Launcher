using System.Windows;
using System.Windows.Controls;

namespace SaintDenis_Launcher.View
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void passwordWatermark_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordWatermark.Visibility = Visibility.Collapsed;
            passwordInput.Visibility = Visibility.Visible;
            passwordInput.Focus();
        }

        private void passwordInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordInput.Text))
            {
                passwordInput.Visibility = Visibility.Collapsed;
                passwordWatermark.Visibility = Visibility.Visible;
            }
        }


        private void ipWatermark_GotFocus(object sender, RoutedEventArgs e)
        {
            ipWatermark.Visibility = Visibility.Collapsed;
            ipInput.Visibility = Visibility.Visible;
            ipInput.Focus();
        }

        private void ipInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ipInput.Text))
            {
                ipInput.Visibility = Visibility.Collapsed;
                ipWatermark.Visibility = Visibility.Visible;
            }
        }

        private void serveripWatermark_GotFocus(object sender, RoutedEventArgs e)
        {
            serveripWatermark.Visibility = Visibility.Collapsed;
            serveripInput.Visibility = Visibility.Visible;
            serveripInput.Focus();
        }

        private void serveripInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(serveripInput.Text))
            {
                serveripInput.Visibility = Visibility.Collapsed;
                serveripWatermark.Visibility = Visibility.Visible;
            }
        }

        private void timerWatermark_GotFocus(object sender, RoutedEventArgs e)
        {
            timerWatermark.Visibility = Visibility.Collapsed;
            timerInput.Visibility = Visibility.Visible;
            timerInput.Focus();
        }

        private void timerInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(serveripInput.Text))
            {
                timerInput.Visibility = Visibility.Collapsed;
                timerWatermark.Visibility = Visibility.Visible;
            }
        }
    }
}
