using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TaskService
    {
        private readonly IMongoCollection<TaskItem> _tasks;

        public TaskService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("TodoAppDb"));
            var database = client.GetDatabase("TodoApp");
            _tasks = database.GetCollection<TaskItem>("Tasks");
        }

        public async Task<List<TaskItem>> GetTasks() => await _tasks.Find(task => true).ToListAsync();

        public async Task<TaskItem> GetTask(string id) => await _tasks.Find(task => task.Id == id).FirstOrDefaultAsync();

        public async Task<TaskItem> CreateTask(TaskItem task)
        {
            await _tasks.InsertOneAsync(task);
            return task;
        }

        public async Task UpdateTask(TaskItem updatedTask) =>
            await _tasks.ReplaceOneAsync(task => task.Id == updatedTask.Id, updatedTask);

        public async Task DeleteTask(string id) => await _tasks.DeleteOneAsync(task => task.Id == id);
    }
}
