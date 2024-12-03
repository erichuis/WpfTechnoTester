using Cybervision.Dapr.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginRequest)
        {
            var user = await _userRepository.GetByNameAsync(loginRequest.Username);
            //if (user == null || !PasswordHelper.VerifyPassword(loginRequest.PasswordHash, user.PasswordHash))
            //{
            //    return Unauthorized("Invalid username or password.");
            //}

            // In a real-world scenario, generate a token here
            return Ok("Login successful.");
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async Task<ActionResult<IAsyncEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var result = await _userRepository.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async Task<ActionResult<UserDto>> GetUserAsync(string id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            return Ok(result);
        }

        //[Authorize(Policy = "ApiScope")]
        [HttpPost()]
        public async Task<ActionResult<UserDto>> CreateUserAsync([FromBody] UserDto user)
        {
            // Check if the user already exists
            var existingUser = await _userRepository.GetByNameAsync(user.Username);
            if (existingUser != null)
            {
                return BadRequest("User already exists.");
            }
            //Todo move it
            // Hash the password
            //user.PasswordHashed = PasswordHelper.HashPassword(new NetworkCredential(string.Empty, user.Password).Password);

            // Save the user
            var result = await _userRepository.CreateAsync(user);

            return Ok("User registered successfully.");
        }
        //public async Task<ActionResult<TodoItem>> CreateTodoItemAsync(TodoItem todoItem)
        //{

        //    var newTodoItem = await _todoItemService.CreateTodoItem(todoItem);
        //    var result = CreatedAtAction(nameof(GetTodoItemAsync), new { id = newTodoItem.Id }, newTodoItem);
        //    var newResult = result.Value as TodoItem;

        //    if (newResult == null)
        //    {
        //        return BadRequest();
        //    }

        //    return newResult;
        //}

        [Authorize(Policy = "ApiScope")]
        [HttpPut()]
        public async Task<ActionResult<bool>> UpdateAsync(UserDto user)
        {
            if (user == null)
            {
                throw new NullReferenceException(nameof(User));
            }

            try
            {
                return await _userRepository.UpdateAsync(user);

            }
            catch (Exception)
            {
                return false;
            }
        }

        [Authorize(Policy = "ApiScope")]
        [HttpDelete()]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            try
            {
                return await _userRepository.DeleteAsync(id);

            }
            catch (Exception)
            {
                return false;
                //log something
            }
        }
    }
}
