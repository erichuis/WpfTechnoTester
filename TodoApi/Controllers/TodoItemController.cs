using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [HttpGet()]
        public async Task<ActionResult<string>> GetHelloWorldAsync(string name) => await Task.Run(() => "Hello " + name);

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async Task<ActionResult<List<TodoItem>>> GetAllTodoItemsAsync() => await _todoItemService.GetTodoItems();

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async Task<ActionResult<TodoItem>> GetTodoItemAsync(string id) => await _todoItemService.GetTodoItem(id);

        [Authorize(Policy = "ApiScope")]
        [HttpPost()]
        public async Task<ActionResult<TodoItem>> CreateTodoItemAsync(TodoItem todoItem)
        {
            
            var newTodoItem = await _todoItemService.CreateTodoItem(todoItem);
            var result = CreatedAtAction(nameof(GetTodoItemAsync), new { id = newTodoItem.Id }, newTodoItem);
            var newResult = result.Value as TodoItem;

            if (newResult == null)
            {
                return BadRequest();
            }

            return newResult;
        }

        [Authorize(Policy = "ApiScope")]
        [HttpPut()]
        public async Task<ActionResult<bool>> UpdateTodoItemAsync(TodoItem TodoItem)
        {
            if (TodoItem == null)
            {
                throw new NullReferenceException(nameof(TodoItem));
            }

            try
            {
                await _todoItemService.UpdateTodoItem(TodoItem);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        [Authorize(Policy = "ApiScope")]
        [HttpDelete()]
        public async Task<ActionResult<bool>> DeleteTodoItemAsync(string id)
        {
            try
            {
                await _todoItemService.DeleteTodoItem(id);
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
