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

        public RelayCommand SaveCommand { get; }

        //public event EventHandler<HarvestPasswordEventArgs>? HarvestPassword;

        public UserSignupViewModel(IUserService userService)
        {
            _userService = userService;
            SaveCommand = new RelayCommand(async (param) =>  await userService.AddUserAsync(_user), (param) => _user.CanSave());
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

        //public SecureString Password { private get; set; }

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

