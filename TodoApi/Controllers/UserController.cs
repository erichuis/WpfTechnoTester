using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;
using Domain.Helpers;
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
            if(loginRequest == null || loginRequest.PasswordHashed == null)
            {
                return BadRequest("loginRequest or password can not be null");
            }
            var user = await _userRepository.GetByNameAsync(loginRequest.Username);

            if (user == null || user.PasswordHashed == null) 
            {
                return BadRequest("loginRequest or password can not be null");
            }

            if (!PasswordHelper.VerifyPassword(loginRequest.PasswordHashed, user.PasswordHashed))
            {
                return Unauthorized("Invalid username or password.");
            }

            // In a real-world scenario, generate a token here
            return Ok(user);
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

            return Ok(result);
        }
      

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
