using System.ComponentModel;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using SampleAspNetCoreMcp.ApiService.Data;
using SampleAspNetCoreMcp.ApiService.Models;

namespace SampleAspNetCoreMcp.ApiService.Tools;

[McpServerToolType]
public sealed class DeleteTodoTool
{
    private readonly ClaimsPrincipal _principal;
    private readonly ToDoDbContext _dbContext;

    public DeleteTodoTool(ClaimsPrincipal principal, ToDoDbContext dbContext)
    {
        _principal = principal;
        _dbContext = dbContext;
    }

    [McpServerTool, Description("Deletes a todo item for the authenticated user.")]
    public async Task<ToDoItem?> DeleteTodo(
        [Description("ID of the todo item to delete")] Guid id)
    {
        var userEmail = _principal.FindFirstValue(ClaimTypes.Email) ?? "unknown";

        var todoItem = await _dbContext.ToDoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserEmail == userEmail);

        if (todoItem is null)
        {
            return null;
        }

        var deletedItem = todoItem;
        _dbContext.ToDoItems.Remove(todoItem);
        await _dbContext.SaveChangesAsync();

        return deletedItem;
    }
}
