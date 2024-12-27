using Domain.Models;
using System.ComponentModel.DataAnnotations;
using WpfTechnoTester.Services;

namespace WpfTechnoTester.ViewModels
{
    public class TodoItemEditViewModel : ViewModelBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly TodoViewModel _todoViewModel;

        public TodoItemEditViewModel(ITodoItemService todoItemService, TodoViewModel todoViewModel)
        {
            _todoItemService = todoItemService;
            _todoViewModel = todoViewModel;
            if(_todoViewModel.SelectedTodoItem == null)
            {
                throw new ArgumentException("The selected todo Item can not be null");
            }
            ValidationCtx = new ValidationContext(TodoItem);
        }

        private TodoItem TodoItem
        {
            get
            {
                if (_todoViewModel.SelectedTodoItem == null)
                {
                    throw new Exception("Selected todo item is null"); 
                }
                return _todoViewModel.SelectedTodoItem;
            }
        }

        public string Title
        {
            get => TodoItem.Title;
            set
            {
                if (TodoItem.Title != value)
                {
                    TodoItem.Title = value;
                    OnPropertyChangedExt(nameof(Title), value);
                }
            }
        }
        public string Description
        {
            get => TodoItem.Description;
            set
            {
                if (TodoItem.Description != value)
                {
                    TodoItem.Description = value;
                    OnPropertyChangedExt(nameof(Description), value);
                }
            }
        }

        public DateTime? DateStarted
        {
            get => TodoItem.DateStarted;
            set
            {
                if (TodoItem.DateStarted != value)
                {
                    TodoItem.DateStarted = value;
                    OnPropertyChangedExt(nameof(DateStarted), value);
                }
            }
        }

        public DateTime? DateCompleted
        {
            get => TodoItem.DateCompleted;
            set
            {
                if (TodoItem.DateCompleted != value)
                {
                    TodoItem.DateCompleted = value;
                    OnPropertyChangedExt(nameof(DateCompleted), value);
                }
            }
        }

        public int InProgress
        {
            get => TodoItem.InProgress;
            set
            {
                if (TodoItem.InProgress != value)
                {
                    TodoItem.InProgress = value;
                    OnPropertyChangedExt(nameof(InProgress), value);
                }
            }
        }
        //protected override bool CanDoAction()
        //{
        //    //these explicit validations should not be necessary
        //    //ValidateModel(nameof(Description), TodoItem.Description);
        //    //ValidateModel(nameof(Title), TodoItem.Title);
        //    ValidateModel(ValidationCtx)
        //    return !HasErrors;
        //}

        protected override void DoAction()
        {
            if (TodoItem.TodoItemId != Guid.Empty)
            {
                var response = _todoItemService.UpdateAsync(TodoItem).GetAwaiter().GetResult();
                if (!response )
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
