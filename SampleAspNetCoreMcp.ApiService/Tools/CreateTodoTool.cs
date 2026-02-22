using System.ComponentModel;
using System.Security.Claims;
using ModelContextProtocol.Server;
using SampleAspNetCoreMcp.ApiService.Data;
using SampleAspNetCoreMcp.ApiService.Models;

namespace SampleAspNetCoreMcp.ApiService.Tools;

[McpServerToolType]
public sealed class CreateTodoTool
{
    private readonly ClaimsPrincipal _principal;
    private readonly ToDoDbContext _dbContext;

    public CreateTodoTool(ClaimsPrincipal principal, ToDoDbContext dbContext)
    {
        _principal = principal;
        _dbContext = dbContext;
    }

    [McpServerTool, Description("Creates a new todo item for the authenticated user.")]
    public async Task<ToDoItem> CreateTodo(
        [Description("Title of the todo item")] string title,
        [Description("Description of the todo item")] string description)
    {
        var userEmail = _principal.FindFirstValue(ClaimTypes.Email) ?? "unknown";

        var todoItem = new ToDoItem
        {
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            UserEmail = userEmail
        };

        _dbContext.ToDoItems.Add(todoItem);
        await _dbContext.SaveChangesAsync();

        return todoItem;
    }
}
