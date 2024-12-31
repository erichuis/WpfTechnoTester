using Domain.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost()]
        public async Task<ActionResult<UserDto>> Login(UserDto userDto)
        {
            if (userDto == null || userDto.Password == null)
            {
                return BadRequest("loginRequest or password can not be null");
            }

            var user = await _authenticationService.Login(userDto).ConfigureAwait(false);

            // In a real-world scenario, generate a token here
            return Ok(user);
        }
    }
}
