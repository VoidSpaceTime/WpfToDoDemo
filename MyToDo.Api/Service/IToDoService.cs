using MyToDo.Api.Context;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Service
{
    public interface IToDoService : IBaseService<ToDo>
    {
        Task<ApiResponse> GetFliterAll(ToDoParameter toDoParameter);
    }
}
