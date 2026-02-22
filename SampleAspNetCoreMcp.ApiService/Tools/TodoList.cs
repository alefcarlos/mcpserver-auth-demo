using System.ComponentModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using SampleAspNetCoreMcp.ApiService.Data;
using SampleAspNetCoreMcp.ApiService.Models;

namespace SampleAspNetCoreMcp.ApiService.Tools;

[McpServerToolType]
[Authorize]
public sealed class TodoList
{
    private readonly ClaimsPrincipal _principal;
    private readonly ToDoDbContext _dbContext;

    public TodoList(ClaimsPrincipal principal, ToDoDbContext dbContext)
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

    [McpServerTool, Description("Lists all todo items for the authenticated user.")]
    public async Task<List<ToDoItem>> ListTodos()
    {
        var userEmail = _principal.FindFirstValue(ClaimTypes.Email) ?? "unknown";

        return await _dbContext.ToDoItems
            .Where(t => t.UserEmail == userEmail)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
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

        if (title is not null || description is not null)
        {
            await _dbContext.SaveChangesAsync();
        }

        return todoItem;
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

    [McpServerTool, Description("Lists all todo items from all users (admin only).")]
    [Authorize(Roles = "Admin")]
    public async Task<List<ToDoItem>> ListAllTodos(
        [Description("Filter by completed status (optional)")] bool? isCompleted = null)
    {
        var query = _dbContext.ToDoItems.AsQueryable();

        if (isCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == isCompleted.Value);
        }

        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }
}
