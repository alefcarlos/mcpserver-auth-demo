using System.ComponentModel;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using SampleAspNetCoreMcp.ApiService.Data;
using SampleAspNetCoreMcp.ApiService.Models;

namespace SampleAspNetCoreMcp.ApiService.Tools;

[McpServerToolType]
public sealed class CompleteTodoTool
{
    private readonly ClaimsPrincipal _principal;
    private readonly ToDoDbContext _dbContext;

    public CompleteTodoTool(ClaimsPrincipal principal, ToDoDbContext dbContext)
    {
        _principal = principal;
        _dbContext = dbContext;
    }

    [McpServerTool, Description("Marks a todo item as completed.")]
    public async Task<ToDoItem?> CompleteTodo(
        [Description("ID of the todo item to complete")] Guid id)
    {
        var userEmail = _principal.FindFirstValue(ClaimTypes.Email) ?? "unknown";

        var todoItem = await _dbContext.ToDoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserEmail == userEmail);

        if (todoItem is null)
        {
            return null;
        }

        todoItem.IsCompleted = true;

        await _dbContext.SaveChangesAsync();

        return todoItem;
    }
}
