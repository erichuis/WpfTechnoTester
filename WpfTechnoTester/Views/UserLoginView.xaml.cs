using System.Windows;
using System.Windows.Controls;
using WpfTechnoTester.ViewModels;

namespace WpfTechnoTester.Views
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLoginView : Window
    {
        public UserLoginView(UserLoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
       
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext != null)
            {
                var vm = ((UserLoginViewModel)DataContext);
                if (!vm.CanClose)
                {
                    e.Cancel = true;
                }

               DialogResult = !vm.IsCancelled;
            }
        }
    }
}
