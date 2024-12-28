using Domain.Models;

namespace WpfTechnoTester.Services
{
    public interface IWindowService
    {
        bool ShowTodoItemEditViewDialog(TodoItem todoItem);
        bool ShowJournalEntryEditViewDialog(JournalEntry journalEntry);
        bool ShowNewUserSignupDialog();
        bool ShowUserLoginDialog();
    }
}
