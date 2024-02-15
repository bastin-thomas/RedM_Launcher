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
        private String? _currentVersion;

        private bool _isClearCacheOnLaunch;
        private bool _isAzertyInstallOnLaunch;
        private bool _isDirectConnectOnLaunch;
        private bool _isOpenRockstarOnLaunch;
        private bool _isOpenEpicgameOnLaunch;
        private bool _isOpenTeamSpeakOnLaunch;
        private bool _isDirectConnectTSOnLaunch;

        private String _redmServerIP;
        private String _tsServerIP;
        private String _tsPassword;
        
        private String _redmFolder;
        private String _steamFolder;
        private String _rockstarFolder;
        private String _epicFolder;
        private String _teamspeakFolder;
        #endregion

        #region Accessors
        public String? CurrentVersion
        {
            get { return _currentVersion; }
            set { _currentVersion = value; OnPropertyChanged(); }
        }

        public bool IsClearCacheOnLaunch
        {
            get { return _isClearCacheOnLaunch; }
            set { _isClearCacheOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsAzertyInstallOnLaunch
        {
            get { return _isAzertyInstallOnLaunch; }
            set { _isAzertyInstallOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsDirectConnectOnLaunch
        {
            get { return _isDirectConnectOnLaunch; }
            set { _isDirectConnectOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsOpenRockstarOnLaunch
        {
            get { return _isOpenRockstarOnLaunch; }
            set { _isOpenRockstarOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsOpenEpicgameOnLaunch
        {
            get { return _isOpenEpicgameOnLaunch; }
            set { _isOpenEpicgameOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsOpenTeamSpeakOnLaunch
        {
            get { return _isOpenTeamSpeakOnLaunch; }
            set { _isOpenTeamSpeakOnLaunch = value; OnPropertyChanged(); }
        }
        public bool IsDirectConnectTSOnLaunch
        {
            get { return _isDirectConnectTSOnLaunch; }
            set { _isDirectConnectTSOnLaunch = value; OnPropertyChanged(); }
        }

        public String RedmServerIP
        {
            get { return _redmServerIP; }
            set { _redmServerIP = value; OnPropertyChanged(); }
        }
        public String TSServerIP
        {
            get { return _tsServerIP; }
            set { _tsServerIP = value; OnPropertyChanged(); }
        }
        public String TSPassword
        {
            get { return _tsPassword; }
            set { _tsPassword = value; OnPropertyChanged(); }
        }

        public String RedmFolder
        {
            get { return _redmFolder; }
            set { _redmFolder = value; }
        }
        public String SteamFolder
        {
            get { return _steamFolder; }
            set { _steamFolder = value; }
        }
        public String RockstarFolder
        {
            get { return _rockstarFolder; }
            set { _rockstarFolder = value; }
        }
        public String EpicFolder
        {
            get { return _epicFolder; }
            set { _epicFolder = value; }
        }
        public String TeamSpeakFolder
        {
            get { return _teamspeakFolder; }
            set { _teamspeakFolder = value; }
        }
        #endregion

        #region Constructors
        public SettingsPageVM()
        {
            Version version = Assembly.GetExecutingAssembly()
                                      .GetName().Version!;
            CurrentVersion = version.ToString();

            IsClearCacheOnLaunch = false;
            IsAzertyInstallOnLaunch = false;

            IsDirectConnectOnLaunch = true;

            IsOpenRockstarOnLaunch = true;
            IsOpenEpicgameOnLaunch = false;

            IsOpenTeamSpeakOnLaunch = true;
            IsDirectConnectTSOnLaunch = true;

            RedmServerIP = "162.19.253.49";
            TSServerIP = "162.19.253.49";
            TSPassword = "4jKQz4t5";

            RedmFolder = "";
            SteamFolder = "";
            RockstarFolder = "";
            EpicFolder = "";
            TeamSpeakFolder = "";
        }
        #endregion

        #region Methods
        private String GetFolder(String name, String folder) => FileTools.GetFolder(name, folder);
        private String GetFolderFromFile(String name, String folder) => FileTools.GetFolderFromFile(name, folder);
        #endregion

        #region Events

        #region Commands
        public RelayCommand onRedMFolderClick => new RelayCommand(execute => {
            Logger.Information("Open RedM Folder Click");
            try
            {
                if (RedmFolder.Equals("")) { RedmFolder = FileTools.DefaultRedMFolder; }
                RedmFolder = GetFolderFromFile("Select RedM.exe", RedmFolder);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });
                
        public RelayCommand onSteamFolderClick => new RelayCommand(execute => {
            Logger.Information("Open Steam Folder Click");
            try
            {
                if (SteamFolder.Equals("")) { SteamFolder = FileTools.DefaultSteamFolder; }
                SteamFolder = GetFolderFromFile("Select Steam.exe Executable", SteamFolder); 
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });
                
        public RelayCommand onRockstarFolderClick => new RelayCommand(execute => {
            Logger.Information("Open Rockstar Folder Click");
            try
            {
                if (RockstarFolder.Equals("")) { RockstarFolder = FileTools.DefaultRockstarFolder; }
                RockstarFolder = GetFolderFromFile("Select Launcher.exe Executable", RockstarFolder);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });

        public RelayCommand onEpicFolderClick => new RelayCommand(execute => {
            Logger.Information("Open Epic Folder Click");
            try
            {
                if (EpicFolder.Equals("")) { EpicFolder = FileTools.DefaultEpicFolder; }
                EpicFolder = GetFolder("Select Epic Folder", EpicFolder);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        });

        public RelayCommand onTeamSpeakFolderClick => new RelayCommand(execute => {
            Logger.Information("Open TeamSpeak Folder Click");
            try
            {
                if (TeamSpeakFolder.Equals("")) { TeamSpeakFolder = FileTools.DefaultTSFolder; }
                TeamSpeakFolder = GetFolderFromFile("Select ts3client_win64.exe Executable", TeamSpeakFolder);
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
