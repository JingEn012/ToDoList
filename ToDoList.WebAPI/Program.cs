using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Services;
using ToDoList.Infrastructure.Persistence;
using ToDoList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

builder.Services.AddDbContext<ToDoDbContext>(options =>
        options.UseSqlite("Data Source=../ToDoList.Infrastructure/Item.db"));

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
