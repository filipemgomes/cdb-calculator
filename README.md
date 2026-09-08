# Simulador de CDB

Aplicação para simulação de investimento em CDB, composta por uma Web API em .NET Framework e uma interface em Angular.

O usuário informa o valor inicial e o prazo em meses. A aplicação calcula e apresenta os valores bruto e líquido do investimento.

## Tecnologias

- .NET Framework 4.8
- ASP.NET Web API 2
- Angular
- xUnit
- SonarAnalyzer / SonarJS

## Estrutura

- `Cdb.Domain` — regras de cálculo, imposto e validação
- `Cdb.WebApi` — API responsável pela simulação
- `Cdb.Domain.Tests` — testes da camada de negócio
- `cdb-web` — interface Angular

Todos os projetos podem ser acessados pela solução `Cdb.sln`.

## Executando a API

Abra `Cdb.sln` no Visual Studio 2022 e execute o projeto `Cdb.WebApi`.

A API será iniciada em:

```text
http://localhost:44300
```

Endpoint da simulação:

```text
POST /api/cdb/simular
```

Exemplo de requisição:

```json
{
  "valorInicial": 1000,
  "prazoMeses": 2
}
```

## Executando o Angular

Na pasta do projeto:

```bash
cd src/cdb-web
npm install
npm start
```

A aplicação ficará disponível em:

```text
http://localhost:4200
```

Para funcionar normalmente, a Web API também deve estar em execução.

## Testes

Testes da camada de negócio:

```bash
dotnet test tests/Cdb.Domain.Tests/Cdb.Domain.Tests.csproj
```

Para gerar o relatório de cobertura:

```bash
dotnet test tests/Cdb.Domain.Tests/Cdb.Domain.Tests.csproj --collect:"XPlat Code Coverage"
```

Testes do Angular:

```bash
cd src/cdb-web
npm test
```

Lint:

```bash
npm run lint
```

Build:

```bash
npm run build
```

## Regras da simulação

O cálculo considera CDI de 0,9% e taxa bancária de 108% do CDI, com rendimento acumulado mês a mês.

O imposto incide sobre o rendimento da aplicação e varia conforme o prazo:

- até 6 meses: 22,5%
- até 12 meses: 20%
- até 24 meses: 17,5%
- acima de 24 meses: 15%
