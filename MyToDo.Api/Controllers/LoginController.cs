using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Extensions;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;
        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<ApiResponse> Login([FromBody] UserDto userDto)
        {
            userDto.Password = userDto.Password.GetMD5();
            return await loginService.LoginAsync(userDto);
        }
        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] UserDto userDto)
        {
            userDto.Password = userDto.Password.GetMD5();
            return await loginService.RegisterAsync(userDto);
        }
    }
}
