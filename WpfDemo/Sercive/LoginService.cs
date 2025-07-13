using MyToDo.Shared;
using MyToDo.Shared.Dtos;

namespace WpfDemo.Sercive
{
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient restClient;
        private string serviceName = "Login";
        public LoginService(HttpRestClient restClient)
        {
            this.restClient = restClient;
        }
        public async Task<ApiResponse<UserDto>> LoginAsync(UserDto userDto)
        {
            BaseRequest baseRequest = new BaseRequest
            {
                Metod = RestSharp.Method.Post,
                Route = $"api/{serviceName}/Login",
                ContentType = "application/json",
                Parameters = userDto
            };
            return await restClient.ExcuteAsync<UserDto>(baseRequest);
        }

        public async Task<ApiResponse> RegisterAsync(UserDto userDto)
        {
            BaseRequest baseRequest = new BaseRequest
            {
                Metod = RestSharp.Method.Post,
                Route = $"api/{serviceName}/Register",
                // 明确指定ContentType为application/json，确保后端能正确解析
                ContentType = "application/json",
                Parameters = userDto
            };
            return await restClient.ExcuteAsync(baseRequest);
        }
    }
}
