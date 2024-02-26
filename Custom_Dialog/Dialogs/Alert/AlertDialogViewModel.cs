using Custom_Dialog.Dialogs.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Custom_Dialog.Dialogs.Alert
{
    public class AlertDialogViewModel : DialogViewModelBase<DialogResults>
    {

        #region Properties
        private Alerts _type;
        #endregion

        #region Accessors
        public ICommand OKCommand { get; private set; }
        public Alerts Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructors
        public AlertDialogViewModel(string title, string message, Alerts type) : base(title, message)
        {
            OKCommand = new RelayCommand<IDialogWindow>(OK);
            Type = type;
        }
        #endregion

        #region Methods
        private void OK(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Undefined);
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
