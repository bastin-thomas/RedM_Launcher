using RedM_Launcher.Properties;
using RedM_Launcher.Tools;
using RedM_Launcher.Tools.API_Calls;
using System.Globalization;
using System.IO;
using System.Windows;

namespace RedM_Launcher
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
            VersionAPI.Setup();
            Logger.Setup();

            if (InstanceChecker.IsAlreadyLaunched())
            {
                Logger.Information("Application already launched");

                string Title = (string)App.Current.FindResource("App_Already_Opened_Popup_Title");
                string Message = (string)App.Current.FindResource("App_Already_Opened_Popup_Message");
                DialogBox.Error(Message, Title);

                Environment.Exit(0);
            }

            Logger.Information($"Launcher Startup");
            Logger.Information($"CurrentVersion: {VersionAPI.CurrentRealesedVersion}");
            Logger.Information($"LastVersion: {VersionAPI.LastRealesedVersion}");


            //Get OS Language and init the right Localization
            CultureInfo culture = CultureInfo.CurrentUICulture;
            string language = culture.TwoLetterISOLanguageName;
            Logger.Information($"Actual Language: {language}");

            ResourceDictionary newLocalization;
            switch (language)
            {
                case "fr":
                    newLocalization = new()
                    {
                        Source = new Uri("/Properties/Localization_FR.xaml", UriKind.Relative)
                    };
                    break;

                case "en":
                default:
                    newLocalization = new()
                    {
                        Source = new Uri("/Properties/Localization_EN.xaml", UriKind.Relative)
                    };
                    break;
            }

            ResourceDictionary actualLocalization = App.Current.Resources.MergedDictionaries.Last();
            App.Current.Resources.MergedDictionaries.Remove(actualLocalization);
            App.Current.Resources.MergedDictionaries.Add(newLocalization);



            //Put default Settings on Launch
            Logger.Information("Verify Corrupted Folder");

            if (!Directory.Exists(Settings.Default.RedmFolder))
            {
                Logger.Information("RedmFolder Corrupted");
                Settings.Default.RedmFolder = FileTools.DefaultRedMFolder;
            }

            if (!Directory.Exists(Settings.Default.SteamFolder))
            {
                Logger.Information("SteamFolder Corrupted");
                Settings.Default.SteamFolder = FileTools.DefaultSteamFolder;
            }

            if (!Directory.Exists(Settings.Default.TeamSpeakFolder))
            {
                Logger.Information("TeamSpeakFolder Corrupted");
                Settings.Default.TeamSpeakFolder = FileTools.DefaultTSFolder;
            }

            if (!Directory.Exists(Settings.Default.RockstarFolder))
            {
                Logger.Information("RockstarFolder Corrupted");
                Settings.Default.RockstarFolder = FileTools.DefaultRockstarFolder;
            }

            if (!Directory.Exists(Settings.Default.EpicFolder))
            {
                Logger.Information("EpicFolder Corrupted");
                Settings.Default.EpicFolder = FileTools.DefaultEpicFolder;
            }

            Settings.Default.Save();
        }
    }
}
