using WpfTechnoTester.State;

namespace WpfTechnoTester.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<JournalEntriesViewModel> _createJournalViewModel;
        private readonly CreateViewModel<TodoItemsViewModel> _createTodoViewModel;
        private readonly CreateViewModel<ImageViewModel> _createImageViewModel;
        private readonly CreateViewModel<GameViewModel> _createGameViewModel;
        private readonly CreateViewModel<AdminViewModel> _createAdminViewModel;

        public ViewModelFactory(
            CreateViewModel<HomeViewModel> createHomeViewModel, 
            CreateViewModel<JournalEntriesViewModel> createJournalViewModel,
            CreateViewModel<TodoItemsViewModel> createTodoViewModel,
            CreateViewModel<ImageViewModel> createImageViewModel,
            CreateViewModel<GameViewModel> createGameViewModel,
            CreateViewModel<AdminViewModel> createAdminViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createJournalViewModel = createJournalViewModel;
            _createTodoViewModel = createTodoViewModel;
            _createImageViewModel = createImageViewModel;
            _createGameViewModel = createGameViewModel;
            _createAdminViewModel = createAdminViewModel;
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
                case ViewType.Admin:
                    return _createAdminViewModel();
                default:
                    throw new ArgumentException($"This viewtype '{viewType}' does not have a viewmodel");
            }
        }
    }
}
