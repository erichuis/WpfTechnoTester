using WpfTechnoTester.State;

namespace WpfTechnoTester.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
