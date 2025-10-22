# Protected MCP Server Sample

This sample demonstrates how to create an MCP server that requires OAuth 2.0 authentication to access its tools and resources. The server provides simple math tools (addition and multiplication) protected by JWT bearer token authentication.

## Overview

The Protected MCP Server sample shows how to:
- Create an MCP server with OAuth 2.0 protection
- Configure JWT bearer token authentication
- Implement protected MCP tools and resources
- Integrate with ASP.NET Core authentication and authorization
- Provide OAuth resource metadata for client discovery

## Prerequisites

- .NET 9.0 or later

## Setup and Running

### Step 1: Start the Aspire Host project

First, you need to start APpHost project:

```bash
cd SampleAspNetCoreMcp.AppHost
dotnet run --lp http
```

The OAuth server will start at `http://localhost:8080` and server will be available at `https://localhost:5522`

> The vscode cliente for mcp does not support https using self-signed certificates yet, so you need to use http endpoint for testing. [#248170](https://github.com/microsoft/vscode/issues/248170) 

## What the Server Provides

### Protected Resources

- **MCP Endpoint**: `http://localhost:5522/` (requires authentication)
- **OAuth Resource Metadata**: `http://localhost:5522/.well-known/oauth-protected-resource`

### Available Tools

The server provides math tools that require authentication:

1. **Add**: Add two numbers
  - Parameters: `a` (double), `b` (double)
  - Example: `Add` with `a: 2.5, b: 4.25` returns `6.75`

2. **Multiply**: Multiply two numbers
  - Parameters: `a` (double), `b` (double)
  - Example: `Multiply` with `a: 3, b: 5` returns `15`

### Authentication Configuration

The server is configured to:
- Accept JWT bearer tokens from the OAuth server at `https://localhost:8080`
- Validate token audience as `apiservice`
- Require tokens to have appropriate scopes (`mcp:tools`)
- Provide OAuth resource metadata for client discovery

## Architecture

The server uses:
- **ASP.NET Core** for hosting and HTTP handling
- **JWT Bearer Authentication** for token validation
- **MCP Authentication Extensions** for OAuth resource metadata
- **Simple in-process logic** for math operations
- **Authorization** to protect MCP endpoints

## Configuration Details

- **Server URL**: `http://localhost:8080`
- **OAuth Server**: `https://localhost:5522`

## Testing Without Client

You can test the server directly using HTTP tools:

1. Get an access token from the OAuth server
2. Include the token in the `Authorization: Bearer <token>` header
3. Make requests to the MCP endpoints

## External Dependencies

No external data dependencies for math tools.

## Troubleshooting

- Ensure the ASP.NET Core dev certificate is trusted.
  ```
  dotnet dev-certs https --clean
  dotnet dev-certs https --trust
  ```