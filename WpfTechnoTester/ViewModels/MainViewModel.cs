using Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TodoApi.Services;
using WpfTechnoTester.Clients;
using WpfTechnoTester.Commands;
using WpfTechnoTester.Services;
using WpfTechnoTester.State;


public class MainViewModel : INotifyPropertyChanged
{
    private readonly ITodoItemService _todoItemService;
    private readonly IWindowService _windowService;
    private readonly INavigator _navigator;
    private readonly IAuthenticator _authenticator;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<TodoItem> TodoItems { get; set; }

    public List<TodoItem> SelectedTodoItems { get; set; }

    private string _title;
    private string _description;
    private bool _isCompleted;

    public INavigator Navigator
    {
        get { return _navigator; }
    }

    public RelayCommand AddTodoItemCommand { get; }
    public RelayCommand DeleteTaskCommand { get; }
    public RelayCommand UpdateTaskCommand { get; }
    public RelayCommand LoadTasksCommand { get; }
    public RelayCommand LoginCommand { get; }
    public RelayCommand LogoutCommand { get; }
    public RelayCommand SignUpCommand { get; }


    public MainViewModel(
        ITodoItemService todoItemService, 
        IWindowService windowService, 
        INavigator navigator,
        IAuthenticator authenticator)
    {
        TodoItems = [];
        SelectedTodoItems = [];
        AddTodoItemCommand = new RelayCommand((param) => AddTodoItem(), null);
        DeleteTaskCommand = new RelayCommand((param) => DeleteTask(), null);
        UpdateTaskCommand = new RelayCommand((param) => UpdateTask(), null);
        LoadTasksCommand = new RelayCommand((param) => RetrieveTodoItems(), null);
        LoginCommand = new RelayCommand((param) => Login(), null);
        LogoutCommand = new RelayCommand((param) => Logout(), null);
        SignUpCommand = new RelayCommand((param) => SignUp(), (param) => true);
        _title = string.Empty;
        _description = string.Empty;
        _todoItemService = todoItemService;
        _windowService = windowService;
        _navigator = navigator;
        _authenticator  = authenticator;
        _navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Home);
    }

    private bool CanRetrieve()
    {
        return _authenticator.IsLoggedIn;
    }

    /// <summary>
    /// currently works for add, update, delete
    /// </summary>
    /// <returns></returns>
    private bool CanExecute()
    {
        return string.IsNullOrEmpty(_title) &&
               string.IsNullOrEmpty(_description);
    }


    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //AddTodoItemCommand.OnCanExecuteChanged();
    }

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

    private async void RetrieveTodoItems()
    {
        TodoItems.Clear();

        var tasks = await _todoItemService.GetAllAsync();
        foreach (var task in tasks) TodoItems.Add(task);
    }

    private void SignUp()
    {
        _windowService.ShowNewUserSignup();
    }

    private void Login()
    {
        _windowService.ShowUserLogin();
    }

    private void Logout()
    {
        _authenticator.Logout();
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
        if (response != null)
        {
            TodoItems.Add(response);
        }
    }

    private async void DeleteTask()
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

    private async void UpdateTask()
    {
        if (SelectedTodoItems.Count != 1)
        {
            return;
        }

        var item = SelectedTodoItems.Single();

        item.Title = Title;
        item.Description = Description;
        item.IsCompleted = IsCompleted;

        await _todoItemService.UpdateAsync(item);

    }
}