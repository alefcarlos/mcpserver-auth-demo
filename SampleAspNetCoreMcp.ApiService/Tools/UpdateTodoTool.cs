using System.ComponentModel;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using SampleAspNetCoreMcp.ApiService.Data;
using SampleAspNetCoreMcp.ApiService.Models;

namespace SampleAspNetCoreMcp.ApiService.Tools;

[McpServerToolType]
public sealed class UpdateTodoTool
{
    private readonly ClaimsPrincipal _principal;
    private readonly ToDoDbContext _dbContext;

    public UpdateTodoTool(ClaimsPrincipal principal, ToDoDbContext dbContext)
    {
        _principal = principal;
        _dbContext = dbContext;
    }

    [McpServerTool, Description("Updates an existing todo item for the authenticated user.")]
    public async Task<ToDoItem?> UpdateTodo(
        [Description("ID of the todo item to update")] Guid id,
        [Description("New title (optional)")] string? title = null,
        [Description("New description (optional)")] string? description = null)
    {
        var userEmail = _principal.FindFirstValue(ClaimTypes.Email) ?? "unknown";

        var todoItem = await _dbContext.ToDoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserEmail == userEmail);

        if (todoItem is null)
        {
            return null;
        }

        if (title is not null)
        {
            todoItem.Title = title;
        }

        if (description is not null)
        {
            todoItem.Description = description;
        }

        await _dbContext.SaveChangesAsync();

        return todoItem;
    }
}
