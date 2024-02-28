using System.Windows.Input;

namespace RedM_Launcher.Utils
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private Func<object, bool>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

#pragma warning disable CS8604 // Possible null reference argument.
        public bool CanExecute(object? parameter)
        {

            return _canExecute == null || _canExecute(parameter);

        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }
#pragma warning restore CS8604 // Possible null reference argument.
    }
}
