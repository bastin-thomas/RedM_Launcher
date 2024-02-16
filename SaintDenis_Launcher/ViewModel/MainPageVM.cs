using SaintDenis_Launcher.Properties;
using SaintDenis_Launcher.Tools;
using SaintDenis_Launcher.Tools.Handlers;
using SaintDenis_Launcher.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SaintDenis_Launcher.ViewModel
{
    /// <summary>
    /// This Page will provide Button to Launch RedM / Clear Cache / Install Azerty And also display Servers Status
    /// </summary>
    class MainPageVM : INotifyPropertyChanged
    {
        #region Properties
        private int _currentPlayer;
        private int _maxPlayer;

        private bool _isServerOnline;
        private bool _isRedMOnline;
        #endregion

        #region Accessors
        public int CurrentPlayer
        {
            get { return _currentPlayer; }
            set { _currentPlayer = value; OnPropertyChanged(); }
        }
        public int MaxPlayer
        {
            get { return _maxPlayer; }
            set { _maxPlayer = value; OnPropertyChanged(); }
        }
        public bool IsServerOnline
        {
            get { return _isServerOnline; }
            set { _isServerOnline = value; OnPropertyChanged(); }
        }
        public bool IsRedMOnline
        {
            get { return _isRedMOnline; }
            set { _isRedMOnline = value; OnPropertyChanged(); }
        }

        public Settings Config 
        {
            get { return Properties.Settings.Default; }
        }
        #endregion

        #region Constructors
        public MainPageVM()
        {
            CurrentPlayer = 32;
            MaxPlayer = 48;

            IsServerOnline = true;
            IsRedMOnline = true;
        }
        #endregion

        #region Methods
        #endregion

        #region Events
        #region Commands
        public RelayCommand onLaunchClick => new RelayCommand(execute => {
            Logger.Information("== LaunchButton Click ==");

            //Launch Rockstar Launcher if Needed
            if(Config.IsOpenRockstarOnLaunch) 
            {
                try
                {
                    Rockstar.Start();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    //TODO: Add a custom popup that Warn the user
                }
            }


            //Launch EpicGame if Needed
            if(Config.IsOpenEpicgameOnLaunch) 
            {
                EpicGame.Start();
            }


            //Launch Steam & Wait it is Ready to be used by RedM
            try 
            {
                Steam.Start();
            } 
            catch(Exception ex) 
            {
                Logger.LogError(ex);
                //TODO: Add a custom popup that Warn the user
            }

            //Launching RedM
            try
            {
                RedM.Start(Config.IsDirectConnectOnLaunch);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //TODO: Add a custom popup that Warn the user
            }




            //Launch TeamSpeak
            if (Config.IsOpenTeamSpeakOnLaunch)
            {
                if (Config.IsDirectConnectTSOnLaunch)
                {
                    throw new NotImplementedException();
                }
                else 
                {
                    throw new NotImplementedException();
                }
            }
        });

        public RelayCommand onClearCacheClick => new RelayCommand(execute => {
            Logger.Information("== ClearCache Click ==");
        });

        public RelayCommand onAzertyInstallClick => new RelayCommand(execute => {
            Logger.Information("== AzertyInstall Click ==");
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
