using System.Windows;
using System.Windows.Controls;

namespace WpfTechnoTester.Views.Controls
{
    /// <summary>
    /// Interaction logic for UcLabelTextbox.xaml
    /// </summary>
    public partial class UcLabelTextbox : UserControl
    {
        public UcLabelTextbox()
        {
            InitializeComponent();
            ((FrameworkElement)Content).DataContext = this;
        }

        // Dependency Property for Label Text
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), 
                typeof(UcLabelTextbox), 
                new PropertyMetadata(string.Empty, SetLabelText));

        private static void SetLabelText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UcLabelTextbox control)
            {
                control.BaseLabel.Content = e.NewValue.ToString();
            }
        }

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        // Dependency Property for TextBox Text
        public static readonly DependencyProperty TextboxTextProperty =
            DependencyProperty.Register("TextboxText", 
                typeof(string), 
                typeof(UcLabelTextbox),
                new PropertyMetadata(string.Empty, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UcLabelTextbox control)
            {
                control.BaseTextbox.Text = e.NewValue.ToString();
            }
        }

        public string TextboxText
        {
            get => (string)GetValue(TextboxTextProperty);
            set => SetValue(TextboxTextProperty, value);
        }
    }
}
