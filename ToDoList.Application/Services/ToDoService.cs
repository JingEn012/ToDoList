using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly IToDoRepository _toDoRepository;

        public TodoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _toDoRepository.GetAllItemsAsync();
        }

        public async Task<Item> AddItemAsync(Item entity)
        {
            await _toDoRepository.AddItemAsync(entity);
            return entity;
        }

        public async Task<bool> UpdateItemAsync(int itemId, Item entity)
        {
            Item itemFound = await _toDoRepository.GetItemByIdAsync(itemId);

            if (itemFound != null)
            {
                itemFound.Title = entity.Title;
                itemFound.DueDate = entity.DueDate;

                await _toDoRepository.UpdateItemAsync(itemFound);
                return true;
            }
            return false;
        }

        public async Task<bool> ToggleCompleteAsync(int itemId)
        {
            Item itemFound = await _toDoRepository.GetItemByIdAsync(itemId);

            if (itemFound != null)
            {
                itemFound.IsCompleted = !itemFound.IsCompleted;

                await _toDoRepository.UpdateItemAsync(itemFound);
                return true;
            }
            return false;
        }


        public async Task<bool> DeleteItemAsync(int itemId)
        {
            Item itemFound = await _toDoRepository.GetItemByIdAsync(itemId);

            if (itemFound != null)
            {
                await _toDoRepository.DeleteItemAsync(itemId);
                return true;
            }

            return false;
        }
    }
}
