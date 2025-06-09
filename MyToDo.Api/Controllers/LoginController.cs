using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Service;

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

        [HttpGet]
        public async Task<ApiResponse> Login(string account, string password)
        {
            return await loginService.LoginAsync(account, password);
        }
        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] Shared.Dtos.UserDto userDto)
        {
            return await loginService.RegisterAsync(userDto);
        }
    }
}
