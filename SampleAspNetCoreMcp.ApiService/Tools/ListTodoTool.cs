using System.ComponentModel;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using SampleAspNetCoreMcp.ApiService.Data;
using SampleAspNetCoreMcp.ApiService.Models;

namespace SampleAspNetCoreMcp.ApiService.Tools;

[McpServerToolType]
public sealed class ListTodoTool
{
    private readonly ClaimsPrincipal _principal;
    private readonly ToDoDbContext _dbContext;

    public ListTodoTool(ClaimsPrincipal principal, ToDoDbContext dbContext)
    {
        _principal = principal;
        _dbContext = dbContext;
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
}
