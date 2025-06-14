using DryIoc;
using MaterialDesignColors;
using MyToDo.Shared;
using MyToDo.Shared.Parameters;
using RestSharp;

namespace WpfDemo.Sercive
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly HttpRestClient restClient;
        private readonly string serviceNmae;
        public BaseService(HttpRestClient restClient, string serviceNmae)
        {
            this.restClient = restClient;
            this.serviceNmae = serviceNmae;
        }
        public async Task<ApiResponse<T>> AddAsync(T model)
        {
            var requset = new BaseRequest
            {
                Route = $"api/{serviceNmae}/Add",
                Metod = Method.Post,
                ContentType = "application/json",
                Parameters = model
            };
            return await restClient.ExcuteAsync<T>(requset);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var requset = new BaseRequest
            {
                Route = $"api/{serviceNmae}/Delete?id={id}",
                Metod = Method.Post,
                ContentType = "application/json",
            };
            return await restClient.ExcuteAsync(requset);
        }

        public async Task<ApiResponse<PagedList<T>>> GetAllAsync(MyToDo.Shared.Parameters.QueryParameter queryParameter)
        {
            var requset = new BaseRequest
            {
                Metod = Method.Get,
                Route = $"api/{serviceNmae}/GetAll?PageIndex={queryParameter.PageIndex}&PageSize={queryParameter.PageSize}&Search={queryParameter.Search}",
                ContentType = "application/json",
            };
            var res = await restClient.ExcuteAsync<PagedList<T>>(requset);
            return res;
        }

        public async Task<ApiResponse<T>> GetFirstOrDefaultAsync(int id)
        {
            var requset = new BaseRequest
            {
                Route = $"api/{serviceNmae}/Get?id={id}",
                Metod = Method.Get,
                ContentType = "application/json",
            };
            return await restClient.ExcuteAsync<T>(requset);
        }

        public async Task<ApiResponse<T>> UpdateAsync(T model)
        {
            var requset = new BaseRequest
            {
                Route = $"api/{serviceNmae}/Update",
                Metod = Method.Post,
                ContentType = "application/json",
                Parameters = model
            };
            return await restClient.ExcuteAsync<T>(requset);
        }
    }
}
