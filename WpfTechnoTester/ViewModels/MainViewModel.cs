using WpfTechnoTester.State;

namespace WpfTechnoTester.ViewModels
{
    public class MainViewModel
    {
        private readonly INavigator _navigator;

        public INavigator Navigator
        {
            get { return _navigator; }
        }

        public MainViewModel(
            INavigator navigator)
        {
            _navigator = navigator;
            _navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }
    }
}