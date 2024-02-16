using Microsoft.Win32;
using SaintDenis_Launcher.Properties;
using SaintDenis_Launcher.Tools;
using SaintDenis_Launcher.Tools.Handlers;
using SaintDenis_Launcher.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;

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
                    new MessageDialog("Impossible to Launch Rockstar", "Rockstar Error");
                }
            }
            

            //Launch EpicGame if Needed
            if(Config.IsOpenEpicgameOnLaunch) 
            {
                try 
                {
                    EpicGame.Start();
                }
                catch (Exception ex) 
                {
                    Logger.LogError(ex);
                    new MessageDialog("Impossible to Launch Epic", "EpicGame Error");
                }   
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
                new MessageDialog("Impossible to Launch Steam", "Steam Error");
            }


            //Launching RedM
            try
            {
                if(Settings.Default.IsTimerOnLaunch) 
                {
                    Thread.Sleep(Settings.Default.Timer * 1000);
                }

                RedM.Start(Config.IsDirectConnectOnLaunch);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //TODO: Add a custom popup that Warn the user
                new MessageDialog("Impossible to Launch RedM", "RedM Error");
            }


            //Launch TeamSpeak
            if (Config.IsOpenTeamSpeakOnLaunch)
            {
                try 
                {
                    Teamspeak.Start(Settings.Default.IsDirectConnectTSOnLaunch);
                }
                catch (Win32Exception ex)
                {
                    Logger.LogError(ex);
                    new MessageDialog("Impossible to Launch Teamspeak", "Teamspeak Error");
                }
            }
        });

        public RelayCommand onClearCacheClick => new RelayCommand(execute => {
            Logger.Information("== ClearCache Click ==");
            try 
            {
                ClearCache.StartAsync();
                ClearCache.Wait();
            }
            catch(Exception ex) 
            {
                Logger.LogError(ex);
                new MessageDialog("Impossible to Clear Cache", "Clear Cache Error");
            }
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
