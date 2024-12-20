﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
                if (!((TodoItemEditViewModel)DataContext).CanClose)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
