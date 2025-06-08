using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Context.Repository;
using MyToDo.Api.Service;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService toDoService;

        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await toDoService.GetByIdAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll() => await toDoService.GetAllAsync();

        //[HttpGet]
        //public async Task<ApiResponse> Summary() => await toDoService.Summary();

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDo model) => await toDoService.AddAsync(model);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDo model) => await toDoService.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await toDoService.DeleteAsync(id);


    }
}
