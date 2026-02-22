---
id: TASK-4
title: Criar tool UpdateTodo
status: Done
assignee: []
created_date: '2026-02-22 05:56'
updated_date: '2026-02-22 07:39'
labels: []
dependencies: []
priority: high
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
## Descrição

Criar tool UpdateTodo que permite ao usuário autenticado atualizar o título e/ou descrição de uma tarefa existente. A tool deve verificar se a tarefa pertence ao usuário autenticado antes de fazer a atualização.

### Passos para executar:
1. Criar `Tools/UpdateTodoTool.cs` seguindo o padrão de `CreateTodoTool`
2. Adicionar ao `Program.cs`: `.WithTools<UpdateTodoTool>()` (já usa assembly, não precisa)
3. Verificar build com `dotnet build`
4. Testar com `aspire run` e verificar recursos

### Implementação
```csharp
[McpServerTool, Description("Updates an existing todo item for the authenticated user.")]
public async Task<ToDoItem?> UpdateTodo(
    [Description("ID of the todo item to update")] Guid id,
    [Description("New title (optional)")] string? title = null,
    [Description("New description (optional)")] string? description = null)
{
    // 1. Get user email from _principal
    // 2. Find ToDoItem by Id
    // 3. Check if item.UserEmail matches current user (segurança!)
    // 4. If not found or not owner, return null
    // 5. Update fields (title, description) only if provided
    // 6. Save changes
    // 7. Return updated item
}
```
<!-- SECTION:DESCRIPTION:END -->
