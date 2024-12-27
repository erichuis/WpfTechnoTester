using System.Windows;
using WpfTechnoTester.ViewModels;

namespace WpfTechnoTester.Views
{
    /// <summary>
    /// Interaction logic for TodoItemEditView.xaml
    /// </summary>
    public partial class TodoItemEditView : Window
    {
        public TodoItemEditView(TodoItemEditViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext != null)
            {
                var vm = ((TodoItemEditViewModel)DataContext);
                if (!vm.CanClose)
                {
                    e.Cancel = true;
                }

                DialogResult = !vm.IsCancelled;
            }
        }
    }
}
