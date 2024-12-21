using System.Windows.Input;
using WpfTechnoTester.Services;
using WpfTechnoTester.State;
using WpfTechnoTester.ViewModels.Factories;

namespace WpfTechnoTester.Commands
{
    internal class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IAuthenticator _authenticator;
        private readonly IWindowService _windowService;

        public UpdateCurrentViewModelCommand(INavigator navigator, 
            IViewModelFactory viewModelFactory,
            IAuthenticator authenticator, 
            IWindowService windowService)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _authenticator = authenticator;
            _windowService = windowService;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }
        
        public void Execute(object? parameter)
        {
            if (parameter is ViewType type)
            {
                if (type != ViewType.Home && !_authenticator.IsLoggedIn)
                {
                    var result = _windowService.ShowUserLoginDialog();

                    if (!result)
                    {
                        _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Home);
                        return;
                    }
                }
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(type);
                
            }
        }
    }
}
