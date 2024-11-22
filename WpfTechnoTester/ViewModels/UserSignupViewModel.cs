using System.Security;
using System.Windows.Input;
using WpfTechnoTester.Commands;
using WpfTechnoTester.EventArgs;
using WpfTechnoTester.Models;

namespace WpfTechnoTester.ViewModels
{
    public class UserSignupViewModel : ViewModelBase
    {
        //private readonly IUserService _userService;
        private string _userName = string.Empty;
        private string _email = string.Empty;
        private string _emailVerified = string.Empty;
        private SecureString? _password;
        private User _user = new();

        public RelayCommand? SaveCommand { get; }

        public event EventHandler<HarvestPasswordEventArgs>? HarvestPassword;

        public UserSignupViewModel(/*IUserService userService*/)
        {
            SaveCommand = new RelayCommand((param) => _user.Save(), (param) => _user.CanSave());
            //_userService = userService;
        }

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
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
                    OnPropertyChanged(nameof(EmailVerified));
                }
            }
        }

        public SecureString Password
        {
            get => _password!;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
    }
}

