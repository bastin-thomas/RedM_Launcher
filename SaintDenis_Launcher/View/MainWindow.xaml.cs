using SaintDenis_Launcher.Properties;
using SaintDenis_Launcher.Tools;
using SaintDenis_Launcher.Tools.API_Calls;
using System.Windows;
using System.Windows.Input;

namespace SaintDenis_Launcher.View
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

        #region Cross Removal
        // Prep stuff needed to remove close button on window
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        #endregion

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
