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

        //public event EventHandler<HarvestPasswordEventArgs>? HarvestPassword;

        public UserSignupViewModel(IUserService userService)
        {
            _userService = userService;
            SubmitCommand = new RelayCommand(async (param) =>  await userService.AddUserAsync(_user), (param) => _user.CanSave());
            ResetCommand = new RelayCommand((param) => ResetForm());
            CancelCommand = new RelayCommand((param) => CancelForm());
        }

        private void CancelForm()
        {
            _cancelForm = true;
        }

        private void ResetForm()
        {
            Email = string.Empty;
            EmailVerified = string.Empty;
            UserName = string.Empty;
            _user.Password?.Clear();
        }

        public string UserName
        {
            get => _user.UserName;
            set
            {
                if (_user.UserName != value)
                {
                    _user.UserName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        public string Email
        {
            get => _user!.Email;
            set
            {
                if (_user.Email != value)
                {
                    _user.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string EmailVerified
        {
            get => _user.EmailVerified;
            set
            {
                if (_user.EmailVerified != value)
                {
                    _user.EmailVerified = value;
                    OnPropertyChanged(nameof(EmailVerified));
                }
            }
        }

        public bool CanClose
        {
            get
            {
                return _cancelForm || _user.CanSave();
            }
        }

        public SecureString Password
        {
            set
            {
                if (_user.Password != value)
                {
                    _user.Password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
    }
}

