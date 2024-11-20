namespace WpfTechnoTester.Clients
{
    public interface ITaskClient
    {
        Task<IEnumerable<TodoItem>> GetAllTasksAsync();
        Task<TodoItem> GetTaskByIdAsync(string id);
        Task<TodoItem> CreateTaskAsync(TodoItem item);
        Task<bool> DeleteTaskByIdAsync(string id);
        Task<bool> UpdateTaskAsync(TodoItem item);
        Task GetToken();
        Task<bool> Logout();
    }
}
