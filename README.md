# SalesControl
Aplicação de exemplo para gerenciar clientes, produtos e vendas com Windows Forms, PostgreSQL e ReportViewer.

Visão geral
- Arquitetura: Clean-ish layered (UI / Application / Domain / Infrastructure)
- Tecnologias: C# 13, .NET 10, WinForms, Entity Framework Core, Npgsql, ReportViewerCore.WinForms, MediatR
- Banco: PostgreSQL (recomenda-se PostgreSQL 15+). Scripts de criação estão na pasta `databaseScript`.

Como executar
1. Configure o banco PostgreSQL e crie a database. Execute os scripts em `databaseScript`.
2. Atualize a connection string no `appsettings.json` do projeto `SalesControl.UI` (chave `DefaultConnection`).
3. Abra a solução no Visual Studio 2022/2023 ou rode via `dotnet`:
   - `dotnet build` na raiz
   - `dotnet run --project src/SalesControl.UI/SalesControl.UI.csproj`

Relatórios (RDLC)
- O projeto usa `ReportViewerCore.WinForms` (controle via código). O arquivo RDLC está em `src/SalesControl.UI/Relatorios/Report1.rdlc` e é incluído como recurso embutido.
- Se o designer apresentar problemas ao associar tipos, use o `.xsd` tipado (`SaleReportDataSet.xsd`) ou passe um `DataTable` em runtime (como implementado no `FormRelatorio`).

Testes
- Testes unitários xUnit estão em `src/SalesControl.Tests`. Para executar:
  - `dotnet test src/SalesControl.Tests/SalesControl.Tests.csproj`

Decisões técnicas e justificativas (detalhado)

- Arquitetura adotada: Clean-style em camadas
  - Por que: separação de responsabilidades melhora manutenção, testabilidade e clareza. O projeto está dividido em:
    - `SalesControl.UI`: interface Windows Forms — responsável apenas por apresentar dados e capturar ações do usuário.
    - `SalesControl.Application`: lógica de aplicação (DTOs, Commands/Queries, Handlers) — orquestra regras sem acesso direto ao EF.
    - `SalesControl.Domain`: entidades de domínio (models, regras e invariantes).
    - `SalesControl.Infrastructure`: implementação de persistência (EF Core + Npgsql), repositórios e serviços concretos.
  - Benefício: facilita swap de infra (por exemplo mudar EF/DB) sem tocar regras de negócio; permite isolar e testar a lógica com mocks.

- Uso de interfaces e abstrações
  - `IRepository<T>`, `IReadRepository<T>` e `ISalesService` expõem contratos usados pela camada de Application/UI.
  - Por que: favorece inversão de dependência, teste unitário (mocks/fakes) e clareza do contrato entre camadas.

- Padrões e bibliotecas
  - MediatR: centraliza Commands/Queries e promove separação entre UI e handlers (Command/Query Responsibility Segregation simples).
  - Ardalis.Result: padrão leve para resultados (sucesso/erros) que evita exceções para fluxo normal.
  - Ardalis.Specification: abstração para queries reutilizáveis no repositório.

- Modelos usados (Entities, DTOs, ViewModels)
  - Entities (Domain): representam invariantes e regras (ex.: `Product`, `Client`, `Sale`). Contêm validações e métodos que preservam consistência (ex.: `DecreaseStock`).
  - DTOs (Application layer): objetos de transferência usados entre Handlers/Services/UI (ex.: `SaleReportRowDto`, `RegisterSaleDto`). Evitam expor entidades diretamente para UI.
  - ViewModels (UI): modelos específicos para interação com formulários (ex.: `RegisterSaleViewModel`). Permitem adaptar a visualização sem poluir domínio.
  - Por que essa separação: promove SRP (Single Responsibility Principle) e evita problemas de mapeamento bidirecional/estado compartilhado entre UI e domínio.

- Persistência e PostgreSQL
  - Usamos Entity Framework Core com `Npgsql` (provider para PostgreSQL). Versão recomendada: PostgreSQL 15+.
  - Por que EF Core: produtividade, mapeamento relational-to-object, migrações (opcional) e integração com LINQ.
  - Observação sobre timestamps: armazenamos `DateTimeOffset.UtcNow` nas entidades e normalizamos filtros para UTC ao consultar para evitar problemas com `timestamptz` e offsets locais.

- Transações e integridade nas vendas
  - Requisito: venda com transação. Implementamos `RegisterSaleAsync` no `SalesService` usando `BeginTransactionAsync` + único `SaveChangesAsync` antes do Commit.
  - Por que: garante atomicidade — se qualquer item falhar (estoque insuficiente), a transação é revertida e nada é persistido.

- Por que `ReportViewerCore.WinForms` (em vez do ReportViewer clássico)
  - O ReportViewer original (Microsoft.ReportViewer.*) não tem suporte oficial moderno para .NET 5+/6+/10 em Windows Forms sem workarounds. `ReportViewerCore.WinForms` é uma alternativa compatível com .NET moderno que fornece controle LocalReport para renderizar RDLC.
  - Vantagens:
    - Funciona com .NET 10 e WinForms sem precisar portar o projeto para .NET Framework.
    - Permite carregar RDLC embutidos e vincular `ReportDataSource` em runtime (aceita `DataTable` ou `IEnumerable<T>`).
  - Decisão prática no projeto: gerar o layout do RDLC no designer quando possível e, em runtime, popular o relatório com um `DataTable` (idempotente e desacoplado do designer que pode ter limitações em projetos SDK-style).

- Escolha do formato de dados para o relatório
  - Usamos `SaleReportRowDto` como estrutura de dados e no runtime criamos um `DataTable` com as mesmas colunas. Por que:
    - Compatibilidade com RDLC: `DataTable` funciona de forma confiável como `ReportDataSource` mesmo quando o designer não consegue refletir tipos de assemblies SDK-style.
    - Simplicidade: evita depender do designer para descobrir tipos e funciona em deploy.

- Async/await e acesso a dados
  - Todas as operações assíncronas (EF Core / Npgsql) usam async/await para não bloquear a UI thread. Em handlers/serviços usamos `async Task` com CancellationToken sempre que aplicável.

- Testes
  - Existem testes unitários básicos cobrindo domínio e `SalesService` (SQLite in-memory) para validar transação/rollback.
  - Por que SQLite in-memory: permite testes rápidos e previsíveis sem depender de infra externa. Para integração completa, recomenda-se testes usando PostgreSQL real em ambiente de CI.


O que falta/possíveis melhorias
- Melhor tratamento e logging centralizado (ILogger).
- Testes de integração com PostgreSQL real.
- Melhoria na UI/UX (WinForms é limitado).
- 
Requisitos mínimos (para rodar localmente)
- Sistema Operacional: Windows (a UI usa WinForms).
- .NET SDK: .NET 10 (instale a versão correspondente do SDK).
- IDE (recomendada): Visual Studio 2022/2023 com workload de .NET Desktop; alternativa: VS Code + extensões C# e .NET CLI.
- Banco de dados: PostgreSQL 15+ (psql ou pgAdmin para executar scripts). Os scripts estão na pasta `databaseScript`.
- Ferramentas: dotnet CLI (dotnet build/run/test), psql (opcional para executar scripts SQL).
- RDLC: para editar relatórios no designer do Visual Studio, instale a extensão "Microsoft RDLC Report Designer" (opcional).

Observações rápidas
- Os testes unitários usam SQLite in-memory (não precisam do PostgreSQL).
- Verifique e ajuste a `DefaultConnection` em `src/SalesControl.UI/appsettings.json` antes de executar.
