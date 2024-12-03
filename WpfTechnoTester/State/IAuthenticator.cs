using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using WpfTechnoTester.Models;

namespace WpfTechnoTester.State
{
    public interface IAuthenticator
    {
        User CurrentUser { get; }
        bool IsLoggedIn { get; }
        Task<bool> Register(string email, string userName, SecureString password, SecureString confirmPassword);

        Task<bool> Login(string userName, SecureString password);
        void Logout();
    }
}
