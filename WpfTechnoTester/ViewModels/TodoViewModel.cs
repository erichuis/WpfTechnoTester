using Domain.Models;
using System.Collections.ObjectModel;
using WpfTechnoTester.Commands;
using WpfTechnoTester.Services;
using WpfTechnoTester.State;

namespace WpfTechnoTester.ViewModels
{
    public class TodoViewModel : ViewModelBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly IWindowService _windowService;

        public RelayCommand AddTodoItemCommand { get; }
        public RelayCommand DeleteTodoItemCommand { get; }
        public RelayCommand EditTodoItemCommand { get; }
        public RelayCommand LoadTodoItemsCommand { get; }

        public TodoViewModel(ITodoItemService todoItemService, 
            IWindowService windowService)
        {
            TodoItems = [];
            SelectedTodoItems = [];
            _todoItemService = todoItemService;
            _windowService = windowService;
            
            AddTodoItemCommand = new RelayCommand((param) => AddTodoItem(), null);
            DeleteTodoItemCommand = new RelayCommand((param) => DeleteTodoItem(), null);
            EditTodoItemCommand = new RelayCommand((param) => EditTodoItem(), null);
            LoadTodoItemsCommand = new RelayCommand((param) => RetrieveTodoItems(), null);
        }
        public List<TodoItem> SelectedTodoItems { get; set; }

        public ObservableCollection<TodoItem> TodoItems { get; set; }

        private async void RetrieveTodoItems()
        {
            TodoItems?.Clear();

            var tasks = await _todoItemService.GetAllAsync();
            foreach (var task in tasks) TodoItems?.Add(task);
        }

        private void AddTodoItem()
        {
            _windowService.ShowTodoItemEditViewDialog();
            //Todo TodoItems.Add(response); or requery
        }

        private void EditTodoItem()
        {
            //Todo pass the item to be edited
            _windowService.ShowTodoItemEditViewDialog();
        }

        private async void DeleteTodoItem()
        {
            if (SelectedTodoItems.Count == 0)
            {
                return;
            }

            foreach (var item in SelectedTodoItems)
            {
                await _todoItemService.DeleteAsync(item.Id);
                TodoItems.Remove(item);
            }
        }
    }
}
