using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechnoTester.Services
{
    public interface IWindowService
    {
        void ShowEditTodoItemViewDialog();
        void ShowNewUserSignupDialog();
        bool ShowUserLoginDialog();
    }
}
