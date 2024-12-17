using System.Net;
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

        private static void OnPasswordPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is PasswordBox passwordBox && passwordBox.SecurePassword != (SecureString)e.NewValue)
            {
                passwordBox.Password = new NetworkCredential(string.Empty, (SecureString)e.NewValue).Password;
            }
        }
    }
}
