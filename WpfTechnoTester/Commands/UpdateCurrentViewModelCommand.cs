using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfTechnoTester.Services;
using WpfTechnoTester.State;
using WpfTechnoTester.ViewModels;
using WpfTechnoTester.ViewModels.Factories;
using static WpfTechnoTester.State.INavigator;

namespace WpfTechnoTester.Commands
{
    internal class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private INavigator _navigator;
        private IViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }
        
        public void Execute(object? parameter)
        {
            if (parameter is ViewType type)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(type);
                
            }
        }
    }
}
