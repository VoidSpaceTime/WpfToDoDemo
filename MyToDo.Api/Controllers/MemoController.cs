using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService memoService;
        private readonly IMapper mapper;
        public MemoController(IMemoService memoService, IMapper mapper)
        {
            this.memoService = memoService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await memoService.GetByIdAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery]QueryParameter queryParameter) => await memoService.GetAllAsync(queryParameter);

        //[HttpGet]
        //public async Task<ApiResponse> Summary() => await memoService.Summary();

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] MemoDto model) => await memoService.AddAsync(mapper.Map<Memo>(model));

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] MemoDto model) => await memoService.UpdateAsync(mapper.Map<Memo>(model));

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await memoService.DeleteAsync(id);
    }
}
