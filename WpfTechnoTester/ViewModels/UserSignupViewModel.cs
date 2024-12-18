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
        private bool _cancelForm = false;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand ResetCommand { get; }
        public RelayCommand CancelCommand { get; }

        public UserSignupViewModel(IUserService userService)
        {
            _userService = userService;

            SubmitCommand = new RelayCommand((param) => AddUser(), (param) => CanSave);
            ResetCommand = new RelayCommand((param) => ResetForm());
            CancelCommand = new RelayCommand((param) => CancelSignup());
        }

        private void AddUser()
        {
            var response = _userService.CreateAsync(_user).GetAwaiter().GetResult();
            _saveIsSuccesful = true;
        }

        private void CancelSignup()
        {
            _cancelForm = true;
        }

        private void ResetForm()
        {
            _user.Username = string.Empty;
            _user.Email = string.Empty;
            _user.Password?.Clear();
            _user.PasswordVerified?.Clear();
        }

        //private string _userName = string.Empty;
        public string Username
        {
            get => _user.Username;
            set
            {
                if (_user.Username != value)
                {
                    //_userName = value;
                    _user.Username = value;
                    OnPropertyChanged(nameof(Username));
                    ValidateModel(nameof(Username), value);
                }
            }
        }
        //private string _email = string.Empty;
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

        private bool _saveIsSuccesful;
        public bool CanSave
        {
            get {
                //these explicit validations should not be necessary
                ValidateModel(nameof(Username), _user.Username);
                ValidateModel(nameof(Email), _user.Email);
                ValidateModel(nameof(Password), _user.Password!);
                ValidateModel(nameof(PasswordVerified), _user.PasswordVerified!);
                return !HasErrors; 
            }
        }

        public bool CanClose
        {
            get
            {
                return _cancelForm || _saveIsSuccesful;
            }
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

