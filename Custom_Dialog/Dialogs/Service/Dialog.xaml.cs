using System.Windows;
using System.Windows.Input;

namespace Custom_Dialog.Dialogs.Service
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class Dialog : Window, IDialogWindow
    {
        public Dialog()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
