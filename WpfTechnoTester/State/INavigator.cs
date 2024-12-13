using System.Windows.Input;
using WpfTechnoTester.ViewModels;

namespace WpfTechnoTester.State
{
    public enum ViewType
    {
        Home,
        Todo,
        Journal,
        Image,
        Game
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
