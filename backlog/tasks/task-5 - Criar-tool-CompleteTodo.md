---
id: TASK-5
title: Criar tool CompleteTodo
status: Done
assignee: []
created_date: '2026-02-22 05:56'
updated_date: '2026-02-22 07:54'
labels: []
dependencies: []
priority: high
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
## Descrição

Criar tool CompleteTodo que marca uma tarefa como completada. Verifica se a tarefa pertence ao usuário autenticado antes de modificar.

### Passos para executar:
1. Criar `Tools/CompleteTodoTool.cs` seguindo o padrão de `UpdateTodoTool`
2. Usar query composta (id + email) conforme TASK-11
3. Verificar build com `dotnet build`
4. Testar com `aspire run`

### Implementação
```csharp
[McpServerTool, Description("Marks a todo item as completed.")]
public async Task<ToDoItem?> CompleteTodo(
    [Description("ID of the todo item to complete")] Guid id)
{
    // 1. Get user email from _principal
    // 2. Query ToDoItem by Id + UserEmail (composto)
    // 3. If not found, return null
    // 4. Set IsCompleted = true
    // 5. Save changes
    // 6. Return updated item
}
```
<!-- SECTION:DESCRIPTION:END -->
