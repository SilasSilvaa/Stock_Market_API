# Stock Market API

Este projeto é uma API de mercado de ações desenvolvida em .NET 8, utilizando Entity Framework para acesso a banco de dados, Identity JWT para autenticação e autorização de usuários, a API [financialmodelingprep.com](https://site.financialmodelingprep.com/) para buscas de ações financeiras, Swagger para documentação da API e PostgreSQL como banco de dados.

## Pré-requisitos

- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Configuração

1. No arquivo `appsettings.json` já possui uma APIKey para testes apenas remova o " - APIKey for testing".

2. Certifique-se de que as portas 5432 (PostgreSQL) e 5067 (API) estão livres no seu ambiente.

## Instalação e Execução

Clone o repositório e execute o comando abaixo para iniciar os containers:

```bash
docker-compose up --build
```

### Esse comando irá:

1. Construir a imagem da aplicação.
2. Inicializar o container do PostgreSQL.
3. Executar as migrações do banco de dados automaticamente.

## Acessando a API

Uma vez que os containers estiverem rodando, você pode acessar a API em:

```bash
http://localhost:5067/swagger
```

## Rotas da API

Todas as requsições precisam de um token JWT, com exeção da rota Account.

### Account

#### POST api/account/register

- Corpo da requisição (JSON):

```json
{
  "userName": "username",
  "email": "email@exemplo.com",
  "password": "strongpassword"
}
```

#### Retorno

```json
{
  "userName": "username",
  "email": "email@exemplo.com",
  "token": "jwt_token"
}
```

#### POST api/account/login

- Corpo da requisição (JSON):

```json
{
  "email": "email@exemplo.com",
  "password": "strongpassword"
}
```

#### Retorno

```json
{
  "userName": "username",
  "email": "email@exemplo.com",
  "token": "jwt_token"
}
```

#### POST api/account/login

- Sem corpo de requisição

- Retorno: HTTP Status 200 (OK)

### Stock API

- Recebe uma string como parametro e retorna uma ação financeira da API
  [financialmodelingprep.com](https://site.financialmodelingprep.com/)

#### GET api/stockbysymbol/{symbol}

- Parâmetro: {symbol} (símbolo da ação)

- Retorno: JSON contendo dados da ação

```json
{
  "symbol": "AAPL",
  "image": "https://financialmodelingprep.com/image-stock/AAPL.png",
  "companyName": "Apple Inc.",
  "price": 166.9,
  "changes": 1.06,
  "lastDiv": 0.96,
  "marketCap": 2577253110000,
  "currency": "USD",
  "description": "...",
  "industry": "Consumer Electronics"
}
```

### Stock DB

- Gerenciamento de Ações financeiras no Banco de Dados

#### GET api/stock

- Obtem todas as ações armazenadas no banco de dados.

- Opções de Query Params:

  - e-mail: e-mail do usuário - Obrigatório

  - orderBySymbol: ordena por símbolo da ação.

  - orderByName: ordena por nome da empresa.

  - orderByPrice: ordena por preço.

  - pageSize: define o número de resultados por página (padrão: 20).

  - pageNumber: define a página a ser retornada (padrão: 1).

Retorno: Lista de objetos Stock Portifólio.

```json
[
  {
    "symbol": "AAPL",
    "image": "https://financialmodelingprep.com/image-stock/AAPL.png",
    "companyName": "Apple Inc.",
    "price": 166.9,
    "changes": 1.06,
    "lastDiv": 0.96,
    "marketCap": 2577253110000,
    "currency": "USD",
    "description": "...",
    "industry": "Consumer Electronics"
  },
  {
    "symbol": "AMZN",
    "image": "https://financialmodelingprep.com/image-stock/AMZN.png",
    "companyName": "Amazon.com, Inc.",
    "price": 179.54,
    "changes": 2.31,
    "lastDiv": 0,
    "marketCap": 1866713288000,
    "currency": "USD",
    "description": "...",
    "industry": "Specialty Retail"
  }
]
```

#### GET api/stock/{id}

- Obtem uma ação específica pelo ID.
- Parâmetro: (int) Id (ID da ação).
- Retorno: Objeto Stock com o ID especificado.

```json
{
  "id": 1,
  "symbol": "AMZN",
  "image": "https://financialmodelingprep.com/image-stock/AMZN.png",
  "companyName": "Amazon.com, Inc.",
  "price": 179.54,
  "changes": 2.31,
  "lastDiv": 0,
  "marketCap": 1866713288000,
  "currency": "USD",
  "description": "...",
  "industry": "Specialty Retail"
}
```

#### POST api/stock

- Cadastra uma nova ação no banco de dados.
- Corpo da requisição (JSON): Objeto Stock completo.

```json
{
  "symbol": "AMZN",
  "image": "https://financialmodelingprep.com/image-stock/AMZN.png",
  "companyName": "Amazon.com, Inc.",
  "price": 179.54,
  "changes": 2.31,
  "lastDiv": 0,
  "marketCap": 1866713288000,
  "currency": "USD",
  "description": "...",
  "industry": "Specialty Retail"
}
```

- Retorno: Objeto Stock criado, incluindo o ID gerado.

```json
{
  "id": 1,
  "symbol": "AMZN",
  "image": "https://financialmodelingprep.com/image-stock/AMZN.png",
  "companyName": "Amazon.com, Inc.",
  "price": 179.54,
  "changes": 2.31,
  "lastDiv": 0,
  "marketCap": 1866713288000,
  "currency": "USD",
  "description": "...",
  "industry": "Specialty Retail"
}
```

#### PUT api/stock

- Atualiza uma ação existente no banco de dados.
- Parâmetro: id (ID da ação).
- Corpo da requisição (JSON): Objeto Stock com as atualizações.

```json
id = 1

  {
    "symbol": "AAPL",
    "image": "https://financialmodelingprep.com/image-stock/AAPL.png",
    "companyName": "Apple Inc.",
    "price": 166.9,
    "changes": 1.06,
    "lastDiv": 0.96,
    "marketCap": 2577253110000,
    "currency": "USD",
    "description": "...",
    "industry": "Consumer Electronics"
  }

```

- Retorno: Objeto Stock atualizado.

```json
{
  "id": 1,
  "symbol": "AAPL",
  "image": "https://financialmodelingprep.com/image-stock/AAPL.png",
  "companyName": "Apple Inc.",
  "price": 166.9,
  "changes": 1.06,
  "lastDiv": 0.96,
  "marketCap": 2577253110000,
  "currency": "USD",
  "description": "...",
  "industry": "Consumer Electronics"
}
```

#### DELETE /stock/{id}`

- Exclui uma ação do banco de dados.
- Parâmetro: id (ID da ação).
- Retorno: HTTP Status 200 (OK) se a ação foi excluída com sucesso, ou HTTP Status 404 (Not - Found) se a ação não foi encontrada.

### Stock Portifolio

#### Get api/stockportfolio

- Obtem todas as ações armazenadas no banco de dados do usuário passado no parametro.

- Opções de Query Params:

  - orderBySymbol: ordena por símbolo da ação (ascendente ou descendente).

  - orderByName: ordena por nome da empresa (ascendente ou descendente).

  - orderByPrice: ordena por preço (ascendente ou descendente).

  - pageSize: define o número de resultados por página (padrão: 20).

  - pageNumber: define a página a ser retornada (padrão: 1).

Retorno: Lista de objetos Stock.

```json
[
  {
    "symbol": "AAPL",
    "image": "https://financialmodelingprep.com/image-stock/AAPL.png",
    "companyName": "Apple Inc.",
    "price": 166.9,
    "changes": 1.06,
    "lastDiv": 0.96,
    "marketCap": 2577253110000,
    "currency": "USD",
    "description": "...",
    "industry": "Consumer Electronics"
  },
  {
    "symbol": "AMZN",
    "image": "https://financialmodelingprep.com/image-stock/AMZN.png",
    "companyName": "Amazon.com, Inc.",
    "price": 179.54,
    "changes": 2.31,
    "lastDiv": 0,
    "marketCap": 1866713288000,
    "currency": "USD",
    "description": "...",
    "industry": "Specialty Retail"
  }
]
```

#### GET api/stockportfolio/{id}

- Obtem uma ação do usuário específica pelo ID.
- Parâmetro: (int) Id (ID da ação).
- Retorno: Objeto Stock com o ID especificado.

```json
{
  "id": 1,
  "symbol": "AMZN",
  "image": "https://financialmodelingprep.com/image-stock/AMZN.png",
  "companyName": "Amazon.com, Inc.",
  "price": 179.54,
  "changes": 2.31,
  "lastDiv": 0,
  "marketCap": 1866713288000,
  "currency": "USD",
  "description": "...",
  "industry": "Specialty Retail"
}
```

#### POST api/stockportfolio

- Cadastra uma nova ação no portifólio do usuário com base nas ações da API [financialmodelingprep.com](https://site.financialmodelingprep.com/).
- Corpo da requisição (JSON): e-mail e o símbolo da ação financeira.

```json
{
  "symbol": "AMZN",
  "email": "user@example.com"
}
```

- Retorno: Objeto Stock, caso não tenha no banco de dados, é feito uma requisição na api criando a ação e retornando ela , incluindo o ID gerado.

```json
{
  "id": 1,
  "symbol": "AMZN",
  "image": "https://financialmodelingprep.com/image-stock/AMZN.png",
  "companyName": "Amazon.com, Inc.",
  "price": 179.54,
  "changes": 2.31,
  "lastDiv": 0,
  "marketCap": 1866713288000,
  "currency": "USD",
  "description": "...",
  "industry": "Specialty Retail",
  "portfolio": []
}
```

#### DELETE /stock/{id}`

- Exclui uma ação do banco de dados.
- Parâmetro: id (ID da ação).
- Retorno: HTTP Status 200 (OK) se a ação foi excluída com sucesso, ou HTTP Status 404 (Not - Found) se a ação não foi encontrada.

## License

[MIT](https://choosealicense.com/licenses/mit/)
