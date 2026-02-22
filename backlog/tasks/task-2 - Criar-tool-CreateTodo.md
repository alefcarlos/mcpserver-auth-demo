---
id: TASK-2
title: Criar tool CreateTodo
status: Done
assignee: []
created_date: '2026-02-22 05:56'
updated_date: '2026-02-22 07:23'
labels: []
dependencies: []
priority: high
ordinal: 1000
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
## Descrição

Criar tool CreateTodo que permite ao usuário autenticado criar uma nova tarefa.

### Passos para executar:
1. Criar `Tools/CreateTodoTool.cs` seguindo o padrão de `MathTool`
2. Adicionar ao `Program.cs`: `.WithTools<CreateTodoTool>()`
3. Verificar build com `dotnet build`
4. Testar com `aspire run` e verificar recursos

### Implementação
```csharp
[McpServerTool, Description("Creates a new todo item for the authenticated user.")]
public Task<ToDoItem> CreateTodo(
    [Description("Title of the todo item")] string title,
    [Description("Description of the todo item")] string description)
{
    // 1. Get user email from _principal
    // 2. Create ToDoItem:
    //    - Title = title
    //    - Description = description  
    //    - IsCompleted = false
    //    - CreatedAt = DateTime.UtcNow
    //    - UserEmail = _principal.FindFirstValue(ClaimTypes.Email)
    //    - Id gerenciado pelo EF Core
    // 3. Save to ToDoDbContext
    // 4. Return created item
}
```
<!-- SECTION:DESCRIPTION:END -->
