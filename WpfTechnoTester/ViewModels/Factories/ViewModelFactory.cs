using WpfTechnoTester.State;

namespace WpfTechnoTester.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<JournalViewModel> _createJournalViewModel;
        private readonly CreateViewModel<TodoViewModel> _createTodoViewModel;

        public ViewModelFactory(
            CreateViewModel<HomeViewModel> createHomeViewModel, 
            CreateViewModel<JournalViewModel> createJournalViewModel, 
            CreateViewModel<TodoViewModel> createTodoViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createJournalViewModel = createJournalViewModel;
            _createTodoViewModel = createTodoViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _createHomeViewModel();
                //case ViewType.Register:
                //    _navigator.CurrentViewModel = new UserSignupViewModel(null);
                //    break;
                //case ViewType.Login:
                //    _navigator.CurrentViewModel = new UserLoginViewModel();
                //    break;
                case ViewType.Todo:
                    return _createHomeViewModel();
                case ViewType.Journal:
                    return _createJournalViewModel();
                default:
                    throw new ArgumentException($"This viewtype '{viewType}' does not have a viewmodel");
            }
        }
    }
}
