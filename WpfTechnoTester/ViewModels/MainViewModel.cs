using Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfTechnoTester.Commands;
using WpfTechnoTester.Services;
using WpfTechnoTester.State;


public class MainViewModel : INotifyPropertyChanged
{
    
    private readonly IWindowService _windowService;
    private readonly INavigator _navigator;
    private readonly IAuthenticator _authenticator;

    public event PropertyChangedEventHandler? PropertyChanged;


    private string _title;
    private string _description;
    private bool _isCompleted;

    public INavigator Navigator
    {
        get { return _navigator; }
    }

    
    public RelayCommand LoginCommand { get; }
    public RelayCommand LogoutCommand { get; }
    public RelayCommand SignUpCommand { get; }


    public MainViewModel(
        ITodoItemService todoItemService, 
        IWindowService windowService, 
        INavigator navigator,
        IAuthenticator authenticator)
    {
        
        
        LoginCommand = new RelayCommand((param) => Login(), null);
        LogoutCommand = new RelayCommand((param) => Logout(), null);
        SignUpCommand = new RelayCommand((param) => SignUp(), (param) => true);
        _title = string.Empty;
        _description = string.Empty;
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

}