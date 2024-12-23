using Domain.Models;
using System.Collections.ObjectModel;
using WpfTechnoTester.Commands;
using WpfTechnoTester.Services;

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
            
            AddTodoItemCommand = new RelayCommand((param) => AddTodoItem());
            DeleteTodoItemCommand = new RelayCommand((param) => DeleteTodoItem());
            EditTodoItemCommand = new RelayCommand((param) => EditTodoItem());
            LoadTodoItemsCommand = new RelayCommand((param) => RetrieveTodoItems());
        }
        public ObservableCollection<TodoItem> SelectedTodoItems { get; set; }

        public ObservableCollection<TodoItem> TodoItems { get; set; }

        public TodoItem? SelectedTodoItem { get; set; }

        private async void RetrieveTodoItems()
        {
            TodoItems?.Clear();

            var tasks = await _todoItemService.GetAllAsync();
            foreach (var task in tasks) TodoItems?.Add(task);
        }

        private void AddTodoItem()
        {
            SelectedTodoItem = new TodoItem() { Description = string.Empty, Title = string.Empty };
            _windowService.ShowTodoItemEditViewDialog(SelectedTodoItem);
            TodoItems.Add(SelectedTodoItem);
            //RetrieveTodoItems();
        }

        private void EditTodoItem()
        {
            if (SelectedTodoItem == null)
                return;

            _windowService.ShowTodoItemEditViewDialog(SelectedTodoItem!);
            RetrieveTodoItems();
        }

        private async void DeleteTodoItem()
        {
            if (SelectedTodoItems.Count == 0)
            {
                return;
            }

            var itemsToDelete = SelectedTodoItems.ToList();

            foreach (var item in itemsToDelete)
            {
                await _todoItemService.DeleteAsync(item.TodoItemId);
                TodoItems.Remove(item);
            }
        }
    }
}
