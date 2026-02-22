using Microsoft.EntityFrameworkCore;
using SampleAspNetCoreMcp.ApiService.Models;

namespace SampleAspNetCoreMcp.ApiService.Data;

public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
}
