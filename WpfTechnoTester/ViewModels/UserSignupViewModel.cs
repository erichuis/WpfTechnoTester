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
                    OnPropertyChanged(nameof(Username));
                    ValidateModel(nameof(Username), value);
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
                    OnPropertyChanged(nameof(Email));
                    ValidateModel(nameof(Username), value);
                }
            }
        }

        internal override void RaiseCanExecuteChange()
        {
            //   SubmitCommand.RaiseCanExecuteChanged();
        }

        protected override bool CanDoAction()
        {

            //these explicit validations should not be necessary
            ValidateModel(nameof(Username), _user.Username);
            ValidateModel(nameof(Email), _user.Email);
            ValidateModel(nameof(Password), _user.Password!);
            ValidateModel(nameof(PasswordVerified), _user.PasswordVerified!);
            return !HasErrors;
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
                    OnPropertyChanged(nameof(Password));
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

        private SecureString _passwordVerified = new NetworkCredential(string.Empty, string.Empty).SecurePassword;
        public SecureString PasswordVerified
        {
            get => _passwordVerified;
            set
            {
                if (_user.PasswordVerified != value)
                {
                    _user.PasswordVerified = value;
                    OnPropertyChanged(nameof(PasswordVerified));
                }
            }
        }
    }
}

