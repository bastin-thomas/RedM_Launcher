using OokiiTsuki.Palette;
using System.ComponentModel;
using System.Printing.IndexedProperties;
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
        private BitmapImage _backgroundImage;
        #endregion

        #region Accessors
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; OnPropertyChanged(); }
        }
        public BitmapImage BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructors
        public MainWindowVM()
        {
            BackgroundImage = new BitmapImage(new Uri(@".Resources/Background_Chinois.png", UriKind.Relative));
            Palette palette = Palette.Generate(BackgroundImage);
            BackgroundColor = palette.GetDarkMutedColor();
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
