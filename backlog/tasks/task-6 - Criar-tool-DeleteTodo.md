---
id: TASK-6
title: Criar tool DeleteTodo
status: Done
assignee: []
created_date: '2026-02-22 05:56'
updated_date: '2026-02-22 07:57'
labels: []
dependencies: []
priority: high
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
## Descrição

Criar tool DeleteTodo que remove uma tarefa. Verifica se a tarefa pertence ao usuário autenticado antes de deletar.

### Passos para executar:
1. Criar `Tools/DeleteTodoTool.cs` seguindo o padrão de `UpdateTodoTool`
2. Usar query composta (id + email) conforme TASK-11
3. Usar `Remove()` do EF Core para deletar
4. Verificar build com `dotnet build`
5. Testar com `aspire run`

### Implementação
```csharp
[McpServerTool, Description("Deletes a todo item.")]
public async Task<bool> DeleteTodo(
    [Description("ID of the todo item to delete")] Guid id)
{
    // 1. Get user email from _principal
    // 2. Query ToDoItem by Id + UserEmail (composto)
    // 3. If not found, return false
    // 4. Remove item from DbSet
    // 5. Save changes
    // 6. Return true
}
```
<!-- SECTION:DESCRIPTION:END -->
