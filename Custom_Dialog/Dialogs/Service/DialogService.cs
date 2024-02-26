using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_Dialog.Dialogs.Service
{
    public class DialogService : IDialogService
    {
        public T OpenDialog<T>(DialogViewModelBase<T> viewModel)
        {
            IDialogWindow window = new Dialog();
            window.DataContext = viewModel;
            window.ShowDialog();
            return viewModel.DialogResult;
        }
    }
}
