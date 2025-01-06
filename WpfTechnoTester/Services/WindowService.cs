using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using WpfTechnoTester.Clients;
using WpfTechnoTester.Views;

namespace WpfTechnoTester.Services
{
    public class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool ShowTodoItemEditViewDialog(TodoItem todoItem)
        {
            // Resolve the NewWindow from the service provider
            var window = _serviceProvider.GetRequiredService<TodoItemEditView>();

            // Optionally set properties or initialize if needed
            var dialogResult = window.ShowDialog(); // Open the window modal

            return dialogResult ?? false;
        }

        public bool ShowJournalEntryEditViewDialog(JournalEntry journalEntry)
        {
            // Resolve the NewWindow from the service provider
            var window = _serviceProvider.GetRequiredService<JournalEntryEditView>();

            // Optionally set properties or initialize if needed
            var dialogResult = window.ShowDialog(); // Open the window modal

            return dialogResult ?? false;
        }

        public bool ShowNewUserSignupDialog()
        {
            // Resolve the NewWindow from the service provider
            var window = _serviceProvider.GetRequiredService<UserSignupView>();

            // Optionally set properties or initialize if needed
            var dialogResult = window.ShowDialog(); // Open the window modal

            return dialogResult ?? false;
        }

        public bool ShowUserLoginDialog()
        {
            var oidcClient = new OidcLoginService();
            var result = oidcClient.LoginAsync().GetAwaiter().GetResult();
            return true;
            //// Resolve the NewWindow from the service provider
            //var window = _serviceProvider.GetRequiredService<UserLoginBrowserView>();

            //// Optionally set properties or initialize if needed
            //var dialogResult = window.ShowDialog(); // Open the window modal

            //return dialogResult ?? false;
        }
    }
}
