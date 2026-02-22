---
id: TASK-3
title: Criar tool ListTodos
status: Done
assignee: []
created_date: '2026-02-22 05:56'
updated_date: '2026-02-22 07:23'
labels: []
dependencies: []
priority: high
ordinal: 5000
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
## Descrição

Criar tool ListTodos que permite ao usuário autenticado listar todas as suas tarefas.

### Passos para executar:
1. Criar `Tools/ListTodoTool.cs` seguindo o padrão de `CreateTodoTool`
2. Adicionar ao `Program.cs`: `.WithTools<ListTodoTool>()`
3. Verificar build com `dotnet build`
4. Testar com `aspire run` e verificar recursos

### Implementação
```csharp
[McpServerTool, Description("Lists all todo items for the authenticated user.")]
public Task<List<ToDoItem>> ListTodos()
{
    // 1. Get user email from _principal
    // 2. Query ToDoItems filtering by UserEmail
    // 3. Order by CreatedAt descending
    // 4. Return list of items
}
```
<!-- SECTION:DESCRIPTION:END -->
