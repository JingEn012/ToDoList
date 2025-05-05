using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Persistence
{
    public class ToDoDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Item> Items { get; set; }

    }
}
