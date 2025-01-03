﻿using Domain.Models;
using System.Windows.Input;

namespace WpfTechnoTester.Commands
{
    internal sealed class LoginCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?> _canExecute;

        public LoginCommand(Action<object?> execute, Predicate<object?> canExecute)
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
            _execute((User)parameter!);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
