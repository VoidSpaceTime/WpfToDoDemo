using MyToDo.Api.Service;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfDemo.Sercive
{
    public class HttpRestClient
    {
        private readonly string baseUrl;
        protected readonly RestClient restClient;

        public HttpRestClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
            this.restClient = new RestClient();
        }
        public async Task<ApiResponse> ExcuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseUrl + baseRequest.Route, baseRequest.Metod);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameters != null)
            {
                //if (baseRequest.Metod == Method.Get)
                //{
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameters));
                //}
                //else
                //{
                //    request.AddJsonBody(baseRequest.Parameters);
                //}
            }
            //request.Resource = (baseUrl + baseRequest.Route);
            //var response = await restClient.ExecuteAsync<ApiResponse>(request);
            var response = await restClient.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiResponse>(response.Content) ?? new ApiResponse("Error in response");
        }
        public async Task<ApiResponse<T>> ExcuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseUrl + baseRequest.Route, baseRequest.Metod);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameters != null)
            {
                //if (baseRequest.Metod == Method.Get)
                //{
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameters));
                //}
                //else
                //{
                //    request.AddJsonBody(baseRequest.Parameters);
                //}
            }
            //request.Resource = (baseUrl + baseRequest.Route);
            //var response = await restClient.ExecuteAsync<ApiResponse>(request);
            var response = await restClient.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
        }
    }
}
