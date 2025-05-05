using ToDoList.Domain.Entities;
using ToDoList.Application.Interfaces;
using ToDoList.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure.Repositories
{
    public class ToDoRepository(ToDoDbContext dbContext) : IToDoRepository
    {
        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await dbContext.Items.ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await dbContext.Items.FindAsync(id);
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            dbContext.Items.Add(item);
            await dbContext.SaveChangesAsync();
            return item;
        }

        public async Task UpdateItemAsync(Item entity)
        {
            dbContext.Items.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int itemId)
        {
            var item = dbContext.Items.Find(itemId);

            if (item != null)
            {
                dbContext.Items.Remove(item);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
