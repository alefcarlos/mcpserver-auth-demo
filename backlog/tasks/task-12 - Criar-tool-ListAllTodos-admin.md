---
id: TASK-12
title: Criar tool ListAllTodos (admin)
status: Done
assignee: []
created_date: '2026-02-22 08:00'
updated_date: '2026-02-22 08:07'
labels: []
dependencies: []
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
## Descrição

Criar tool ListAllTodos que lista todas as tarefas de todos os usuários. Apenas usuários com claim `role` contendo "admin" podem executar.

### Passos para executar:
1. Criar `Tools/ListAllTodosTool.cs` seguindo o padrão das outras tools
2. Adicionar verificação de role admin usando ClaimsPrincipal
3. Se não for admin, retornar lista vazia ou throw exception
4. Adicionar parâmetro opcional `isCompleted` para filtrar
5. Retornar todos os ToDoItems sem filtro de usuário (ou filtrados)
6. Verificar build com `dotnet build`
7. Testar com `aspire run`

### Implementação
```csharp
[McpServerTool, Description("Lists all todo items from all users (admin only).")]
public async Task<List<ToDoItem>> ListAllTodos(
    [Description("Filter by completed status (optional)")] bool? isCompleted = null)
{
    // 1. Get roles from _principal
    // 2. Check if user has admin role
    // 3. If not admin, return empty list or throw
    // 4. Query all ToDoItems
    // 5. Apply isCompleted filter if provided
    // 6. Return list
}
```
<!-- SECTION:DESCRIPTION:END -->
