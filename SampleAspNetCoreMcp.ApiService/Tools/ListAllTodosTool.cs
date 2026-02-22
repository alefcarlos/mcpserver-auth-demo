using System.ComponentModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using SampleAspNetCoreMcp.ApiService.Data;
using SampleAspNetCoreMcp.ApiService.Models;

namespace SampleAspNetCoreMcp.ApiService.Tools;

[McpServerToolType]
public sealed class ListAllTodosTool
{
    private readonly ClaimsPrincipal _principal;
    private readonly ToDoDbContext _dbContext;

    public ListAllTodosTool(ClaimsPrincipal principal, ToDoDbContext dbContext)
    {
        _principal = principal;
        _dbContext = dbContext;
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
