using Domain.Models;
using System.ComponentModel.DataAnnotations;
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
                Username = string.Empty,
                Password = new NetworkCredential(string.Empty, string.Empty).SecurePassword
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
                return _cancelLogin || _loginSuccess;
            }
        }
        private bool CanLogin()
        {
            //these explicit validations should not be necessary
            ValidateModel(nameof(Username), _user.Username);
            ValidateModel(nameof(Password), _user.Password!);
            return !HasErrors;
        }

        bool _loginSuccess = false;
        private void Login()
        {
            var success = _authenticator.Login(_user.Username!, _user.Password!).GetAwaiter().GetResult();
            if (!success)
            {
                _loginSuccess = false;
                return;
            }
            _loginSuccess = true;
        }

        public string Username
        {
            get => _user.Username;
            set
            {
                if (_user.Username != value)
                {
                    _user.Username = value;
                    OnPropertyChanged(nameof(Username));
                    ValidateModel(nameof(Username), value);
                }
            }
        }

        public void ValidateModel(string propertyName, object value)
        {
            var context = new ValidationContext(_user)
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

        //private SecureString _password = new NetworkCredential(string.Empty, string.Empty).SecurePassword;
        public SecureString Password
        {
            get => _user.Password!;
            set
            {
                if (_user.Password != value)
                {
                    //_password = value;
                    _user.Password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
    }
}