using Domain.Models;
using System.Net;
using System.Security;
using WpfTechnoTester.Commands;
using WpfTechnoTester.Services;
using WpfTechnoTester.State;

namespace WpfTechnoTester.ViewModels
{
    public class UserLoginViewModel : ViewModelBase
    {
        
        private readonly IAuthenticator _authenticator;
        private readonly IWindowService _windowService;
        private readonly User _user;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand SignupCommand { get; }
        public RelayCommand CancelCommand { get; }

        public UserLoginViewModel(IAuthenticator authenticator, IWindowService windowService)
        {
            _authenticator = authenticator;
            _windowService = windowService;
            _user = new()
            {
                UserName = string.Empty
            };

            SubmitCommand = new RelayCommand((param) => Login(), (param) => CanLogin());
            SignupCommand = new RelayCommand((param) => ShowSignupView());
            CancelCommand = new RelayCommand((param) => CancelLogin());
        }

        private void ShowSignupView()
        {
            _windowService.ShowNewUserSignupDialog();
        }

        private bool _cancelLogin = false;
        private void CancelLogin()
        {
            _cancelLogin = true;
        }

        public bool CanClose
        {
            get
            {
                return _cancelLogin || _user.CanLogin();
            }
        }
        private bool CanLogin()
        {
            return _user.CanLogin();
        }

        private void Login()
        {
            throw new NotImplementedException();
        }

        private string _userName = string.Empty;
        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    _user.UserName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        private SecureString _password = new NetworkCredential(string.Empty, string.Empty).SecurePassword;
        public SecureString Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    _user.Password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
    }
}