using System.Windows;
using WpfTechnoTester.ViewModels;

namespace WpfTechnoTester.Views
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserSignup : Window
    {
        public UserSignup(UserSignupViewModel viewModel)
        {
            InitializeComponent();
            if(viewModel == null)
            {
                viewModel = new UserSignupViewModel();
            }
            viewModel.HarvestPassword += (sender, args) => args.Password = this.pwdBox.SecurePassword;
            DataContext = viewModel;
        }
    }
}
