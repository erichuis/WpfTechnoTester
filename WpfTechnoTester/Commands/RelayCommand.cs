using System;
using System.Windows.Input;

namespace WpfTechnoTester.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        private readonly Func<bool> _canExecute;

        private event EventHandler? CanExecuteChangedInternal;

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            ArgumentNullException.ThrowIfNull(execute);

            ArgumentNullException.ThrowIfNull(canExecute);

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object? parameter)
        {
            
            return  _canExecute.Invoke();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChangedInternal;
            if (handler != null)
            {
                //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        //public void Destroy()
        //{
        //    _canExecute = _ => false;
        //    _execute = _ => { return; };
        //}

    }
}