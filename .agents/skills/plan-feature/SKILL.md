---
name: plan-feature
description: Quebra uma feature em tarefas menores de implementação usando Backlog.md
license: MIT
metadata:
  audience: developers
  workflow: backlog-planning
---

## Quando usar
Quando o usuário quiser criar tarefas de implementação para uma nova feature.

## Pré-requisitos
- Usuário deve fornecer a descrição da feature
- Usuário pode fornecer referências (links, arquivos, tasks existentes)

## Como funciona

### 1. Coletar contexto da feature
- Pergunte ao usuário a descrição completa da feature
- Pergunte se há algum documento/referência (link, arquivo, task do backlog, etc.)
- Se houver referências, analise-as para enrich o contexto

### 2. Analisar a feature
Identifique os domínios envolvidos:
- **Backend**: models, repositories, services, controllers, endpoints
- **Frontend**: components, pages, services, states
- **Database**: migrations, schema changes, entidades
- **Auth**: permissions, roles, policies, authentication
- **Infra**: config, deploy, docker, environment
- **Docs**: documentação, swagger, readme
- **Tests**: unit tests, integration tests, e2e

Identifique dependências entre componentes e ordene as tarefas logicamente.

### 3. Gerar tarefas menores
Para cada área identificada, gere tarefas específicas com:
- **Title**: claro e objetivo (ex: "Criar modelo de Usuário no banco")
- **Descrição**: contexto da tarefa + objetivo
- **Definition of Done**: apropriado ao tipo de tarefa
- **Labels**: relevantes (ex: "backend", "api", "database")
- **Priority**: high/medium/low baseada na dependência e criticidade

#### Definição de Done por Tipo

**Código/Implementação**:
- Código escrito
- Testes unitários criados
- Código revisado

**API/Endpoint**:
- Endpoint implementado
- Documentado (Swagger)
- Testado

**Database**:
- Migration/schema aplicada
- Validada em ambiente dev

**UI/Frontend**:
- Componente implementado
- Testado visualmente
- Responsivo

**Infra**:
- Configuração aplicada
- Verificada em ambiente dev

### 4. Criar no Backlog.md
Para cada tarefa gerada:
1. Use `backlog_task_create` para criar a task
2. Defina título, descrição, priority e labels
3. Adicione as Definition of Done apropriadas
4. Para tarefas com dependência, adicione nas dependencies

### 5. Finalizar
Apresente ao usuário a lista de tarefas criadas com seus IDs
Confirme se deseja ajustar algo ou criar mais tarefas
