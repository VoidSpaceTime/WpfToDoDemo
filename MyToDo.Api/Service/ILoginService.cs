
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(UserDto userDto);
        Task<ApiResponse> RegisterAsync(UserDto userDto);
    }
}
