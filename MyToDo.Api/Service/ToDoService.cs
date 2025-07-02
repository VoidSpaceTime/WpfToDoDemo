using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System.Collections.ObjectModel;

namespace MyToDo.Api.Service
{
    public class ToDoService : IToDoService
    {
        private readonly MyToDoContext dbContext;
        private readonly IMapper mapper;
        public ToDoService(MyToDoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(ToDo model)
        {
            await dbContext.ToDo.AddAsync(model);
            return await dbContext.SaveChangesAsync().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return new ApiResponse(true, mapper.Map<ToDoDto>(model));
                }
                else
                {
                    return new ApiResponse("Failed to add ToDo item.");
                }
            });
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var ob = await dbContext.ToDo.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.ToDo.Remove(ob);
            return await dbContext.SaveChangesAsync().ContinueWith(task =>
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

        public async Task<ApiResponse> GetAllAsync(QueryParameter queryParameter)
        {
            var query = dbContext.ToDo.AsQueryable();

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
            var dtos = items.Select(e => mapper.Map<ToDoDto>(e)).ToList();
            var result = new
            {
                TotalCount = totalCount,
                Items = dtos
            };


            return new ApiResponse(true, result);

        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            return await dbContext.ToDo.FirstOrDefaultAsync(x => x.Id == id).ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully && task.Result != null)
                {
                    return new ApiResponse(true, mapper.Map<ToDoDto>(task.Result));
                }
                else
                {
                    return new ApiResponse("ToDo item not found.");
                }
            });
        }


        public async Task<ApiResponse> UpdateAsync(ToDo model)
        {
            dbContext.ToDo.Update(model);
            return await dbContext.SaveChangesAsync().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return new ApiResponse(true, mapper.Map<ToDoDto>(model));
                }
                else
                {
                    return new ApiResponse("Failed to update ToDo item.");
                }
            });
        }

        public async Task<ApiResponse> GetFliterAll(ToDoParameter toDoParameter)
        {
            var query = dbContext.ToDo.AsQueryable();

            // 搜索条件
            if (!string.IsNullOrWhiteSpace(toDoParameter.Search))
            {
                query = query.Where(t => t.Title.Contains(toDoParameter.Search) || t.Content.Contains(toDoParameter.Search));
            }
            if (toDoParameter.Status != -1)
            {
                query = query.Where(t => t.Status.Equals(toDoParameter.Status));
            }

            // 总数
            var totalCount = await query.CountAsync();

            // 分页
            var items = await query
                .OrderByDescending(t => t.CreateTime)
                .Skip((toDoParameter.PageIndex - 1) * toDoParameter.PageSize)
                .Take(toDoParameter.PageSize)
                .ToListAsync();
            var dtos = items.Select(e => mapper.Map<ToDoDto>(e)).ToList();
            var result = new
            {
                TotalCount = totalCount,
                Items = dtos
            };


            return new ApiResponse(true, result);
        }

        public async Task<ApiResponse> GetSummary()
        {
            // Fetch all ToDo items and Memo items from the database
            var todoList = await dbContext.ToDo.ToListAsync();
            var memoList = await dbContext.Memo.ToListAsync();
            todoList = todoList.OrderByDescending(t => t.CreateTime).ToList();
            memoList = memoList.OrderByDescending(t => t.CreateTime).ToList();
            // Create a summary object with the required calculations
            SummaryDto summary = new SummaryDto
            {
                Sum = todoList.Count,
                CompletedCount = todoList.Count(t => t.Status == 1),
                MemoCount = memoList.Count,
                ToDoCount = todoList.Count(t => t.Status == 0),
                CompletedRadio = (todoList.Count > 0 ? (todoList.Count(t => t.Status == 1) / (double)todoList.Count) * 100 : 0).ToString("0%"),
                ToDoList = mapper.Map<ObservableCollection<ToDoDto>>(todoList.Where(o => o.Status == 0)),
                MemoList = mapper.Map<ObservableCollection<MemoDto>>(memoList)
            };

            // Return the summary wrapped in an ApiResponse
            return new ApiResponse(true, summary);
        }
    }
}
