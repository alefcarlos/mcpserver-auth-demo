# AGENTS.md

This repository is set up to use Aspire. Aspire is an orchestrator for the entire application and will take care of configuring dependencies, building, and running the application. The resources that make up the application are defined in `app-host.cs` including application code and external dependencies.

## Project Overview

- **Solution**: `SampleAspNetCoreMcp.slnx`
- **Framework**: .NET 10.0
- **Projects**:
  - `SampleAspNetCoreMcp.ApiService` - Main MCP server with JWT authentication
  - `SampleAspNetCoreMcp.ServiceDefaults` - Shared Aspire service defaults
- **Language**: C# 12+ with nullable reference types enabled

## Build Commands

```bash
# Build the entire solution
dotnet build

# Run the API service directly (without Aspire)
dotnet run --project SampleAspNetCoreMcp.ApiService

# Run with Aspire (includes Keycloak, API service, MCP Inspector)
aspire run

# Build a specific project
dotnet build SampleAspNetCoreMcp.ApiService/SampleAspNetCoreMcp.ApiService.csproj
```

## Testing

**No test projects currently exist** - This is a demo project.

To add testing in the future, consider:
```bash
# Add xUnit test project
dotnet new xunit --project SampleAspNetCoreMcp.ApiService.Tests

# Run all tests
dotnet test

# Run a single test (example syntax when tests exist)
dotnet test --filter "FullyQualifiedName~TestClassName.TestMethodName"
```

## Running the Application

```bash
aspire run
```

If there is already an instance of the application running it will prompt to stop the existing instance. You only need to restart the application if code in `app-host.cs` is changed.

### Endpoints

- **API Service**: http://localhost:5522
- **MCP Endpoint**: http://localhost:5522/mcp
- **OAuth Server (Keycloak)**: http://localhost:8080
- **MCP Inspector**: http://localhost:6274

### Test Credentials

- **Email**: alef@alef.com
- **Password**: 123

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

## Aspire Guidelines

### CRITICAL: Always Verify Resources

**After any code change, ALWAYS run `aspire run` and verify all resources are healthy:**
- Use **list resources** to confirm all resources are Running
- Check that Keycloak, API Service, and MCP Inspector are all healthy
- This is MANDATORY before considering any task complete

### General Recommendations

1. Before making any changes always run `aspire run` and inspect resource state
2. Changes to `app-host.cs` require a restart of the application
3. Make changes incrementally and validate with `aspire run`
4. Use Aspire MCP tools to check status and debug issues

### Checking Resources

- Use **list resources** to see current state of all resources
- Use **list console logs** to debug issues
- Use **list structured logs** for detailed telemetry

### Listing Integrations

When adding resources, first use **list integrations** to find available packages. Use versions that align with the Aspire.AppHost.Sdk version.

### Persistent Containers

Avoid persistent containers during early development to prevent state management issues.

## Backlog.md MCP Guidelines

This project uses Backlog.md MCP for all task and project management activities.

**CRITICAL GUIDANCE**

- If your client supports MCP resources, read `backlog://workflow/overview` to understand when and how to use Backlog for this project.
- If your client only supports tools or the above request fails, call `backlog.get_workflow_overview()` tool to load the tool-oriented overview (it lists the matching guide tools).

- **First time working here?** Read the overview resource IMMEDIATELY to learn the workflow
- **Already familiar?** You should have the overview cached ("## Backlog.md Overview (MCP)")
- **When to read it**: BEFORE creating tasks, or when you're unsure whether to track work

These guides cover:
- Decision framework for when to create tasks
- Search-first workflow to avoid duplicates
- Links to detailed guides for task creation, execution, and finalization
- MCP tools reference

You MUST read the overview resource to understand the complete workflow. The information is NOT summarized here.

## Official Documentation

- https://aspire.dev
- https://learn.microsoft.com/dotnet/aspire
- https://nuget.org (for NuGet package details)
