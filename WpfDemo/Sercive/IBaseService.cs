using MyToDo.Shared;

namespace WpfDemo.Sercive
{
    public interface IBaseService<T> where T : class
    {
        Task<ApiResponse<T>> AddAsync(T model);
        Task<ApiResponse<T>> UpdateAsync(T model);
        Task<ApiResponse> DeleteAsync(int id);
        Task<ApiResponse<T>> GetFirstOrDefaultAsync(int id);
        Task<ApiResponse<PagedList<T>>> GetAllAsync(MyToDo.Shared.Parameters.QueryParameter queryParameter);

    }
}
