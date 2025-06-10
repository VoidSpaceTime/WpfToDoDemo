using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<ApiResponse<T>> AddAshync(T model)
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

        public Task<ApiResponse> DeleAsync(int id)
        {
            var requset = new BaseRequest
            {
                Route = $"api/{serviceNmae}/Delete?id={id}",
                Metod = Method.Post,
                ContentType = "application/json",
            };
            return await restClient.ExcuteAsync(requset);
        }

        public Task<ApiResponse<PagedList<T>>> GetAllAsync(QueryParameter queryParameter)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<T>> GetFirstOrDefaultAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<T>> UpdateAsync(T model)
        {
            throw new NotImplementedException();
        }
    }
}
