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
        public RelayCommand DeleteTaskCommand { get; }
        public RelayCommand UpdateTaskCommand { get; }
        public RelayCommand LoadTasksCommand { get; }

        public TodoViewModel(ITodoItemService todoItemService, 
            IWindowService windowService)
        {
            TodoItems = [];
            SelectedTodoItems = [];
            _todoItemService = todoItemService;
            _windowService = windowService;
            
            AddTodoItemCommand = new RelayCommand((param) => AddTodoItem(), null);
            DeleteTaskCommand = new RelayCommand((param) => DeleteTodoItem(), null);
            UpdateTaskCommand = new RelayCommand((param) => EditTodoItem(), null);
            LoadTasksCommand = new RelayCommand((param) => RetrieveTodoItems(), null);
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
            _windowService.ShowEditTodoItemViewDialog();
            //Todo TodoItems.Add(response); or requery
        }

        private void EditTodoItem()
        {
            //Todo pass the item to be edited
            _windowService.ShowEditTodoItemViewDialog();
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
