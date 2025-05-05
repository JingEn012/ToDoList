using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private readonly ITodoService _toDoService;

    public ToDoController(ITodoService toDoService)
    {
        _toDoService = toDoService;
    }

    [HttpGet(Name = "GetAllToDoItems")]
    public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
    {
        var items = await _toDoService.GetAllItemsAsync();
        return Ok(items);
    }

    [HttpPost("add", Name = "AddToDoItem")]
    public async Task<ActionResult> AddToDoItem([FromBody] Item entity)
    {
        var item = await _toDoService.AddItemAsync(entity);
        return Created("Created successfully", item);
    }

    [HttpPut("update/{itemId}", Name = "UpdateToDoItem")]
    public async Task<ActionResult> UpdateToDoItem([FromRoute] int itemId, [FromBody] Item entity)
    {
        var success = await _toDoService.UpdateItemAsync(itemId, entity);

        if (!success)
        {
            return NotFound("Item not found");
        }

        return Ok("Item " + itemId + " is being updated successfully");
    }

    [HttpPut("toggleComplete/{itemId}", Name = "ToggleComplete")]
    public async Task<ActionResult> ToogleComplete([FromRoute] int itemId)
    {
        var success = await _toDoService.ToggleCompleteAsync(itemId);

        if (!success)
        {
            return NotFound("Item not found");
        }

        return Ok();
    }

    [HttpDelete("delete/{itemId}", Name = "DeleteToDoItem")]
    public async Task<ActionResult> DeleteToDoItem([FromRoute] int itemId)
    {
        var success = await _toDoService.DeleteItemAsync(itemId);
        if (!success)
        {
            return NotFound("Item not found");
        }

        return Ok("Item " + itemId + " is being deleted successfully");

    }
}

