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
        private String GetFolder(String name, String folder) => FileTools.GetFolder(name, folder);
        private String GetFolderFromFile(String name, String folder, String filter) => FileTools.GetFolderFromFile(name, folder, filter);
        #endregion

        #region Events

        #region Commands
        public RelayCommand onRedMFolderClick => new RelayCommand(execute => {
            Logger.Information("Open RedM _folder Click");
            try
            {
                if (RedmFolder.Equals("")) { RedmFolder = FileTools.DefaultRedMFolder; }
                RedmFolder = GetFolderFromFile("Select RedM.exe", RedmFolder, "RedM|RedM.exe");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });
                
        public RelayCommand onSteamFolderClick => new RelayCommand(execute => {
            Logger.Information("Open Steam _folder Click");
            try
            {
                if (SteamFolder.Equals("")) { SteamFolder = FileTools.DefaultSteamFolder; }
                SteamFolder = GetFolderFromFile("Select Steam.exe Executable", SteamFolder, "Steam|Steam.exe"); 
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });
                
        public RelayCommand onRockstarFolderClick => new RelayCommand(execute => {
            Logger.Information("Open Rockstar _folder Click");
            try
            {
                if (RockstarFolder.Equals("")) { RockstarFolder = FileTools.DefaultRockstarFolder; }
                RockstarFolder = GetFolderFromFile("Select Launcher.exe Executable", RockstarFolder, "Launcher|Launcher.exe");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });

        public RelayCommand onEpicFolderClick => new RelayCommand(execute => {
            Logger.Information("Open EpicGame _folder Click");
            try
            {
                if (EpicFolder.Equals("")) { EpicFolder = FileTools.DefaultEpicFolder; }
                EpicFolder = GetFolder("Select EpicGame _folder", EpicFolder);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });

        public RelayCommand onTeamSpeakFolderClick => new RelayCommand(execute => {
            Logger.Information("Open TeamSpeak _folder Click");
            try
            {
                if (TeamSpeakFolder.Equals("")) { TeamSpeakFolder = FileTools.DefaultTSFolder; }
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
