using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfTechnoTester.Services;
using WpfTechnoTester.State;
using WpfTechnoTester.ViewModels;
using static WpfTechnoTester.State.INavigator;

namespace WpfTechnoTester.Commands
{
    internal class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                var viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.Home:
                        _navigator.CurrentViewModel = new HomeViewModel();
                        break;
                    case ViewType.Register:
                        _navigator.CurrentViewModel = new UserSignupViewModel(null);
                        break;
                    case ViewType.Login:
                        _navigator.CurrentViewModel = new UserLoginViewModel();
                        break;
                    case ViewType.Todo:
                        _navigator.CurrentViewModel = new TodoViewModel();
                        break;
                    case ViewType.Journal:
                        _navigator.CurrentViewModel = new JournalViewModel();
                        break;
                }
            }
        }
    }
}
