---
id: TASK-13
title: Agrupar Todo Tools em TodoList
status: Done
assignee: []
created_date: '2026-02-22 22:27'
updated_date: '2026-02-22 22:34'
labels: []
dependencies: []
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
Agrupar todas as 6 tools de ToDo em um único arquivo TodoList.cs com um único [McpServerToolType] chamado TodoList. Adicionar [Authorize] a nível de classe para autenticação e [Authorize(Roles = "Admin")] apenas no método ListAllTodos.

Passos:
1. Criar novo arquivo SampleAspNetCoreMcp.ApiService/Tools/TodoList.cs
2. Criar classe public sealed class TodoList com [McpServerToolType] e [Authorize]
3. Adicionar campos privados _principal (ClaimsPrincipal) e _dbContext (ToDoDbContext)
4. Criar construtor com injeção de dependência para ClaimsPrincipal e ToDoDbContext
5. Criar método CreateTodo(title, description) - move lógica de CreateTodoTool
6. Criar método ListTodos() - move lógica de ListTodoTool
7. Criar método UpdateTodo(id, title, description) - move lógica de UpdateTodoTool
8. Criar método DeleteTodo(id) - move lógica de DeleteTodoTool
9. Criar método CompleteTodo(id) - move lógica de CompleteTodoTool
10. Criar método ListAllTodos(isCompleted?) com [Authorize(Roles = "Admin")] - move lógica de ListAllTodosTool
11. Deletar os 6 arquivos antigos: CreateTodoTool.cs, ListTodoTool.cs, UpdateTodoTool.cs, DeleteTodoTool.cs, CompleteTodoTool.cs, ListAllTodosTool.cs

Pseudocódigo:
```csharp
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
    public async Task<ToDoItem> CreateTodo(...) { ... }

    [McpServerTool, Description("Lists all todo items for the authenticated user.")]
    public async Task<List<ToDoItem>> ListTodos() { ... }

    [McpServerTool, Description("Updates an existing todo item for the authenticated user.")]
    public async Task<ToDoItem?> UpdateTodo(...) { ... }

    [McpServerTool, Description("Deletes a todo item for the authenticated user.")]
    public async Task<ToDoItem?> DeleteTodo(...) { ... }

    [McpServerTool, Description("Marks a todo item as completed.")]
    public async Task<ToDoItem?> CompleteTodo(...) { ... }

    [McpServerTool, Description("Lists all todo items from all users (admin only).")]
    [Authorize(Roles = "Admin")]
    public async Task<List<ToDoItem>> ListAllTodos(...) { ... }
}
```
<!-- SECTION:DESCRIPTION:END -->
