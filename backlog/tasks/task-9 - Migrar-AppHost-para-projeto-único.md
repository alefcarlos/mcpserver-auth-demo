---
id: TASK-9
title: Migrar AppHost para projeto único
status: Done
assignee: []
created_date: '2026-02-22 06:05'
updated_date: '2026-02-22 07:23'
labels: []
dependencies: []
priority: medium
ordinal: 3000
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
## Objetivo
Migrar o AppHost de projeto tradicional (.csproj) para File-Based App do .NET 10, mantendo os arquivos na raiz do projeto.

## Estrutura atual (raiz do projeto)
```
mcpserver-auth-demo/
├── app-host.cs              # File-based app com diretivas #:
├── app-host.run.json        # Launch profile
├── SampleAspNetCoreMcp.ApiService/
├── SampleAspNetCoreMcp.ServiceDefaults/
└── ...
```

## Implementação realizada

### 1. Criado app-host.run.json na raiz
Contém os dois profiles existentes (https e dev.localhost).

### 2. Criado app-host.cs na raiz
- Adicionado diretivas `#:` no topo
- Adicionado pacotes: `Aspire.Hosting.Keycloak`, `CommunityToolkit.Aspire.Hosting.McpInspector`
- Removido `.WithRealmImport("./keycloak/realms")` (pasta não existente)

### 3. Removido pasta SampleAspNetCoreMcp.AppHost/

### 4. Atualizado arquivos
- `.aspire/settings.json` - apontando para app-host.cs
- `SampleAspNetCoreMcp.slnx` - removido referência ao AppHost antigo

### 5. Testado
- `dotnet build app-host.cs` - OK
- `aspire run` - OK (AppHost iniciando corretamente)
<!-- SECTION:DESCRIPTION:END -->
