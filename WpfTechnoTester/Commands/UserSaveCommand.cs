﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;

//namespace WpfTechnoTester.Commands
//{
//    public class UserSaveCommand : ICommand
//    {
//        private readonly Action<object?> _execute;
//        private readonly Predicate<object?> _canExecute;

//        public UserSaveCommand(Action<object?> execute, Predicate<object?> canExecute)
//        {
//            _execute = execute;
//            _canExecute = canExecute;
//        }

//        public bool CanExecute(object? parameter)
//        {
//            return parameter == null ? true : _canExecute(parameter);
//        }

//        public void Execute(object? parameter)
//        {
//            _execute(null);
//        }

//        public event EventHandler? CanExecuteChanged
//        {
//            add { CommandManager.RequerySuggested += value; }
//            remove { CommandManager.RequerySuggested -= value; }
//        }
//    }
//}
