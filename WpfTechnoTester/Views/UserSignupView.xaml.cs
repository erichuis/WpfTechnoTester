using System.Windows;
using System.Windows.Controls;
using WpfTechnoTester.ViewModels;

namespace WpfTechnoTester.Views
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserSignupView : Window
    {
        public UserSignupView(UserSignupViewModel viewModel)
        {
            InitializeComponent();
           
            //viewModel.HarvestPassword += (sender, args) => args.Password = this.pwdBox.SecurePassword;
            DataContext = viewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DataContext != null)
            {
                var vm = ((UserSignupViewModel)DataContext);
                if (!((UserSignupViewModel)DataContext).CanClose)
                {
                    e.Cancel = true;
                }
                DialogResult = !vm.IsCancelled;
            } 
        }
    }
}
