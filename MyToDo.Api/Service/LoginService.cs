using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using System.Security.Principal;

namespace MyToDo.Api.Service
{
    public class LoginService : ILoginService
    {
        private readonly MyToDoContext dbContext;
        private readonly IMapper mapper;

        public LoginService(MyToDoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> LoginAsync(string account, string password)
        {
            var result = await dbContext.User.FirstOrDefaultAsync(e => e.UserName.Equals(account) && e.Password.Equals(password));
            if (result == null)
            {
                return new ApiResponse(false, "Invalid account or password.");
            }
            else
            {
                return new ApiResponse(true, mapper.Map<UserDto>(result));
            }
        }

        public async Task<ApiResponse> RegisterAsync(UserDto userDto)
        {
            var result = await dbContext.User.FirstOrDefaultAsync(e => e.UserName.Equals(userDto.UserName) && e.Account.Equals(userDto.Account));
            if (result == null)
            {
                var entity = mapper.Map<User>(userDto);
                entity.CreateTime = DateTime.Now;
                await dbContext.User.AddAsync(entity);
                try
                {
                    var saveResult = await dbContext.SaveChangesAsync();
                    if (saveResult > 0)
                    {
                        return new ApiResponse(true, userDto);
                    }
                    else
                    {
                        return new ApiResponse("Failed to add User item.");
                    }
                }
                catch (Exception ex)
                {
                    return new ApiResponse($"Exception: {ex.Message}");
                }
            }
            else
            {
                return new ApiResponse(false, "User already exists with the same account or username.");
            }
        }
    }
}
