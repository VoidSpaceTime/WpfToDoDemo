using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Sercive
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        private readonly HttpRestClient restClient;

        public ToDoService(HttpRestClient restClient) : base(restClient, "ToDo")
        {
            this.restClient = restClient;
        }


       public async Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter parameter)
        {
            var requset = new BaseRequest
            {
                Metod = Method.Get,
                Route = $"api/ToDo/GetAll?PageIndex={parameter.PageIndex}" +
                $"&PageSize={parameter.PageSize}" +
                $"&Search={parameter.Search}" +
                $"&Status={parameter.Status}",
                ContentType = "application/json",
            };
            var res = await restClient.ExcuteAsync<PagedList<ToDoDto>>(requset);
            return res;
        }
    }

}
