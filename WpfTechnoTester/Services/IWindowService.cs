using Domain.Models;

namespace WpfTechnoTester.Services
{
    public interface IWindowService
    {
        bool ShowTodoItemEditViewDialog(TodoItem todoItem);
        bool ShowNewUserSignupDialog();
        bool ShowUserLoginDialog();
    }
}
