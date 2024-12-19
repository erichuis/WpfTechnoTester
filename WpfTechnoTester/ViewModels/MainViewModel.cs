using System.ComponentModel;
using WpfTechnoTester.State;

namespace WpfTechnoTester.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly INavigator _navigator;

        public event PropertyChangedEventHandler? PropertyChanged;

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