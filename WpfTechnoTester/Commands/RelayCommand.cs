using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WpfTechnoTester.Commands
{

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?> _canExecute;
        private readonly EventHandler requerySuggested;
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object?> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute!; 
            _canExecute = canExecute;
            requerySuggested = (s, e) => RaiseCanExecuteChanged();
            CommandManager.RequerySuggested += requerySuggested;
        }
 
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter); 
        }

    }
}