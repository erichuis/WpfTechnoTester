using System.Windows;
using WpfTechnoTester.ViewModels;

namespace WpfTechnoTester.Views
{
    /// <summary>
    /// Interaction logic for JournalItemEditView.xaml
    /// </summary>
    public partial class JournalEntryEditView : Window
    {
        public JournalEntryEditView(JournalEntryEditViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext != null)
            {
                var vm = ((JournalEntryEditViewModel)DataContext);
                if (!vm.CanClose)
                {
                    e.Cancel = true;
                }

                DialogResult = !vm.IsCancelled;
            }
        }
    }
}
