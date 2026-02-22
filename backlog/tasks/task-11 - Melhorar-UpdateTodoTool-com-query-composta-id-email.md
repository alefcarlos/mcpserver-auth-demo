---
id: TASK-11
title: Melhorar UpdateTodoTool com query composta id + email
status: Done
assignee: []
created_date: '2026-02-22 07:45'
updated_date: '2026-02-22 07:48'
labels: []
dependencies: []
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
## Descrição

Melhorar a UpdateTodoTool para fazer query com filtro composto (id + email) direto no banco, em vez de buscar por ID e depois verificar ownership em memória.

### Problema atual
```csharp
var todoItem = await _dbContext.ToDoItems.FindAsync(id);
if (todoItem is null || todoItem.UserEmail != userEmail)
```

### Melhoria proposta
```csharp
var todoItem = await _dbContext.ToDoItems
    .FirstOrDefaultAsync(t => t.Id == id && t.UserEmail == userEmail);
```

### Benefícios
- Query mais eficiente (filtro no banco)
- Menos transferência de dados
- atomicidade na verificação

### Passos para executar:
1. Modificar `Tools/UpdateTodoTool.cs`
2. Alterar a query para usar filtro composto
3. Verificar build com `dotnet build`
4. Testar
<!-- SECTION:DESCRIPTION:END -->
