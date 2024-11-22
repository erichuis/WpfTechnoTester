using System.Windows;
using System.Windows.Controls;
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
           
            //viewModel.HarvestPassword += (sender, args) => args.Password = this.pwdBox.SecurePassword;
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            { ((dynamic)DataContext).Password = ((PasswordBox)sender).SecurePassword; }
        }
    }
}
