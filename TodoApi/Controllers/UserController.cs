using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;
using Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost()]
        public async Task<ActionResult<UserDto>> Login(UserDto userDto)
        {
            if (userDto == null || userDto.Password == null)
            {
                return BadRequest("loginRequest or password can not be null");
            }

            var user = await _userService.Login(userDto).ConfigureAwait(false);

            // In a real-world scenario, generate a token here
            return Ok(user);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async IAsyncEnumerable<UserDto> GetAllUsersAsync()
        {
            var results = _userService.GetAllAsync();
            await foreach (var item in results)
            {
                yield return item;
            }
            //return Ok(result);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async Task<ActionResult<UserDto>> GetUserAsync(Guid id)
        {
            var result = await _userService.GetAsync(id).ConfigureAwait(false);
            return Ok(result);
        }

        //[Authorize(Policy = "ApiScope")]
        [HttpPost()]
        public async Task<ActionResult<UserDto>> CreateUserAsync(UserDto user)
        {
            // Check if the user already exists
            //var existingUser = await _userService.GetByNameAsync(user.Username).ConfigureAwait(false);
            //if (existingUser != null)
            //{
            //    return BadRequest("User already exists.");
            //}
            //Todo move it
            // Hash the password
            //user.PasswordHashed = PasswordHelper.HashPassword(new NetworkCredential(string.Empty, user.Password).Password);

            // Save the user
            var result = await _userService.CreateAsync(user).ConfigureAwait(false);

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
                var result = await _userService.UpdateAsync(user).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Authorize(Policy = "ApiScope")]
        [HttpDelete()]
        public async Task<ActionResult<bool>> DeleteAsync(Guid id)
        {
            try
            {
                return await _userService.DeleteAsync(id);

            }
            catch (Exception)
            {
                return false;
                //log something
            }
        }
    }
}
