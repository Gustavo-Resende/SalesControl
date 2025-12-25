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

Decisões técnicas e observações
- Armazene timestamps em UTC (o código usa `DateTimeOffset.UtcNow` nas entidades e normaliza filtros para UTC ao consultar PostgreSQL).
- O ReportViewer clássico não oferece designer no .NET moderno; usamos `ReportViewerCore.WinForms` e criamos o controle por código.
- Use transações ao registrar vendas; o `SalesService` garante atomicidade e faz um único `SaveChanges` para persistir alterações.

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
