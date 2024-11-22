using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Navigation;
using WpfTechnoTester.Clients;
using WpfTechnoTester.Commands;
using WpfTechnoTester.Views;


public class TodoItemViewModel : INotifyPropertyChanged
{
    private readonly IHttpAppClient _taskClient;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<TodoItem> TodoItems { get; set; }

    public List<TodoItem> SelectedTodoItems { get; set; }

    private string _title;
    private string _description;
    private bool _isCompleted;

    public RelayCommand AddTodoItemCommand { get; }
    public RelayCommand DeleteTaskCommand { get; }
    public RelayCommand UpdateTaskCommand { get; }
    public RelayCommand LoadTasksCommand { get; }
    public RelayCommand LoginCommand { get; }
    public RelayCommand LogoutCommand { get; }
    public SignUpCommand SignUpCommand { get; }


    public TodoItemViewModel(IHttpAppClient taskClient)
    {
        TodoItems = [];
        SelectedTodoItems = [];
        AddTodoItemCommand = new RelayCommand((param) => AddTodoItem(), null);
        DeleteTaskCommand = new RelayCommand((param) => DeleteTask(), null);
        UpdateTaskCommand = new RelayCommand((param) => UpdateTask(), null);
        LoadTasksCommand = new RelayCommand((param) => RetrieveTodoItems(), null);
        LoginCommand = new RelayCommand((param) => Login(), null);
        LogoutCommand = new RelayCommand((param) => Logout(), null);
        SignUpCommand = new SignUpCommand((param) => SignUp(), (param) => true);
        _title = string.Empty;
        _description = string.Empty;
        _taskClient = taskClient;
    }

    private bool CanRetrieve()
    {
        return _taskClient != null;
    }

    /// <summary>
    /// currently works for add, update, delete
    /// </summary>
    /// <returns></returns>
    private bool CanExecute()
    {
        return _taskClient != null &&
            string.IsNullOrEmpty(_title) &&
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

        var tasks = await _taskClient.GetAllTasksAsync();
        foreach (var task in tasks) TodoItems.Add(task);
    }

    private void SignUp()
    {
        if (_taskClient != null)
        {

            //UserLogin login = new UserLogin();
            _taskClient.GetToken();
            UserSignup signup = new UserSignup(null);
            signup.ShowDialog();
            //by machine

        }
    }

    private void Login()
    {
        if (_taskClient != null)
        {
            //UserLogin login = new UserLogin();
            _taskClient.GetToken();
            UserLogin login = new UserLogin(new WpfTechnoTester.ViewModels.UserLoginViewModel());
            login.ShowDialog();
            //by machine
        }
    }

    private void Logout()
    {
        _taskClient.Logout();
    }

    private void AddTodoItem()
    {
        if (string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Title))
            return;

        var newTask = new TodoItem()
        {
            Description = Description,
            Title = Title
        };

        var response = _taskClient.CreateTaskAsync(newTask).GetAwaiter().GetResult();
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
            await _taskClient.DeleteTaskByIdAsync(item.Id);
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

        await _taskClient.UpdateTaskAsync(item);

    }
}