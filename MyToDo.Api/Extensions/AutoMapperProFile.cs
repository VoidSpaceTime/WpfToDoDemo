using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Extensions
{
    public class AutoMapperProFile : Profile // Fixed the base class to "Profile"
    {
        public AutoMapperProFile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap(); // "CreateMap" is a method of the "Profile" class
            CreateMap<Memo, MemoDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
