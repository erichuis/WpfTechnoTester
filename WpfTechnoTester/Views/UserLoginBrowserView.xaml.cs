using System.Web;
using System.Windows;

namespace WpfTechnoTester.Views
{
    /// <summary>
    /// Interaction logic for UserLoginBrowserView.xaml
    /// </summary>
    public partial class UserLoginBrowserView : Window
    {
        public UserLoginBrowserView()
        {
            InitializeComponent();
            WebBrowser.Navigate("https://localhost:5001/api/auth/login");

            WebBrowser.Navigated += (s, e) =>
            {
                if (e.Uri.ToString().StartsWith("http://localhost:7890/"))
                {
                    var query = e.Uri.Query;
                    var token = HttpUtility.ParseQueryString(query).Get("token");

                    if (!string.IsNullOrEmpty(token))
                    {
                        MessageBox.Show("Login Successful! Token: " + token);
                    }
                }
            };
        }
    }
}
