using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace WpfTechnoTester.ViewModels.Helpers
{
    public static class PasswordHelper
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
                "Password",
                typeof(SecureString),
                typeof(PasswordHelper),
                new FrameworkPropertyMetadata(new SecureString(), OnPasswordPropertyChanged));

        public static SecureString GetPassword(DependencyObject obj) => (SecureString)obj.GetValue(PasswordProperty);

        public static void SetPassword(DependencyObject obj, SecureString value) => obj.SetValue(PasswordProperty, value);

        private static void OnPasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                // Update the attached property with the new password value
                SetPassword(passwordBox, passwordBox.SecurePassword);
            }
        }

        private static void OnPasswordPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is PasswordBox passwordBox && passwordBox.SecurePassword != (SecureString)e.NewValue)
            {
                // Unsubscribe from the PasswordChanged event to avoid recursive calls
                passwordBox.PasswordChanged -= OnPasswordBoxPasswordChanged;

                SetPassword(obj, e.NewValue as SecureString);

                // Resubscribe to PasswordChanged
                passwordBox.PasswordChanged += OnPasswordBoxPasswordChanged;
            }
        }
    }
}
