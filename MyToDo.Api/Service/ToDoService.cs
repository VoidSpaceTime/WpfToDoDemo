using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;

namespace MyToDo.Api.Service
{
    public class ToDoService : IToDoService
    {
        private readonly MyToDoContext _dbContext;

        public ToDoService(MyToDoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> AddAsync(ToDo model)
        {
            _dbContext.ToDo.Add(model);
            return await _dbContext.SaveChangesAsync().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return new ApiResponse(true, model);
                }
                else
                {
                    return new ApiResponse("Failed to add ToDo item.");
                }
            });
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var ob = await _dbContext.ToDo.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.ToDo.Remove(ob);
            return await _dbContext.SaveChangesAsync().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return new ApiResponse(true, "Delet completed");
                }
                else
                {
                    return new ApiResponse("Failed to delete ToDo item.");
                }
            });
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var todos = _dbContext.ToDo.ToListAsync();
            return await todos.ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return new ApiResponse(true, task.Result);
                }
                else
                {
                    return new ApiResponse("Failed to retrieve ToDo items.");
                }
            });
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            return await _dbContext.ToDo.FirstOrDefaultAsync(x => x.Id == id).ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully && task.Result != null)
                {
                    return new ApiResponse(true, task.Result);
                }
                else
                {
                    return new ApiResponse("ToDo item not found.");
                }
            });
        }

        public async Task<ApiResponse> UpdateAsync(ToDo model)
        {
            return await _dbContext.ToDo
                 .Where(x => x.Id == model.Id)
                 .ExecuteUpdateAsync(setters => setters
                     .SetProperty(x => x.Title, model.Title)
                     .SetProperty(x => x.Content, model.Content)
                     .SetProperty(x => x.Status, model.Status)
                     .SetProperty(x => x.UpdateDate, DateTime.Now))
                 .ContinueWith(task =>
                 {
                     if (task.IsCompletedSuccessfully)
                     {
                         return new ApiResponse(true, "Update completed");
                     }
                     else
                     {
                         return new ApiResponse("Failed to update ToDo item.");
                     }
                 });
        }
    }
}
