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
        public async Task<ApiResponse> LoginAsync(UserDto userDto)
        {
            BaseRequest baseRequest = new BaseRequest
            {
                Metod = RestSharp.Method.Post,
                Route = $"api/{serviceName}/Login",
                Parameters = userDto
            };
            return await restClient.ExcuteAsync(baseRequest);
        }

        public async Task<ApiResponse> RegisterAsync(UserDto userDto)
        {
            BaseRequest baseRequest = new BaseRequest
            {
                Metod = RestSharp.Method.Post,
                Route = $"api/{serviceName}/Register",
                Parameters = userDto
            };
            return await restClient.ExcuteAsync(baseRequest);
        }
    }
}
