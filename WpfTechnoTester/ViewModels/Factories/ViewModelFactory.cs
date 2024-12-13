using WpfTechnoTester.State;

namespace WpfTechnoTester.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<JournalViewModel> _createJournalViewModel;
        private readonly CreateViewModel<TodoViewModel> _createTodoViewModel;
        private readonly CreateViewModel<ImageViewModel> _createImageViewModel;
        private readonly CreateViewModel<GameViewModel> _createGameViewModel;

        public ViewModelFactory(
            CreateViewModel<HomeViewModel> createHomeViewModel, 
            CreateViewModel<JournalViewModel> createJournalViewModel, 
            CreateViewModel<TodoViewModel> createTodoViewModel,
            CreateViewModel<ImageViewModel> createImageViewModel,
            CreateViewModel<GameViewModel> createGameViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createJournalViewModel = createJournalViewModel;
            _createTodoViewModel = createTodoViewModel;
            _createImageViewModel = createImageViewModel;
            _createGameViewModel = createGameViewModel; 
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.Image:
                    return _createImageViewModel();
                case ViewType.Game:
                    return _createGameViewModel();
                case ViewType.Todo:
                    return _createTodoViewModel();
                case ViewType.Journal:
                    return _createJournalViewModel();
                default:
                    throw new ArgumentException($"This viewtype '{viewType}' does not have a viewmodel");
            }
        }
    }
}
