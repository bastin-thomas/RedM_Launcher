using System.Windows.Input;

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).

namespace Custom_Dialog.Dialogs.Service
{
    /// <summary>
    /// A command implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute = null;
        private readonly Func<T, bool> _canExecute = null;


        public event EventHandler CanExecuteChanged

        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)

        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? ((Func<T, bool>)(_ => true));
        }

        public bool CanExecute(object parameter) => _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action execute)
            : base(_ => execute()) { }

        public RelayCommand(Action execute, Func<bool> canExecute)
            : base(_ => execute(), _ => canExecute()) { }
    }
}

#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
#pragma warning restore CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
