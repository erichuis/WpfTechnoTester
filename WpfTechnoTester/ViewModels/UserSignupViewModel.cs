using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;
using WpfTechnoTester.Commands;
using WpfTechnoTester.Services;

namespace WpfTechnoTester.ViewModels
{
    public class UserSignupViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly User _user = new();

        public RelayCommand ResetCommand { get; }

        public UserSignupViewModel(IUserService userService)
        {
            _userService = userService;

            ResetCommand = new RelayCommand((param) => ResetForm());
            ValidationCtx = new ValidationContext(_user);
        }

        protected override void DoAction()
        {
            var response = _userService.CreateAsync(_user).GetAwaiter().GetResult();
            ActionSucceeded = true;
        }

        private void ResetForm()
        {
            _user.Username = string.Empty;
            _user.Email = string.Empty;
            _user.Password?.Clear();
            _user.PasswordVerified?.Clear();
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
        public string Email
        {
            get => _user.Email;
            set
            {
                if (_user.Email != value)
                {
                    _user.Email = value;
                    OnPropertyChangedExt(nameof(Email), value);
                }
            }
        }

        internal override void RaiseCanExecuteChange()
        {
            //   SubmitCommand.RaiseCanExecuteChanged();
        }

        private SecureString _password = new NetworkCredential(string.Empty, string.Empty).SecurePassword;
        public SecureString Password
        {
            get => _password;
            set
            {
                if (_user.Password != value)
                {
                    _user.Password = value;
                    OnPropertyChangedExt(nameof(Password), value);
                }
            }
        }

        private SecureString _passwordVerified = new NetworkCredential(string.Empty, string.Empty).SecurePassword;
        public SecureString PasswordVerified
        {
            get => _passwordVerified;
            set
            {
                if (_user.PasswordVerified != value)
                {
                    _user.PasswordVerified = value;
                    OnPropertyChangedExt(nameof(PasswordVerified), value);
                }
            }
        }
    }
}

