# Documentação da API RESTful de Ações

## Introdução

Esta documentação descreve os endpoints e métodos disponíveis na nossa API REST FULL de Ações. Ela permite que você consulte informações sobre ações, preços, históricos e muito mais.

## Base URL

A URL base para todos os endpoints da API é `https://api.exemplo.com`.

## Recursos Disponíveis

Aqui estão os principais recursos disponíveis na API:

### Ações

#### Consultar Detalhes de uma Ação

- **Endpoint:** `/stock/{symbol}`
- **Método:** GET
- **Descrição:** Retorna os dados detalhados de uma ação com base no símbolo.
- **Parâmetros de Rota:**
  - `{symbol}`: O símbolo da ação que você deseja consultar.
- **Exemplo de Uso:**
- **GET** https://api.exemplo.com/stock/AAPL


#### Consultar Gráficos de uma Ação

- **Endpoint:** `/stock/{symbol}/charts/{time}`
- **Método:** GET
- **Descrição:** Retorna gráficos de preços de uma ação com base no símbolo e no período de tempo especificados.
- **Parâmetros de Rota:**
- `{symbol}`: O símbolo da ação para a qual você deseja obter gráficos.
- `{time}`: O período de tempo dos gráficos (por exemplo, "1d" para um dia, "1w" para uma semana, "1m" para um mês).
- **Exemplo de Uso:**
- **GET** https://api.exemplo.com/stock/AAPL/charts/1d
- **GET** https://api.exemplo.com/stock/AAPL/charts/1w



## Respostas da API

A API retorna respostas em formato JSON, seguindo um padrão comum. Um exemplo de resposta para a consulta de detalhes de uma ação seria:

```json
{
  "symbol": "AAPL",
  "name": "Apple Inc.",
  "exchange": "NASDAQ",
  "price": 150.00,
  "last_updated": "2023-10-01T12:00:00Z"
}
```

Para os gráficos de ações, a resposta pode incluir dados de séries temporais, dependendo do período de tempo especificado.

## Erros da API

A API utiliza códigos de status HTTP padrão para indicar o resultado da solicitação. Em caso de erro, uma resposta JSON com detalhes adicionais será retornada.
```json
{
  "error_code": 404,
  "message": "Ação não encontrada."
}
```
