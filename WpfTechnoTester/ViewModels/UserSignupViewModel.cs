using System.Security;
using System.Windows.Input;
using WpfTechnoTester.Commands;
using WpfTechnoTester.EventArgs;
using WpfTechnoTester.Models;
using WpfTechnoTester.Services;

namespace WpfTechnoTester.ViewModels
{
    public class UserSignupViewModel : ViewModelBase
    {
        //private readonly IUserService _userService;
        private string _userName = string.Empty;
        private string _email = string.Empty;
        private string _emailVerified = string.Empty;
        private SecureString? _password;
        private IUserService _userService;
        private User _user = new();

        public RelayCommand? SaveCommand { get; }

        //public event EventHandler<HarvestPasswordEventArgs>? HarvestPassword;

        public UserSignupViewModel(IUserService userService)
        {
            _userService = userService;
            SaveCommand = new RelayCommand(async (param) =>  await userService.AddUserAsync(_user), (param) => _user.CanSave());
        }

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

        public string EmailVerified
        {
            get => _emailVerified;
            set
            {
                if (_emailVerified != value)
                {
                    _emailVerified = value;
                    _user.EmailVerified = value;
                    OnPropertyChanged(nameof(EmailVerified));
                }
            }
        }

        //public SecureString Password { private get; set; }

        public SecureString Password
        {
            get => _password!;
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

