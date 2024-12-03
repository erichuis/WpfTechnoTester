using System.Security;
using WpfTechnoTester.Commands;
using WpfTechnoTester.Models;
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
            CancelCommand = new RelayCommand((param) => CancelForm());
        }

        private void AddUser()
        {
            var response = _userService.AddUserAsync(_user).GetAwaiter().GetResult();
        }

        private void CancelForm()
        {
            _cancelForm = true;
        }

        private void ResetForm()
        {
            Email = string.Empty;
            UserName = string.Empty;
            _user.Password?.Clear();
        }

        private string _userName;
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
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    _user.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        internal override void RaiseCanExecuteChange()
        {
            _canSave = _user.CanSave();
            SubmitCommand.RaiseCanExecuteChanged();
        }

        private bool _canSave;
        public bool CanSave
        {
            get { return _canSave; }
        }

        public bool CanClose
        {
            get
            {
                return _cancelForm || _user.CanSave();
            }
        }

        private SecureString _password;
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

        private SecureString _passwordVerified;
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

