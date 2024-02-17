using Microsoft.Win32;
using SaintDenis_Launcher.Model.Utils;
using SaintDenis_Launcher.Properties;
using SaintDenis_Launcher.Tools;
using SaintDenis_Launcher.Tools.Handlers;
using SaintDenis_Launcher.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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

        private bool _isLaunching;
        private bool _isLaunched;
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
        public bool IsLaunching
        {
            get { return _isLaunching; }
            set { _isLaunching = value; }
        }
        public bool IsLaunched
        {
            get { return _isLaunched; }
            set { _isLaunched = value; }
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
            if(IsLaunching) { return; }
            Logger.Information("== LaunchButton Click ==");
            IsLaunching = true;

            //ClearCache
            if (Config.IsClearCacheOnLaunch) 
            {
                try
                {
                    ClearCache.StartAsync();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    DialogBox.Error("Impossible to Clear Cache", "Clear Cache Error");
                }
            }

            //ModeFile
            if(Config.IsAzertyInstallOnLaunch) 
            {
                Task.Run(async () =>
                {
                    try
                    {
                        await PlaceAzerty.MoveFile();
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                        DialogBox.Error("Impossible to Install Azerty File", "Install Azerty File Error");
                    }
                });
            }


            //Launch Rockstar Launcher if Needed
            if(Config.IsOpenRockstarOnLaunch) 
            {
                try
                {
                    Rockstar.StartAsync();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    DialogBox.Error("Impossible to Launch Rockstar", "Rockstar Error");
                }
            }
            

            //Launch EpicGame if Needed
            if(Config.IsOpenEpicgameOnLaunch) 
            {
                try 
                {
                    EpicGame.StartAsync();
                }
                catch (Exception ex) 
                {
                    Logger.LogError(ex);
                    DialogBox.Error("Impossible to Launch Epic", "EpicGame Error");
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
                DialogBox.Error("Impossible to Launch Steam", "Steam Error");
            }


            //Set A Timing / Or Not depending on IsTimerOnLaunch
            long awaitTime = Settings.Default.IsTimerOnLaunch ? Settings.Default.Timer : 0;

            //After Timing Out
            try 
            {
                CallbackTimer.RunAfter(awaitTime, () =>
                {
                    //Launching RedM
                    try
                    {
                        RedM.Start(Config.IsDirectConnectOnLaunch);
                        RedM.WaitRedMInitialized();

                        RedM.OnProcessEnd(() => {
                            Logger.Information("RedM Has Been Closed");
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                        DialogBox.Error("Impossible to Launch RedM", "RedM Error");
                    }



                    //Launch TeamSpeak
                    if (Config.IsOpenTeamSpeakOnLaunch)
                    {
                        try
                        {
                            Teamspeak.StartAsync(Settings.Default.IsDirectConnectTSOnLaunch);
                        }
                        catch (Win32Exception ex)
                        {
                            Logger.LogError(ex);
                            DialogBox.Error("Impossible to Launch Teamspeak", "Teamspeak Error");
                        }
                    }
                });
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex);
                DialogBox.Error("The Timer as turned off unexpectedly ", "Timer Error");
            }
        });

        public RelayCommand onClearCacheClick => new RelayCommand(execute => {
            Logger.Information("== ClearCache Click ==");
            try 
            {
                ClearCache.StartAsync();
            }
            catch(Exception ex) 
            {
                Logger.LogError(ex);
                DialogBox.Error("Impossible to Clear Cache", "Clear Cache Error");
                return;
            }
            DialogBox.Information("Cache Cleared", "Clear Cache");
        });


        public RelayCommand onAzertyInstallClick => new RelayCommand(execute => {
            Logger.Information("== AzertyInstall Click ==");
            Task.Run(async () =>
            {
                try
                {
                    await PlaceAzerty.MoveFile();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    DialogBox.Error("Impossible to Install Azerty File", "Install Azerty File Error");
                    return;
                }
                DialogBox.Information("Game Configured in Azerty", "Install Azerty File");
            });
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
