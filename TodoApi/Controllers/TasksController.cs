using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("HelloWorldAsync")]
        public async Task<ActionResult<string>> GetHelloWorldAsync(string name) => await Task.Run(() => "Hello " + name);

        [Authorize(Policy = "ApiScope")]
        [HttpGet("GetAllTasks")]
        public async Task<ActionResult<List<TaskItem>>> GetAllTasksAsync() => await _taskService.GetTasks();

        //[Authorize(Policy = "ApiScope")]
        [HttpGet("GetTask{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskAsync(string id) => await _taskService.GetTask(id);

        //[Authorize(Policy = "ApiScope")]
        [HttpPost()]
        public async Task<ActionResult<TaskItem>> CreateTaskAsync(TaskItem task)
        {
            
            var newTask = await _taskService.CreateTask(task);
            var result = CreatedAtAction(nameof(GetTaskAsync), new { id = newTask.Id }, newTask);
            var newResult = result.Value as TaskItem;

            if(newResult == null)
            {
                return BadRequest();
            }

            return newResult;
        }

        //[Authorize(Policy = "ApiScope")]
        [HttpPut("UpdateTask{task}")]
        public async Task<ActionResult<bool>> UpdateTaskAsync(TaskItem task)
        {
            if (task == null)
            {
                throw new NullReferenceException(nameof(task));
            }

            try
            {
                await _taskService.UpdateTask(task);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        //[Authorize(Policy = "ApiScope")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTaskAsync(string id)
        {
            try
            {
                await _taskService.DeleteTask(id);
                return true;

            }
            catch (Exception)
            {
                return false;
                //log something
            }
        }
    }
}
