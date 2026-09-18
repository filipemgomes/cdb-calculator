# Simulador de CDB

Aplicação para simulação de investimento em CDB, composta por uma Web API em .NET 10 com ASP.NET Core e um frontend Angular.

O usuário informa o valor inicial e o prazo em meses. A aplicação calcula e apresenta os valores bruto e líquido do investimento.

## Tecnologias

- .NET 10
- ASP.NET Core
- Angular 22.1.5
- Angular CLI 22.1.7
- xUnit 2.5.3
- Coverlet 6.0.0
- SonarAnalyzer.CSharp 10.33.0.1635
- SonarJS 4.2.0

## Estrutura

- `Cdb.Domain` — concentra validação, cálculo de rendimento e imposto para manter a regra de negócio independente da camada HTTP.
- `Cdb.WebApi` — recebe requisições e devolve respostas HTTP, delegando a regra de negócio ao domínio.
- `Cdb.Domain.Tests` — testa diretamente a camada lógica.
- `cdb-web` — responsável pela interface e pelo consumo da API.

Todos os projetos podem ser acessados pela solução `Cdb.sln`.

## Fluxo da simulação

```text
requisição
  → Cdb.WebApi
  → CdbSimulationService
      → InvestmentValidator
      → YieldCalculator
      → TaxCalculator
  → resposta da WebApi
  → exibição no Angular
```

O `CdbSimulationService` coordena a validação do valor e do prazo, o cálculo do rendimento mês a mês e o cálculo do imposto, cuja alíquota depende do prazo. A simulação produz os resultados bruto e líquido, que são devolvidos pela API e apresentados pelo Angular.

## Executando a API

```bash
dotnet run --project src/Cdb.WebApi/Cdb.WebApi.csproj --urls http://localhost:44300
```

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

```bash
cd src/cdb-web
npm install
npm start
```

O frontend ficará disponível em:

```text
http://localhost:4200
```

O proxy Angular encaminha as requisições `/api` para:

```text
http://localhost:44300
```

## Testes

Testes da camada lógica:

```bash
dotnet test tests/Cdb.Domain.Tests/Cdb.Domain.Tests.csproj
```

Cobertura:

```bash
dotnet test tests/Cdb.Domain.Tests/Cdb.Domain.Tests.csproj --collect:"XPlat Code Coverage"
```

A camada lógica deve permanecer com cobertura acima de 90%.

Testes do Angular:

```bash
cd src/cdb-web
npm test
```

Lint:

```bash
npm run lint
```

Build do frontend:

```bash
npm run build
```

## Regras da simulação

O cálculo considera CDI de 0,9% e TB de 108% do CDI, com rendimento acumulado mês a mês.

O imposto incide somente sobre o rendimento da aplicação:

- até 6 meses: 22,5%
- até 12 meses: 20%
- até 24 meses: 17,5%
- acima de 24 meses: 15%
