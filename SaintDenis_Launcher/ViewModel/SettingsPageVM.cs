using SaintDenis_Launcher.Properties;
using SaintDenis_Launcher.Tools;
using SaintDenis_Launcher.Utils;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SaintDenis_Launcher.ViewModel
{
    class SettingsPageVM : INotifyPropertyChanged
    {
        #region Properties
        private readonly Settings Config = Properties.Settings.Default;
        private String? _currentVersion;
        #endregion

        #region Accessors
        public String? CurrentVersion
        {
            get { return _currentVersion; }
            set { _currentVersion = value; OnPropertyChanged(); }
        }

        public bool IsTimerOnLaunch
        {
            get { return Config.IsTimerOnLaunch; }
            set { Config.IsTimerOnLaunch = value; OnPropertyChanged(); }
        }

        public bool IsClearCacheOnLaunch
        {
            get { return Config.IsClearCacheOnLaunch; }
            set { Config.IsClearCacheOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsAzertyInstallOnLaunch
        {
            get { return Config.IsAzertyInstallOnLaunch; }
            set { Config.IsAzertyInstallOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsDirectConnectOnLaunch
        {
            get { return Config.IsDirectConnectOnLaunch; }
            set { Config.IsDirectConnectOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsOpenRockstarOnLaunch
        {
            get { return Config.IsOpenRockstarOnLaunch; }
            set { Config.IsOpenRockstarOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsOpenEpicgameOnLaunch
        {
            get { return Config.IsOpenEpicgameOnLaunch; }
            set { Config.IsOpenEpicgameOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsOpenTeamSpeakOnLaunch
        {
            get { return Config.IsOpenTeamSpeakOnLaunch; }
            set { Config.IsOpenTeamSpeakOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsDirectConnectTSOnLaunch
        {
            get { return Config.IsDirectConnectTSOnLaunch; }
            set { Config.IsDirectConnectTSOnLaunch = value; OnPropertyChanged(); }
        }



        public String RedmServerIP
        {
            get { return Config.RedmServerIP; }
            set { Config.RedmServerIP = value; OnPropertyChanged(); }
        }
        public String TSServerIP
        {
            get { return Config.TSServerIP; }
            set { Config.TSServerIP = value; OnPropertyChanged(); }
        }
        public String TSPassword
        {
            get { return Config.TSPassword; }
            set { Config.TSPassword = value; OnPropertyChanged(); }
        }
        public long Timer 
        {
            get { return Config.Timer; }
            set { Config.Timer = value; OnPropertyChanged(); }
        }
        
        public String RedmFolder
        {
            get { return Config.RedmFolder; }
            set { Config.RedmFolder = value; }
        }
        public String SteamFolder
        {
            get { return Config.SteamFolder; }
            set { Config.SteamFolder = value; }
        }
        public String RockstarFolder
        {
            get { return Config.RockstarFolder; }
            set { Config.RockstarFolder = value; }
        }
        public String EpicFolder
        {
            get { return Config.EpicFolder; }
            set { Config.EpicFolder = value; }
        }
        public String TeamSpeakFolder
        {
            get { return Config.TeamSpeakFolder; }
            set { Config.TeamSpeakFolder = value; }
        }

        public static bool IsLaunched
        {
            get { return false; }
            set { }
        }

        public static bool IsLaunching
        {
            get { return false; }
            set { }
        }
        #endregion

        #region Constructors
        public SettingsPageVM()
        {
            Version version = Assembly.GetExecutingAssembly()
                                      .GetName().Version!;
            CurrentVersion = version.ToString();
        }
        #endregion

        #region Methods
        private static String GetFolder(String name, String folder)
        {
            string? toreturn = FileTools.GetFolder(name, folder);
            if (toreturn is null) return "";
            return toreturn;
        }
        private static String GetFolderFromFile(String name, String folder, String filter)
        {
            string? toreturn = FileTools.GetFolderFromFile(name, folder, filter);
            if (toreturn is null) return "";
            return toreturn;
        }
        #endregion

        #region Events

        #region Commands
        public RelayCommand OnRedMFolderClick => new(execute =>
        {
            Logger.Information("Open RedM _folder Click");
            try
            {
                RedmFolder = GetFolderFromFile("Select RedM.exe", RedmFolder, "RedM|RedM.exe");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });

        public RelayCommand OnSteamFolderClick => new(execute =>
        {
            Logger.Information("Open Steam _folder Click");
            try
            {
                SteamFolder = GetFolderFromFile("Select Steam.exe Executable", SteamFolder, "Steam|Steam.exe"); 
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });

        public RelayCommand OnRockstarFolderClick => new(execute =>
        {
            Logger.Information("Open Rockstar _folder Click");
            try
            {
                RockstarFolder = GetFolderFromFile("Select Launcher.exe Executable", RockstarFolder, "Launcher|Launcher.exe");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });

        public RelayCommand OnEpicFolderClick => new(execute =>
        {
            Logger.Information("Open EpicGame _folder Click");
            try
            {
                EpicFolder = GetFolder("Select EpicGame _folder", EpicFolder);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });

        public RelayCommand OnTeamSpeakFolderClick => new(execute =>
        {
            Logger.Information("Open TeamSpeak _folder Click");
            try
            {
                TeamSpeakFolder = GetFolderFromFile("Select ts3client_win64.exe Executable", TeamSpeakFolder, "TeamSpeak|ts3client_win64.exe");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });
        #endregion

        #region INotifiedProperty Block
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #endregion
    }
}
