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
            set { _isLaunching = value; OnPropertyChanged(); }
        }
        public bool IsLaunched
        {
            get { return _isLaunched; }
            set { _isLaunched = value; OnPropertyChanged(); }
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

            IsLaunching = false;
            IsLaunched = false;
        }
        #endregion

        #region Methods
        private void ResetStateLaunch() 
        {
            IsLaunched = false;
            IsLaunching = false;
        }

        private void LaunchClearCache(bool displayEndPopup = false, bool returnOnFail = false) 
        {
            try
            {
                ClearCache.StartAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                DialogBox.Error("Impossible to Clear Cache", "Clear Cache Error");
                if (returnOnFail)
                {
                    ResetStateLaunch();
                    return;
                }
            }
            if (displayEndPopup) 
            {
                DialogBox.Information("Cache Cleared", "Clear Cache");
                ResetStateLaunch();
            } 
        }

        private void LaunchPlaceAzerty(bool displayEndPopup = false, bool returnOnFail = false)
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
                    if (returnOnFail)
                    {
                        ResetStateLaunch();
                        return;
                    }
                }
                if (displayEndPopup) 
                {
                    DialogBox.Information("Game Configured in Azerty", "Install Azerty File");
                    ResetStateLaunch();
                } 
            });
        }

        private void LaunchRockstar(bool returnOnFail = false)
        {
            try
            {
                Rockstar.StartAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                DialogBox.Error("Impossible to Launch Rockstar, check if you have set the right Folder", "Rockstar Error");
                if (returnOnFail)
                {
                    ResetStateLaunch();
                    return;
                }
            }
        }

        private void LaunchEpic(bool returnOnFail = false) 
        {
            try
            {
                EpicGame.StartAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                DialogBox.Error("Impossible to Launch Epic, check if you have set the right Folder", "EpicGame Error");
                if (returnOnFail)
                {
                    ResetStateLaunch();
                    return;
                }
            }
        }

        private void LaunchSteam(bool returnOnFail = false) 
        {
            try
            {
                Steam.Start();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                DialogBox.Error("Impossible to Launch Steam, check if you have set the right Folder", "Steam Error");
                if (returnOnFail)
                {
                    ResetStateLaunch();
                    return;
                }
            }
        }

        private void LaunchRedM_AND_TeamSpeak(bool returnOnFail = false)
        {
            //Set A Timing / Or Not depending on IsTimerOnLaunch
            long awaitTime = Settings.Default.IsTimerOnLaunch ? Settings.Default.Timer : 0;

            Logger.Information("Steam Running: " + (Steam.IsRunning && Steam.IsInitialized) + ", Rockstar:" + Rockstar.IsLaunched + ", Epic:" + (Settings.Default.IsOpenEpicgameOnLaunch ? EpicGame.IsLaunched : true));
            if (Steam.IsRunning && Steam.IsInitialized && Rockstar.IsLaunched && (Settings.Default.IsOpenEpicgameOnLaunch ? EpicGame.IsLaunched : true))
            {
                awaitTime = 0;
            }

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
                        IsLaunched = true;

                        RedM.OnProcessEnd(() => {
                            IsLaunching = false;
                            IsLaunched = false;
                            Logger.Information("RedM Has Been Closed");
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                        DialogBox.Error("Impossible to Launch RedM", "RedM Error");
                        if (returnOnFail)
                        {
                            ResetStateLaunch();
                            return;
                        }
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
                            if (returnOnFail)
                            {
                                ResetStateLaunch();
                                return;
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                DialogBox.Error("The Timer as turned off unexpectedly ", "Timer Error");
                if (returnOnFail)
                {
                    ResetStateLaunch();
                    return;
                }
            }
        }
        #endregion

        #region Events
        #region Commands
        public RelayCommand onLaunchClick => new RelayCommand(execute => {
            if(IsLaunching) { return; }
            IsLaunching = true;

            Logger.Information("== LaunchButton Click ==");

            Task.Run(() => {
                //LaunchClearCache
                if (Config.IsClearCacheOnLaunch)
                {
                    LaunchClearCache();
                }

                //ModeFile
                if (Config.IsAzertyInstallOnLaunch)
                {
                    LaunchPlaceAzerty();
                }
                
                //Launch Rockstar Launcher if Needed
                if (Config.IsOpenRockstarOnLaunch)
                {
                    LaunchRockstar();
                }
                
                //Launch EpicGame if Needed
                if (Config.IsOpenEpicgameOnLaunch)
                {
                    LaunchEpic();
                }

                //Launch Steam & Wait it is Ready to be used by RedM
                LaunchSteam();

                LaunchRedM_AND_TeamSpeak();
            });
        });

        public RelayCommand onClearCacheClick => new RelayCommand(execute => {
            Logger.Information("== LaunchClearCache Click ==");
            IsLaunching = true;
            LaunchClearCache(true, true);
        });


        public RelayCommand onAzertyInstallClick => new RelayCommand(execute => {
            Logger.Information("== AzertyInstall Click ==");
            IsLaunching = true;
            LaunchPlaceAzerty(true, true);
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
