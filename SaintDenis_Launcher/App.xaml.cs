using SaintDenis_Launcher.Properties;
using SaintDenis_Launcher.Tools;
using Steamworks;
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

            if (Settings.Default.FirstLaunch) 
            {
                Logger.Information("First Launch: Getting all the Folder");

                if (Settings.Default.RedmFolder == "") 
                {
                    Settings.Default.RedmFolder = FileTools.DefaultRedMFolder;
                }

                if (Settings.Default.SteamFolder == "")
                {
                    Settings.Default.SteamFolder = FileTools.DefaultSteamFolder;
                }

                if (Settings.Default.TeamSpeakFolder == "")
                {
                    Settings.Default.TeamSpeakFolder = FileTools.DefaultTSFolder;
                }

                if (Settings.Default.RockstarFolder == "")
                {
                    Settings.Default.RockstarFolder = FileTools.DefaultRockstarFolder;
                }

                if (Settings.Default.EpicFolder == "")
                {
                    Settings.Default.EpicFolder = FileTools.DefaultEpicFolder;
                }

                Settings.Default.FirstLaunch = false;

                Logger.Information("First Launch: All Folder Retrieved");
                Settings.Default.Save();
            }
        }
    }
}
