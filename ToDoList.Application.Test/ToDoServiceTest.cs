using Moq;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Services;
using ToDoList.Domain.Entities;
using Xunit;

namespace ToDoList.Application.Test
{
    public class ToDoServiceTest
    {
        private readonly Mock<IToDoRepository> _mockRepository;
        private readonly TodoService _todoService;
        public ToDoServiceTest()
        {
            _mockRepository = new Mock<IToDoRepository>();
            _todoService = new TodoService(_mockRepository.Object);
        }

        /* GET ALL ITEMS TEST */
        [Fact]
        public async Task GetAllItems_ReturnItems()
        {
            //Arrange
            var expectedItems = new List<Item>
            {
            new() { Id = 1, Title = "Task 1", DueDate = new DateOnly(), IsCompleted = false },
            new() { Id = 2, Title = "Task 2", DueDate = new DateOnly(), IsCompleted = true }
            };

            _mockRepository.Setup(repo => repo.GetAllItemsAsync()).ReturnsAsync(expectedItems);

            //Act
            var result = await _todoService.GetAllItemsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(expectedItems, result);
        }

        /* ADD ITEM TEST */
        [Fact]
        public async Task AddItem_CallsRepositoryMethod()
        {
            //Arrange
            var newFakeItem = new Item
            {
                Title = "Task 5",
                DueDate = new DateOnly(),
                IsCompleted = true
            };

            //Act
            await _todoService.AddItemAsync(newFakeItem);

            //Assert
            _mockRepository.Verify(repo => repo.AddItemAsync(newFakeItem), Times.Once());
        }

        /* UPDATE ITEM TEST */
        [Fact]
        public async Task UpdateItem_CallsRepositoryMethodSuccess()
        {
            //Arrange
            var updatedFakeItem = new Item
            {
                Title = "Updated Task",
                DueDate = new DateOnly(2025, 8, 30),
                IsCompleted = false
            };

            var itemGetById = new Item
            {
                Id = 3,
                Title = "Task 3",
                IsCompleted = false
            };
            _mockRepository.Setup(repo => repo.GetItemByIdAsync(3)).ReturnsAsync(itemGetById);


            //Act
            var result = await _todoService.UpdateItemAsync(3, updatedFakeItem);

            //Assert
            Assert.True(result);
            _mockRepository.Verify(repo => repo.UpdateItemAsync(It.IsAny<Item>()), Times.Once());
        }

        [Fact]
        public async Task UpdateItem_CallsRepositoryMethodUnsuccess()
        {
            //Arrange
            var updatedFakeItem = new Item
            {
                Title = "Updated Task",
                DueDate = new DateOnly(2025, 8, 30),
                IsCompleted = false
            };
            _mockRepository.Setup(repo => repo.GetItemByIdAsync(3)).ReturnsAsync((Item)null);


            //Act
            var result = await _todoService.UpdateItemAsync(3, updatedFakeItem);

            //Assert
            Assert.False(result);
            _mockRepository.Verify(repo => repo.UpdateItemAsync(It.IsAny<Item>()), Times.Never());
        }

        [Fact]
        public async Task ToggleComplete_CallsRepositoryMethodSuccess()
        {
            //Arrange
            var itemGetById = new Item
            {
                Id = 3,
                Title = "Task 3",
                IsCompleted = false
            };
            _mockRepository.Setup(repo => repo.GetItemByIdAsync(3)).ReturnsAsync((itemGetById));


            //Act
            var result = await _todoService.ToggleCompleteAsync(3);

            //Assert
            Assert.True(result);
            _mockRepository.Verify(repo => repo.UpdateItemAsync(It.IsAny<Item>()), Times.Once());
        }

        [Fact]
        public async Task ToggleComplete_CallsRepositoryMethodUnsuccess()
        {
            //Arrange
            _mockRepository.Setup(repo => repo.GetItemByIdAsync(2)).ReturnsAsync((Item)null);


            //Act
            var result = await _todoService.ToggleCompleteAsync(2);

            //Assert
            Assert.False(result);
            _mockRepository.Verify(repo => repo.UpdateItemAsync(It.IsAny<Item>()), Times.Never());
        }



        /* DELETE ITEM TEST */
        [Fact]
        public async Task DeleteItem_CallsRepositoryMethodSuccess()
        {

            //Arrange
            var itemGetById = new Item
            {
                Id = 3,
                Title = "Task 3",
                IsCompleted = false
            };

            int fakeItemId = 3;
            _mockRepository.Setup(repo => repo.GetItemByIdAsync(fakeItemId)).ReturnsAsync(itemGetById);


            //Act
            var result = await _todoService.DeleteItemAsync(fakeItemId);

            //Assert
            Assert.True(result);
            _mockRepository.Verify(repo => repo.DeleteItemAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task DeleteItem_CallsRepositoryMethodUnsuccess()
        {
            //Arrange
            _mockRepository.Setup(repo => repo.GetItemByIdAsync(3)).ReturnsAsync((Item)null);


            //Act
            var result = await _todoService.DeleteItemAsync(3);

            //Assert
            Assert.False(result);
            _mockRepository.Verify(repo => repo.DeleteItemAsync(It.IsAny<int>()), Times.Never());
        }
    }
}
