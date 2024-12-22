using Domain.Models;
using System.ComponentModel.DataAnnotations;
using WpfTechnoTester.Services;

namespace WpfTechnoTester.ViewModels
{
    public class TodoItemEditViewModel : ViewModelBase
    {
        private readonly ITodoItemService _todoItemService;
        private TodoItem _todoItem = new() { Description = string.Empty, Title = string.Empty };

        public TodoItemEditViewModel(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public TodoItem TodoItem
        {
            get { return _todoItem; }
            set { _todoItem = value; }
        }

        public string Title
        {
            get => _todoItem.Title;
            set
            {
                if (_todoItem.Title != value)
                {
                    _todoItem.Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public string Description
        {
            get => _todoItem.Description;
            set
            {
                if (_todoItem.Description != value)
                {
                    _todoItem.Description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public DateTime? DateStarted
        {
            get => _todoItem.DateStarted;
            set
            {
                if (_todoItem.DateStarted != value)
                {
                    _todoItem.DateStarted = value;
                    OnPropertyChanged(nameof(DateStarted));
                }
            }
        }

        public DateTime? DateCompleted
        {
            get => _todoItem.DateCompleted;
            set
            {
                if (_todoItem.DateCompleted != value)
                {
                    _todoItem.DateCompleted = value;
                    OnPropertyChanged(nameof(DateCompleted));
                }
            }
        }

        public int InProgress
        {
            get => _todoItem.InProgress;
            set
            {
                if (_todoItem.InProgress != value)
                {
                    _todoItem.InProgress = value;
                    OnPropertyChanged(nameof(InProgress));
                }
            }
        }
        protected override bool CanDoAction()
        {
            //these explicit validations should not be necessary
            ValidateModel(nameof(Description), _todoItem.Description);
            ValidateModel(nameof(Title), _todoItem.Title);
            return !HasErrors;
        }

        public void ValidateModel(string propertyName, object value)
        {
            var context = new ValidationContext(_todoItem)
            {
                MemberName = propertyName,
            };
            var results = new List<ValidationResult>();

            // Perform validation
            if (!Validator.TryValidateProperty(value, context, results))
            {
                foreach (var result in results)
                {
                    AddError(propertyName, result.ErrorMessage ?? "An error fix this");
                }
            }
        }

        protected override void DoAction()
        {
            if (_todoItem.TodoItemId != Guid.Empty)
            {
                var response = _todoItemService.UpdateAsync(_todoItem).GetAwaiter().GetResult();
                if (response == null)
                {
                    return;
                    //Todo do logging and show error dialog...with logging ;-)
                }
            }
            else
            {
                var todoItem = new TodoItem()
                {
                    Description = Description,
                    Title = Title
                };
    
                var response = _todoItemService.CreateAsync(todoItem).GetAwaiter().GetResult();
                if (response == null)
                {
                    return;
                    //Todo do logging and show error dialog...with logging ;-)
                }
            }

            
            ActionSucceeded = true;
        }
    }
}
