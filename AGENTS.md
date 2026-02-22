# AGENTS.md

This is a .NET MCP server demo project with JWT authentication.

## Project Overview

- **Solution**: `SampleAspNetCoreMcp.slnx`
- **Framework**: .NET 10.0
- **Projects**:
  - `SampleAspNetCoreMcp.ApiService` - Main MCP server with JWT authentication
  - `SampleAspNetCoreMcp.ServiceDefaults` - Shared Aspire service defaults
- **Language**: C# 12+ with nullable reference types enabled

## Project Tools

- **Task Management**: Uses Backlog.md MCP for task management
- **Development Stack**: Uses Aspire for running the application

## Code Style Guidelines

### General Conventions

- **Implicit usings**: Enabled - do not add explicit `using` statements for common namespaces
- **Nullable reference types**: Enabled - always use proper nullable annotations
- **File-scoped namespaces**: Use `namespace X.Y;` instead of braces-wrapped namespaces
- **Target framework**: net10.0

### Naming Conventions

- **Classes**: PascalCase (`ToDoItem`, `MathTools`)
- **Methods**: PascalCase (`Add`, `Multiply`)
- **Properties**: PascalCase (`UserEmail`, `IsCompleted`)
- **Private fields**: `_camelCase` with underscore prefix (`_principal`)
- **Parameters**: camelCase (`firstOperand`, `secondOperand`)
- **Interfaces**: Prefix with `I` (`IHostApplicationBuilder`)

### Imports

- Group imports by category (System, Microsoft, third-party, project):
  ```csharp
  using System;
  using System.ComponentModel;
  using Microsoft.AspNetCore.Authentication.JwtBearer;
  using ModelContextProtocol.Server;
  using SampleAspNetCoreMcp.ApiService.Data;
  using SampleAspNetCoreMcp.ApiService.Tools;
  ```

### Types and Patterns

- **Records**: Use `record` for immutable DTOs
- **Primary constructors**: Use when dependency injection is needed:
  ```csharp
  public sealed class MathTools
  {
      private readonly ClaimsPrincipal _principal;
      public MathTools(ClaimsPrincipal principal) => _principal = principal;
  }
  ```
- **Pattern matching**: Prefer `is` and `switch` expressions over explicit casts
- **String interpolation**: Use `$"text {variable}"` instead of `string.Format`

### MCP Tools

- Mark tool classes with `[McpServerToolType]` attribute
- Use `[McpServerTool, Description("...")]` for tool methods
- Use `[Description("...")]` for parameters
- Inject `ClaimsPrincipal` via constructor for user context

### Error Handling

- Use `ProblemDetails` for HTTP error responses
- Throw meaningful exceptions with context
- Log errors with appropriate levels (`_logger.LogError`)
- Never expose sensitive information in error messages

### Entity Framework

- Use `DbContext` with constructor injection
- Define `DbSet<T>` properties for each entity
- Use in-memory database for development: `options.UseInMemoryDatabase("ToDoDb")`

## Official Documentation

- https://aspire.dev
- https://learn.microsoft.com/dotnet/aspire
- https://nuget.org (for NuGet package details)
