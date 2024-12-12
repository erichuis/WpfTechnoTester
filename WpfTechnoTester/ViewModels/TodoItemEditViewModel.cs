using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTechnoTester.Services;

namespace WpfTechnoTester.ViewModels
{
    public class TodoItemEditViewModel : ViewModelBase
    {
        private readonly ITodoItemService _todoItemService;
        public TodoItemEditViewModel(ITodoItemService todoItemService) 
        {
            _todoItemService = todoItemService;
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        private string _description = string.Empty; 
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        
        private bool _isCompleted = false; 
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        private void AddTodoItem()
        {
            if (string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Title))
                return;

            var todoItem = new TodoItem()
            {
                Description = Description,
                Title = Title
            };

            var response = _todoItemService.CreateAsync(todoItem).GetAwaiter().GetResult();
           
        }
    }
}
