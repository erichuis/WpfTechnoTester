using System.Windows.Input;

namespace WpfTechnoTester.Commands
{
    public sealed class SignUpCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?> _canExecute;

        public SignUpCommand(Action<object?> execute, Predicate<object?> canExecute)
        {
            _execute = execute; 
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return parameter == null ? true : _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(null);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

}
