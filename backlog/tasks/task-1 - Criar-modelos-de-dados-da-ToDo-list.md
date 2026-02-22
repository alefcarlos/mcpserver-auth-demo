---
id: TASK-1
title: Criar modelos de dados da ToDo list
status: Done
assignee: []
created_date: '2026-02-22 05:56'
updated_date: '2026-02-22 07:23'
labels: []
dependencies: []
priority: high
ordinal: 4000
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
## Implementação

### 1. Adicionar pacote NuGet
Adicionar ao `SampleAspNetCoreMcp.ApiService.csproj`:
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="10.0.0" />
```

### 2. Criar `Models/ToDoItem.cs`
```csharp
public class ToDoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserEmail { get; set; } = string.Empty;
}
```

### 3. Criar `Data/ToDoDbContext.cs`
```csharp
public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }
    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
}
```

### 4. Configurar no `Program.cs` do ApiService
```csharp
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseInMemoryDatabase("ToDoDb"));
```

### 5. Claim do JWT para UserEmail
As tools usarão a claim `email` do `ClaimsPrincipal` para filtrar tarefas por usuário.

---

**Nota:** O banco InMemory será criado automaticamente. Não requer migrations.
<!-- SECTION:DESCRIPTION:END -->
