using MyToDo.Api.Service;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Sercive
{
    public interface IBaseService<T> where T : class
    {
        Task<ApiResponse<T>> AddAshync(T model);
        Task<ApiResponse<T>> UpdateAsync(T model);
        Task<ApiResponse<T>> DeleAsync(int id);
        Task<ApiResponse<T>> GetFirstOrDefaultAsync(int id);
        Task<ApiResponse<PagedList<T>>> GetAllAsync(QueryParameter queryParameter);

    }
}
