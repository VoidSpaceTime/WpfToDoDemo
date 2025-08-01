using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;
using MyToDo.Api.Context.Repository;
using MyToDo.Api.Extensions;
using MyToDo.Api.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("ToDoConnection");
builder.Services.AddDbContext<MyToDoContext>(options =>
    options.UseSqlite(connectionString));
//builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
//builder.Services.AddScoped<IRepositoryBase<User>, UserRepository>();
//builder.Services.AddScoped<IRepositoryBase<ToDo>, ToDoRepository>();
//builder.Services.AddScoped<IRepositoryBase<Memo>, MemoRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ToDoRepository>();
builder.Services.AddScoped<MemoRepository>();
builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IMemoService, MemoService>();
builder.Services.AddScoped<ILoginService, LoginService>();

var automapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProFile());
});
builder.Services.AddSingleton(automapperConfig.CreateMapper());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
