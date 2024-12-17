using System.Windows;
using System.Windows.Controls;
using WpfTechnoTester.ViewModels;

namespace WpfTechnoTester.Views
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin(UserLoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            { ((dynamic)DataContext).Password = ((PasswordBox)sender).SecurePassword; }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext != null)
            {
                if (!((UserLoginViewModel)DataContext).CanClose)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
