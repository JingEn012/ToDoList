using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> AddItemAsync(Item item);
        Task<bool> UpdateItemAsync(int itemId, Item entity);
        Task<bool> ToggleCompleteAsync(int itemId);
        Task<bool> DeleteItemAsync(int itemId);
    }
}
