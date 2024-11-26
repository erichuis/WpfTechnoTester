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
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemController(ITodoItemRepository todoItemService)
        {
            _todoItemRepository = todoItemService;
        }

        [HttpGet()]
        public async Task<ActionResult<string>> GetHelloWorldAsync(string name) => await Task.Run(() => "Hello " + name);

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async Task<ActionResult<IAsyncEnumerable<TodoItem>>> GetAllTodoItemsAsync()
        {
            var result = await _todoItemRepository.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async Task<ActionResult<TodoItem>> GetTodoItemAsync(string id)
        {
            var result = await _todoItemRepository.GetByIdAsync(id); 
            return Ok(result);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpPost()]
        public async Task<ActionResult<TodoItem>> CreateTodoItemAsync(TodoItem todoItem)
        {
            var newTodoItem = await _todoItemRepository.CreateAsync(todoItem);
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
                return await _todoItemRepository.UpdateAsync(TodoItem);
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
                return await _todoItemRepository.DeleteAsync(id);

            }
            catch (Exception)
            {
                return false;
                //log something
            }
        }
    }
}
