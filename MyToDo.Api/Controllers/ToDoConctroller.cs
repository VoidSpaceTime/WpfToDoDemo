using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Context.Repository;
using MyToDo.Api.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService toDoService;
        private readonly IMapper mapper;

        public ToDoController(IToDoService toDoService, IMapper mapper)
        {
            this.toDoService = toDoService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await toDoService.GetByIdAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll(QueryParameter queryParameter) => await toDoService.GetAllAsync(queryParameter);

        //[HttpGet]
        //public async Task<ApiResponse> Summary() => await toDoService.Summary();

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDoDto model) => await toDoService.AddAsync(mapper.Map<ToDo>(model));

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDoDto model) => await toDoService.UpdateAsync(mapper.Map<ToDo>(model));

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await toDoService.DeleteAsync(id);


    }
}
