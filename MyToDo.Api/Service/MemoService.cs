using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Service
{
    public class MemoService : IMemoService
    {
        private readonly MyToDoContext _dbContext;
        private readonly IMapper mapper;
        public MemoService(MyToDoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(Memo model)
        {
            _dbContext.Memo.Add(model);
            return await _dbContext.SaveChangesAsync().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return new ApiResponse(true, model);
                }
                else
                {
                    return new ApiResponse("Failed to add Memo item.");
                }
            });
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var ob = await _dbContext.Memo.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Memo.Remove(ob);
            return await _dbContext.SaveChangesAsync().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return new ApiResponse(true, "Delet completed");
                }
                else
                {
                    return new ApiResponse("Failed to delete Memo item.");
                }
            });
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter queryParameter)
        {
            var query = _dbContext.Memo.AsQueryable();
            //var memos = _dbContext.Memo.ToListAsync();

            // 搜索条件
            if (!string.IsNullOrWhiteSpace(queryParameter.Search))
            {
                query = query.Where(t => t.Title.Contains(queryParameter.Search) || t.Content.Contains(queryParameter.Search));
            }

            // 总数
            var totalCount = await query.CountAsync();

            // 分页
            var items = await query
                .OrderByDescending(t => t.CreateTime)
                .Skip((queryParameter.PageIndex - 1) * queryParameter.PageSize)
                .Take(queryParameter.PageSize)
                .ToListAsync();

            var result = new
            {
                TotalCount = totalCount,
                Items = items
            };


            return new ApiResponse(true, result);

        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            return await _dbContext.Memo.FirstOrDefaultAsync(x => x.Id == id).ContinueWith(task =>
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

        public async Task<ApiResponse> UpdateAsync(Memo model)
        {
            return await _dbContext.Memo
                 .Where(x => x.Id == model.Id)
                 .ExecuteUpdateAsync(setters => setters
                     .SetProperty(x => x.Title, model.Title)
                     .SetProperty(x => x.Content, model.Content)
                     .SetProperty(x => x.UpdateDate, DateTime.Now))
                 .ContinueWith(task =>
                 {
                     if (task.IsCompletedSuccessfully)
                     {
                         return new ApiResponse(true, "Update completed");
                     }
                     else
                     {
                         return new ApiResponse("Failed to update Memo item.");
                     }
                 });
        }
    }
}
