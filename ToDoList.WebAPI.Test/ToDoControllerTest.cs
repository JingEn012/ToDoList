using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.WebAPI.Controllers;
using Xunit;

namespace ToDoList.WebAPI.Test
{
    public class ToDoControllerTest
    {
        private readonly Mock<ITodoService> _mockService;
        private readonly ToDoController _controller;

        public ToDoControllerTest()
        {
            _mockService = new Mock<ITodoService>();
            _controller = new ToDoController(_mockService.Object);
        }

        /* GET ALL ITEMS TEST */
        [Fact]
        public async Task GetAll_ReturnOkResult_WithItems()
        {
            // Arrange
            var expectedItems = new List<Item>
            {
            new Item { Id = 1, Title = "Task 1", DueDate = new DateOnly(), IsCompleted = false },
            new Item { Id = 2, Title = "Task 2", IsCompleted = true }
            };

            _mockService.Setup(service => service.GetAllItemsAsync()).ReturnsAsync(expectedItems);

            // Act
            var result = await _controller.GetAllItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var items = Assert.IsType<List<Item>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        /* ADD ITEM TEST */
        [Fact]
        public async Task AddItem_ReturnCreatedResult()
        {
            // Arrange
            var newFakeItem = new Item
            {
                Id = 5,
                Title = "Task 5",
                DueDate = new DateOnly(2025, 10, 10),
                IsCompleted = true
            };

            // Act
            var result = await _controller.AddToDoItem(newFakeItem);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        /* UPDATE ITEM TEST */
        [Fact]
        public async Task UpdateItem_ReturnOkResult()
        {
            //Arrange
            var updatedFakeItem = new Item
            {
                Title = "Updated Task",
                IsCompleted = false
            };

            _mockService.Setup(repo => repo.UpdateItemAsync(1, updatedFakeItem)).ReturnsAsync(true);

            //Act
            var result = await _controller.UpdateToDoItem(1, updatedFakeItem);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateItem_WhenIdDoNotFound()
        {
            //Arrange
            var updatedFakeItem = new Item
            {
                Title = "Updated Task",
                IsCompleted = false
            };
            _mockService.Setup(repo => repo.UpdateItemAsync(1, updatedFakeItem)).ReturnsAsync(false);


            //Act
            var result = await _controller.UpdateToDoItem(2, updatedFakeItem);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task ToogleComplete_ReturnOkResult()
        {
            //Arrange
            _mockService.Setup(repo => repo.ToggleCompleteAsync(3)).ReturnsAsync(true);


            //Act
            var result = await _controller.ToogleComplete(3);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task ToogleComplete_WhenIdNotFound()
        {
            //Arrange
            _mockService.Setup(repo => repo.ToggleCompleteAsync(3)).ReturnsAsync(false);


            //Act
            var result = await _controller.ToogleComplete(2);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        /* DELETE ITEM TEST */
        [Fact]
        public async Task DeleteItem_ReturnOkResult()
        {
            //Arrange
            _mockService.Setup(repo => repo.DeleteItemAsync(2)).ReturnsAsync(true);


            //Act
            var result = await _controller.DeleteToDoItem(2);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteItem_WhenIdDoNotFound()
        {
            //Arrange
            _mockService.Setup(repo => repo.DeleteItemAsync(3)).ReturnsAsync(false);


            //Act
            var result = await _controller.DeleteToDoItem(2);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
