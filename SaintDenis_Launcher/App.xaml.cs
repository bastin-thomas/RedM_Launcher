using SaintDenis_Launcher.Tools;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace SaintDenis_Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string workingDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string Local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Logger.Setup();
            Logger.Information("Launcher Startup");
        }
    }

}
