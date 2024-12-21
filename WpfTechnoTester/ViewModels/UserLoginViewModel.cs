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

        public RelayCommand SignupCommand { get; }

        public UserLoginViewModel(IAuthenticator authenticator, IWindowService windowService)
        {
            _authenticator = authenticator;
            _windowService = windowService;
            _user = new()
            {
                Username = string.Empty,
                Password = new NetworkCredential(string.Empty, string.Empty).SecurePassword
            };
            
            SignupCommand = new RelayCommand((param) => ShowSignupView());
        }

        private void ShowSignupView()
        {
            _windowService.ShowNewUserSignupDialog();
        }

        protected override bool CanDoAction()
        {
            //these explicit validations should not be necessary
            ValidateModel(nameof(Username), _user.Username);
            ValidateModel(nameof(Password), _user.Password!);
            return !HasErrors;
        }

        protected override void DoAction()
        {
            var success = _authenticator.Login(_user.Username!, _user.Password!).GetAwaiter().GetResult();
            if (!success)
            {
                ActionSucceeded = false;
                return;
            }
            
            ActionSucceeded = true;
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