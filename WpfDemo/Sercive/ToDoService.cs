using MyToDo.Shared.Dtos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Sercive
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        public ToDoService(HttpRestClient restClient) : base(restClient, "ToDo")
        {
        }
        // Implement any additional methods specific to ToDoService if needed
    }

}
