using System.ComponentModel;
using System.Windows.Input;
using WpfTechnoTester.Commands;
using WpfTechnoTester.ViewModels;
using WpfTechnoTester.ViewModels.Helpers;

namespace WpfTechnoTester.State
{
    public class Navigator : ObservableObject, INavigator
    {
        public Navigator() 
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this);
        }
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel 
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }

        public ICommand UpdateCurrentViewModelCommand { get; set; }

    }
}
