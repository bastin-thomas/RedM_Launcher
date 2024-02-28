using RedM_Launcher.Properties;
using RedM_Launcher.Tools;
using RedM_Launcher.Tools.API_Calls;
using System.Windows;
using System.Windows.Input;

namespace RedM_Launcher.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Settings Config = Properties.Settings.Default;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Logger.Information("App is Closed Normally");
            Config.Save();
            Close();
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
            if (VersionAPI.NeedUpdate)
            {
                string Title = (string)App.Current.FindResource("NeedUpdate_Popup_Warning_Title");
                string Message = (string)App.Current.FindResource("NeedUpdate_Popup_Warning_Message");
                DialogBox.Warning(Message, Title);
            }
        }
    }
}
