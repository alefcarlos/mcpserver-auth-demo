---
id: TASK-10
title: Migrar projeto para Central Package Manager (CPM)
status: Done
assignee: []
created_date: '2026-02-22 06:38'
updated_date: '2026-02-22 07:23'
labels: []
dependencies: []
priority: medium
ordinal: 2000
---

## Description

<!-- SECTION:DESCRIPTION:BEGIN -->
## Pacotes atuais que precisam ser centralizados:

### ApiService.csproj
- Microsoft.AspNetCore.Authentication.JwtBearer → 10.0.3
- Microsoft.EntityFrameworkCore.InMemory → 10.0.0
- ModelContextProtocol.AspNetCore → 0.9.0-preview.1

### ServiceDefaults.csproj
- Microsoft.Extensions.Http.Resilience → 10.3.0
- Microsoft.Extensions.ServiceDiscovery → 10.3.0
- OpenTelemetry.* → 1.15.0 (5 pacotes)

### app-host.cs (Aspire)
- Aspire.Hosting.Keycloak → 13.1.1-preview.1.26105.8
- CommunityToolkit.Aspire.Hosting.McpInspector → 13.1.1

## Passos para executar:
1. Criar Directory.Packages.props com ManagePackageVersionsCentrally=true
2. Definir todos os PackageVersion no arquivo central
3. Remover Version="..." dos .csproj (manter apenas Include="...")
4. Para o app-host.cs, manter as versões inline (não é um .csproj padrão)

## Referências
- https://learn.microsoft.com/nuget/consume-packages/central-package-management
<!-- SECTION:DESCRIPTION:END -->
