using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Service
{
    public class MemoService : IMemoService
    {
        private readonly MyToDoContext dbContext;
        private readonly IMapper mapper;
        public MemoService(MyToDoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(Memo model)
        {
            dbContext.Memo.Add(model);
            return await dbContext.SaveChangesAsync().ContinueWith(task =>
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
            var ob = await dbContext.Memo.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.Memo.Remove(ob);
            return await dbContext.SaveChangesAsync().ContinueWith(task =>
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
            var query = dbContext.Memo.AsQueryable();
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
            return await dbContext.Memo.FirstOrDefaultAsync(x => x.Id == id).ContinueWith(task =>
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
    
            dbContext.Memo.Update(model);
            return await dbContext.SaveChangesAsync().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return new ApiResponse(true, mapper.Map<MemoDto>(model));
                }
                else
                {
                    return new ApiResponse("Failed to update Memo item.");
                }
            });
        }
    }
}
