# Aspire Guidelines

This repository uses Aspire as an orchestrator for the entire application. It handles configuring dependencies, building, and running the application.

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

## CRITICAL: Always Verify Resources

**After any code change, ALWAYS run `aspire run` and verify all resources are healthy:**
- Use **list resources** to confirm all resources are Running
- Check that Keycloak, API Service, and MCP Inspector are all healthy
- This is MANDATORY before considering any task complete

## General Recommendations

1. Before making any changes always run `aspire run` and inspect resource state
2. Changes to `app-host.cs` require a restart of the application
3. Make changes incrementally and validate with `aspire run`
4. Use Aspire MCP tools to check status and debug issues

## Checking Resources

- Use **list resources** to see current state of all resources
- Use **list console logs** to debug issues
- Use **list structured logs** for detailed telemetry

## Listing Integrations

When adding resources, first use **list integrations** to find available packages. Use versions that align with the Aspire.AppHost.Sdk version.

## Persistent Containers

Avoid persistent containers during early development to prevent state management issues.
