using System.Windows;

namespace WpfTechnoTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(TodoItemViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var viewmodel = (TodoItemViewModel)DataContext;
            viewmodel.SelectedTodoItems = lvTasks.SelectedItems
                .Cast<TodoItem>()
                .ToList();

            if (lvTasks.SelectedItems.Count == 1)
            {
                viewmodel.Title = viewmodel.SelectedTodoItems[0].Title;
                viewmodel.Description = viewmodel.SelectedTodoItems[0].Description;
            }
            else
            {
                viewmodel.Title = string.Empty; 
                viewmodel.Description = string.Empty;
            }
        }
    }
}