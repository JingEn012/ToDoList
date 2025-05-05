using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces
{
    public interface IToDoRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> AddItemAsync(Item item);
        Task UpdateItemAsync(Item entity);
        Task DeleteItemAsync(int itemId);
    }
}
