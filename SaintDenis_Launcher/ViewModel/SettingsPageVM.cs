using OokiiTsuki.Palette;
using System.ComponentModel;
using System.Printing.IndexedProperties;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace SaintDenis_Launcher.ViewModel
{
    class SettingsPageVM : INotifyPropertyChanged
    {
        #region Properties
        private String? _currentVersion;
        #endregion

        #region Accessors
        public String? CurrentVersion
        {
            get { return _currentVersion; }
            set { _currentVersion = value; OnPropertyChanged(); }
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
        #endregion

        #region Events
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
