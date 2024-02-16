using SaintDenis_Launcher.Properties;
using SaintDenis_Launcher.Tools;
using SaintDenis_Launcher.Utils;
using SaintDenis_Launcher.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SaintDenis_Launcher.ViewModel
{
    class MainWindowVM : INotifyPropertyChanged
    {
        #region Properties
        private object? _currentPage;
        private int _stateMachine;
        #endregion

        #region Accessors
        public object? CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; OnPropertyChanged(); }
        }

        public int StateMachine
        {
            get { return _stateMachine; }
            set { _stateMachine = value; OnPropertyChanged(); }
        }
        
        public Settings Config
        {
            get { return Properties.Settings.Default; }
        }



        public MainPageVM MainPage { get; set; }
        public SettingsPageVM Settings { get; set; }
        #endregion

        #region Constructors
        public MainWindowVM()
        {
            MainPage = new MainPageVM();
            Settings = new SettingsPageVM();

            CurrentPage = MainPage;
            StateMachine = 0;
        }
        #endregion

        #region Methods
        #endregion

        #region Events
        #region Commands
        public RelayCommand onSettingClick => new RelayCommand(execute => {
            switch (StateMachine) 
            {
                case 0:
                    CurrentPage = Settings;
                    StateMachine = 1;
                    Logger.Information("== Go To Settings Page ==");
                    break;
                case 1:
                default:
                    CurrentPage = MainPage;
                    StateMachine = 0;
                    Logger.Information("== Go To Main Page ==");
                    Config.Save();
                    break;
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
