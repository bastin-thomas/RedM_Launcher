using Custom_Dialog.Dialogs.Alert;
using Microsoft.Win32;
using SaintDenis_Launcher.Model.Utils;
using SaintDenis_Launcher.Properties;
using SaintDenis_Launcher.Tools;
using SaintDenis_Launcher.Tools.API_Calls;
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
        private int _currentPlayer = 0;
        private int _maxPlayer = 0;

        private bool _isServerOnline = false;
        private bool _isRedMOnline = false;

        private bool _isLaunching = false;
        private bool _isLaunched = false;
        #endregion

        #region Accessors
        public int ConnectedPlayers
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

        public static Settings Config 
        {
            get { return Properties.Settings.Default; }
        }
        #endregion

        #region Constructors
        public MainPageVM()
        {   
            Task.Run(async () => {
                while (true) {
                    //Run tasks in parallel
                    Task[] tasks = [
                        new Task(() => { IsRedMOnline = CfxAPI.IsOnline;                }),
                        new Task(() => { ConnectedPlayers = ServerAPI.ConnectedPlayers; }),
                        new Task(() => { MaxPlayer = ServerAPI.MaxPlayers;              }),
                        new Task(() => { IsServerOnline = ServerAPI.IsOnline;           }),
                    ];

                    foreach (var task in tasks) { task.Start(); }

                    try
                    {
                        await Task.WhenAll(tasks);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                    }

                    //Wait some time before refresh
                    await Task.Delay(15 * 1000);
                }
            });
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
            var task = Task.Run(Cache.Clear);
            task.Wait();

            if(task.IsFaulted) 
            {
                Logger.LogError(task.Exception);

                string Title = (string)App.Current.FindResource("ClearCache_Error_Popup_Title");
                string Message = (string)App.Current.FindResource("ClearCache_Error_Popup_Message");
                DialogBox.Error(Message, Title);

                if (returnOnFail)
                {
                    ResetStateLaunch();
                    return;
                }
            }

            if (displayEndPopup)
            {
                string Title = (string)App.Current.FindResource("ClearCache_Info_Popup_Title");
                string Message = (string)App.Current.FindResource("ClearCache_Info_Popup_Message");
                DialogBox.Information(Message, Title);

                ResetStateLaunch();
            }
        }

        private void LaunchPlaceAzerty(bool displayEndPopup = false, bool returnOnFail = false)
        {
            var task = Task.Run(PlaceAzerty.MoveFile);
            task.Wait();

            if(task.IsFaulted)
            {
                Logger.LogError(task.Exception);

                string Title = (string)App.Current.FindResource("Azerty_Error_Popup_Title");
                string Message = (string)App.Current.FindResource("Azerty_Error_Popup_Message");
                DialogBox.Error(Message, Title);

                if (returnOnFail)
                {
                    ResetStateLaunch();
                    return;
                }
            }

            if (displayEndPopup)
            {
                string Title = (string)App.Current.FindResource("Azerty_Info_Popup_Title");
                string Message = (string)App.Current.FindResource("Azerty_Info_Popup_Message");
                DialogBox.Information(Message, Title);

                ResetStateLaunch();
            }
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

                string Title = (string)App.Current.FindResource("Rockstar_Error_Popup_Title");
                string Message = (string)App.Current.FindResource("Rockstar_Error_Popup_Message");
                DialogBox.Error(Message, Title);

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

                string Title = (string)App.Current.FindResource("Epic_Error_Popup_Title");
                string Message = (string)App.Current.FindResource("Epic_Error_Popup_Message");
                DialogBox.Error(Message, Title);

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

                string Title = (string)App.Current.FindResource("Steam_Error_Popup_Title");
                string Message = (string)App.Current.FindResource("Steam_Error_Popup_Message");
                DialogBox.Error(Message, Title);

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

            Logger.Information("Steam Running: " + Steam.HasBeenSkipped + ", Rockstar:" + (Settings.Default.IsOpenRockstarOnLaunch ? Rockstar.HasBeenSkipped : true) + ", Epic:" + (Settings.Default.IsOpenEpicgameOnLaunch ? EpicGame.HasBeenSkipped : true));
            if (Steam.HasBeenSkipped && (Settings.Default.IsOpenRockstarOnLaunch ? Rockstar.HasBeenSkipped : true) && (Settings.Default.IsOpenEpicgameOnLaunch ? EpicGame.HasBeenSkipped : true))
            {
                awaitTime = 1;
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
                        
                        string Title = (string)App.Current.FindResource("RedM_Error_Popup_Title");
                        string Message = (string)App.Current.FindResource("RedM_Error_Popup_Message");
                        DialogBox.Error(Message, Title);

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

                            string Title = (string)App.Current.FindResource("TeamSpeak_Error_Popup_Title");
                            string Message = (string)App.Current.FindResource("TeamSpeak_Error_Popup_Message");
                            DialogBox.Error(Message, Title);

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

                string Title = (string)App.Current.FindResource("Timer_Error_Popup_Title");
                string Message = (string)App.Current.FindResource("Timer_Error_Popup_Message");
                DialogBox.Error(Message, Title);
                
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
        public RelayCommand OnLaunchClick => new(execute =>
        {
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

        public RelayCommand OnClearCacheClick => new(execute =>
        {
            Logger.Information("== LaunchClearCache Click ==");
            IsLaunching = true;
            LaunchClearCache(true, true);
        });


        public RelayCommand OnAzertyInstallClick => new(execute =>
        {
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
