using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MyToDo.Api.Context
{
    public class MyToDoContext : DbContext
    {

        DbSet<User> User { get; set; }
        DbSet<ToDo> ToDo { get; set; }
        DbSet<Memo> Memo { get; set; }

        public MyToDoContext(DbContextOptions<MyToDoContext> options) : base(options)
        {
        }

        public class MyToDoContextFactory : IDesignTimeDbContextFactory<MyToDoContext>
        {
            public MyToDoContext CreateDbContext(string[] args)
            {
                // 关键代码，必须加在这里！
                //Batteries.Init();

                var optionsBuilder = new DbContextOptionsBuilder<MyToDoContext>();
                optionsBuilder.UseSqlite("Data Source=app.db;");
                return new MyToDoContext(optionsBuilder.Options);
            }
        }


    }
}
