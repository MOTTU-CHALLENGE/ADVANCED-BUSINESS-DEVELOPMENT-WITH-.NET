
<h1 align="center">Mottu Moto API  </h1>
<p align="center">
  <img src="https://img.shields.io/badge/version-1.0.0-blue.svg" />
  </a>
</p>


API RESTful desenvolvida em ASP.NET Core 8.0 para gerenciamento de motos no pátio da Mottu, utilizando Oracle como banco de dados e Entity Framework Core como ORM.
**Autor:**  
Seu nome aqui

## Sumário

- [Descrição](#descrição)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Configuração do Projeto](#configuração-do-projeto)
- [Banco de Dados](#banco-de-dados)
- [Endpoints](#endpoints)
  - [Listar todas as motos](#listar-todas-as-motos)
  - [Filtrar motos por marca/modelo](#filtrar-motos-por-marcamodelo)
  - [Filtrar motos por andar](#filtrar-motos-por-andar)
  - [Buscar moto por ID](#buscar-moto-por-id)
  - [Adicionar nova moto](#adicionar-nova-moto)
  - [Atualizar moto](#atualizar-moto)
  - [Remover moto](#remover-moto)
- [Exemplos de Requests e Responses](#exemplos-de-requests-e-responses)
- [Swagger](#swagger)
- [Como rodar o projeto](#como-rodar-o-projeto)

---

## Descrição

Esta API permite o cadastro, consulta, atualização e remoção de motos em um pátio (CRUD). Cada moto possui as informações modelo, placa, chassi, vaga e andar.

## Tecnologias Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core 9.0.4
- Oracle.EntityFrameworkCore 9.23.80
- Swashbuckle (Swagger) 6.6.2

## Configuração do Projeto

1. **Clonar o repositório**
2. **Configurar a string de conexão**  
   Edite o arquivo [`appsettings.json`](appsettings.json) com os dados do seu banco Oracle:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data source=oracle.fiap.com.br:1521/orcl;User Id=user;Password=password;"
   }
   ```

3. **Restaurar pacotes e rodar as migrações**
   ```sh
   dotnet restore
   dotnet ef database update
   ```

4. **Executar a aplicação**
   ```sh
   dotnet run
   ```

## Banco de Dados

A tabela principal é `Motos` com os seguintes campos:

- `Id` (int, PK, auto-generated)
- `Modelo` (string)
- `Placa` (string)
- `Chassi` (string)
- `Vaga` (string) (porque pode ser vaga A4 ou G6, etc.)
- `Andar` (int)

## Endpoints

### Listar todas as motos

- **GET** `/api/motos`

### Filtrar motos por modelo

- **GET** `/api/motos/filtrar?marca=Honda`

### Filtrar motos por andar

- **GET** `/api/motos/andar/{andar}`

### Buscar moto por ID

- **GET** `/api/motos/{id}`

### Adicionar nova moto

- **POST** `/api/motos`

### Atualizar moto

- **PUT** `/api/motos/{id}`

### Remover moto

- **DELETE** `/api/motos/{id}`

## Exemplos de Requests e Responses

### 1. Listar todas as motos

**Request:**
```http
GET /api/motos
```

**Response:**
```json
[
  {
    "id": 1,
    "modelo": "Honda",
    "placa": "ABC1234",
    "chassi": "9C2KC1670FR123456",
    "vaga": "A1",
    "andar": 2
  },
  {
    "id": 2,
    "modelo": "Yamaha",
    "placa": "XYZ5678",
    "chassi": "9C2KC1670FR654321",
    "vaga": "B2",
    "andar": 1
  }
]
```

### 2. Filtrar motos por marca/modelo

**Request:**
```http
GET /api/motos/filtrar?marca=Honda
```

**Response:**
```json
[
  {
    "id": 1,
    "modelo": "Honda",
    "placa": "ABC1234",
    "chassi": "9C2KC1670FR123456",
    "vaga": "A1",
    "andar": 2
  }
]
```

### 3. Filtrar motos por andar

**Request:**
```http
GET /api/motos/andar/2
```

**Response:**
```json
[
  {
    "id": 1,
    "modelo": "Honda",
    "placa": "ABC1234",
    "chassi": "9C2KC1670FR123456",
    "vaga": "A1",
    "andar": 2
  }
]
```

### 4. Buscar moto por ID

**Request:**
```http
GET /api/motos/1
```

**Response:**
```json
{
  "id": 1,
  "modelo": "Honda",
  "placa": "ABC1234",
  "chassi": "9C2KC1670FR123456",
  "vaga": "A1",
  "andar": 2
}
```

### 5. Adicionar nova moto

**Request:**
```http
POST /api/motos
Content-Type: application/json

{
  "modelo": "Suzuki",
  "placa": "DEF4321",
  "chassi": "9C2KC1670FR789012",
  "vaga": "C3",
  "andar": 3
}
```

**Response:**
```json
{
  "id": 3,
  "modelo": "Suzuki",
  "placa": "DEF4321",
  "chassi": "9C2KC1670FR789012",
  "vaga": "C3",
  "andar": 3
}
```

### 6. Atualizar moto

**Request:**
```http
PUT /api/motos/3
Content-Type: application/json

{
  "id": 3,
  "modelo": "Suzuki",
  "placa": "DEF4321",
  "chassi": "9C2KC1670FR789012",
  "vaga": "C4",
  "andar": 4
}
```

**Response:**
```
204 No Content
```

### 7. Remover moto

**Request:**
```http
DELETE /api/motos/3
```

**Response:**
```
204 No Content
```

## Swagger

A documentação interativa da API pode ser acessada em:

```
http://localhost:5231/swagger
```

## Como rodar o projeto

1. Configure o banco Oracle e ajuste a string de conexão em [`appsettings.json`](appsettings.json).
2. Execute as migrações com `dotnet ef database update`.
3. Inicie a API com `dotnet run`.
4. Acesse o Swagger para testar os endpoints.
5. Prontinho, já está rodando!

## Acadêmico
##### Este é um projeto acadêmico feito em colaboração com a FIAP e Mottu, como parte do curso de Análise e Desenvolvimento de Sistemas. 
---


