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
        private readonly UserLogin _user;

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
            ValidationCtx = new ValidationContext(_user);
            SignupCommand = new RelayCommand((param) => ShowSignupView());
        }

        private void ShowSignupView()
        {
            _windowService.ShowNewUserSignupDialog();
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
                    OnPropertyChangedExt(nameof(Username), value);
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
                    _user.Password = value;
                    OnPropertyChangedExt(nameof(Password), value);
                }
            }
        }
    }
}