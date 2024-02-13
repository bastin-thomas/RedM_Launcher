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
    class MainWindowVM : INotifyPropertyChanged
    {
        #region Properties
        private Color _backgroundColor;
        private BitmapImage? _backgroundImage;
        private String _currentVersion;
        #endregion

        #region Accessors
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; OnPropertyChanged(); }
        }
        public BitmapImage? BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; OnPropertyChanged(); }
        }

        public String CurrentVersion
        {
            get { return _currentVersion; }
            set { _currentVersion = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructors
        public MainWindowVM()
        {
            CurrentVersion = "Unkown";

            BackgroundImage = new BitmapImage(new Uri(@".Resources/Chinois_Background.png", UriKind.Relative));
            Palette palette = Palette.Generate(BackgroundImage);
            BackgroundColor = palette.GetDarkMutedColor(Colors.Black);
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
