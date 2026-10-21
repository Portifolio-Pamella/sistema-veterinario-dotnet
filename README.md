# 🐾 Sistema Veterinário API

API RESTful desenvolvida em **ASP.NET Core** para o gerenciamento de uma clínica veterinária.

O sistema permite o controle de **Tutores**, seus **Pets** e os **Veterinários** responsáveis, garantindo a integridade dos dados através de uma **arquitetura em camadas** e integração com **Oracle** e **MongoDB**, utilizando uma estratégia de **Persistência Híbrida**.

A aplicação também conta com recursos avançados de **observabilidade corporativa**, incluindo:

- 🩺 Health Checks
- 📝 Logging Estruturado com Serilog
- 📊 Tracing e Métricas com OpenTelemetry
- 🔐 Autenticação JWT
- 🧪 Testes unitários e de integração

---

## 👥 Integrantes

| Integrante | RM |
|---|---:|
| Lucas Matsubara Reis | RM565020 |
| João Pedro Camilo | RM562005 |
| Pamella Christiny | RM565206 |

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Utilização |
|---|---|
| **ASP.NET Core 10.0** | Desenvolvimento da API RESTful |
| **Entity Framework Core** | ORM e acesso ao banco relacional |
| **Oracle Database** | Persistência relacional |
| **MongoDB** | Persistência NoSQL |
| **Swagger (OpenAPI)** | Documentação da API |
| **OpenTelemetry** | Rastreamento distribuído e métricas de desempenho |
| **Serilog** | Logging estruturado |
| **JWT (JSON Web Token)** | Autenticação e segurança |
| **xUnit & Moq** | Testes automatizados |

---

## 📋 Pré-requisitos

Para executar o projeto, certifique-se de ter instalado:

- [.NET SDK 10.0]
- Oracle Database (**Xe, 19c ou superior**)
- SQL Developer
- Conexão com o **MongoDB Atlas** configurada via `appsettings`

---

# 🚀 Como Executar

## 1. Clonar o repositório

```bash
git clone git@github.com:Portifolio-Pamella/sistema-veterinario-dotnet.git
```

## 2. Configuração do Banco

Crie um usuário no seu **Oracle** e configure o **MongoDB Atlas**.

No arquivo `appsettings.json`, atualize as strings de conexão:

```json
"ConnectionStrings": {
  "OracleConnection": "User Id=USUARIO;Password=SENHA;Data Source=localhost:1521/xe;"
},
"GameStoreDatabase": {
  "ConnectionString": "sua_string_conexao_mongodb",
  "DatabaseName": "SistemaVeterinarioNoSqlDb",
  "CollectionName": "LogsAtendimentos"
}
```

## 3. Aplicar Migrations

No terminal, na pasta do projeto, execute:

```bash
dotnet ef database update
```

Isso criará as tabelas e executará as **Triggers** e **Sequences** necessárias.

## 4. Executar a aplicação

```bash
dotnet run --project .\SistemaVeterinario.API\SistemaVeterinario.API.csproj
```

Após iniciar a aplicação, acesse:

**Swagger:** http://localhost:5262/swagger

---

## ▶️ Como Executar os Testes

O projeto possui uma suíte organizada de:

- 🧪 Testes unitários utilizando **xUnit e Moq**
- 🔗 Testes de integração utilizando **WebApplicationFactory**

Para rodar todos os testes da solução, execute o comando na raiz do repositório:

```bash
dotnet test
```

---

# 📊 Monitoramento e Health Checks

A API dispõe de endpoints dedicados para verificação de **saúde e disponibilidade da aplicação**, além da conectividade com o banco de dados.

### Health Checks

Utilize os caminhos configurados na API:

```text
/health/live
/health/ready
```

Esses endpoints permitem verificar se:

- O serviço está operacional;
- O Oracle está operacional.

### OpenTelemetry & Logging

O sistema coleta:

- Métricas de desempenho;
- Tempo de resposta;
- Taxa de erros;
- Rastreamento de requisições entre camadas;
- Logs estruturados.

Os logs são exibidos diretamente no **console**.

---

# 🔐 Autenticação e Segurança (JWT)

Os endpoints de gerenciamento da API são protegidos por tokens **JWT (JSON Web Token)**.

Para acessar as rotas de:

- Tutores
- Pets
- Veterinários

é necessário autenticar-se previamente.

## 🔑 Como usar a Autenticação

### 1. Gerar o Token

Acesse a documentação do **Swagger** e faça uma requisição:

```http
POST /api/Auth/login
```

Enviando o JSON de credenciais padrão:

```json
{
  "username": "admin",
  "password": "123456"
}
```

### 2. Copiar o Token

Na resposta da requisição de login, o sistema retornará um **token JWT**.

Copie a string gerada.

### 3. Autorizar no Swagger

No Swagger:

1. Clique no botão **Authorize** (ícone de cadeado) localizado no canto superior direito da página.
2. No campo de inserção, digite a palavra `Bearer`, seguida de um espaço, e cole o token copiado.

Exemplo:

```text
Bearer eyJhbGciOi...
```

3. Clique em **Authorize**.
4. Feche a janela.

Agora você terá acesso liberado para executar as operações protegidas da API.

---

# 🔎 Paginação, Ordenação, Filtros e HATEOAS

As consultas de listagem (`GET`) de **Veterinários, Tutores e Pets** contam com suporte completo a:

### 📄 Paginação

Controlada através dos parâmetros de query string:

```text
pageNumber
pageSize
```

### ↕️ Ordenação Dinâmica

Suporte aos parâmetros:

```text
sortBy
ascending
```

O parâmetro `ascending` aceita:

```text
true
false
```

### 🔍 Filtros

Filtros específicos por entidade e busca textual global utilizando o parâmetro:

```text
search
```

### 🔗 HATEOAS

As respostas seguem o modelo paginado (`PagedResponse`) contendo:

- Itens;
- Metadados de paginação;
- Links dinâmicos:
  - `self`
  - `next_page`
  - `prev_page`
- Links individuais de navegação em cada registro.

---

# 🏗️ Arquitetura do Projeto

O sistema foi construído seguindo uma **Arquitetura em Camadas**:

```text
Data
Models
Repositories
Services
DTOs
Controllers
```

## 💾 Persistência Híbrida

A aplicação utiliza:

- **EF Core + Oracle** para as entidades principais:
  - Tutores
  - Pets
  - Veterinários

- **MongoDB (NoSQL)** para:
  - Documentos flexíveis
  - Histórico de atendimentos

## 🌐 API

Desenvolvida seguindo o padrão **REST**, com tratamento global de erros para assegurar retornos HTTP semânticos:

| Código | Significado |
|---:|---|
| `200` | OK |
| `201` | Created |
| `204` | No Content |
| `400` | Bad Request |
| `401` | Unauthorized |
| `404` | Not Found |

---

# 📚 Documentação das Rotas

## 🔑 Autenticação — `/api/Auth`

### `POST /login`

Autentica o usuário e retorna o token JWT de acesso.

---

## 🩺 Veterinário — `/api/Veterinario`

> 🔐 **Protegido por JWT**

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/Veterinario` | Lista todos os veterinários |
| `POST` | `/api/Veterinario` | Cadastra um novo veterinário |
| `GET` | `/api/Veterinario/{id}` | Busca um veterinário específico |
| `PUT` | `/api/Veterinario/{id}` | Atualiza um registro |
| `DELETE` | `/api/Veterinario/{id}` | Remove um registro |

O `GET` suporta:

- Paginação;
- Ordenação;
- Filtros por especialidade;
- Busca por `search`.

---

## 👤 Tutor — `/api/Tutor`

> 🔐 **Protegido por JWT**

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/Tutor` | Lista todos os tutores |
| `POST` | `/api/Tutor` | Cadastra um novo tutor |
| `GET` | `/api/Tutor/{id}` | Busca um tutor por ID |
| `PUT` | `/api/Tutor/{id}` | Atualiza um registro de tutor |
| `DELETE` | `/api/Tutor/{id}` | Remove um registro de tutor |

O `GET` suporta:

- Paginação;
- Ordenação;
- Filtros por cidade;
- Busca por `search`.

---

## 🐶 Pet — `/api/Pet`

> 🔐 **Protegido por JWT**

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/Pet` | Lista todos os pets |
| `POST` | `/api/Pet` | Cadastra um novo pet vinculado a um tutor |
| `GET` | `/api/Pet/{id}` | Busca um pet específico por ID |
| `PUT` | `/api/Pet/{id}` | Atualiza os dados do pet |
| `DELETE` | `/api/Pet/{id}` | Remove um registro de pet |

O `GET` suporta:

- Paginação;
- Ordenação;
- Filtros por espécie;
- Busca por `search`.

---

## 📋 Histórico de Atendimento — NoSQL

### `/api/HistoricoAtendimento`

> 🔐 **Protegido por JWT**

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/HistoricoAtendimento` | Lista todos os históricos armazenados no MongoDB |
| `GET` | `/api/HistoricoAtendimento/pet/{idPet}` | Busca o histórico de atendimento de um pet específico pelo ID no MongoDB |
| `POST` | `/api/HistoricoAtendimento` | Cadastra um novo registro de histórico no MongoDB |

---

# 🧪 Exemplos de JSON para Teste

## 🩺 Cadastro de Veterinário

```json
{
  "nomeVeterinario": "Dra. Maria Teste",
  "crmVeterinario": "CRM-12345",
  "especialidadeVeterinario": "Felinos",
  "telefoneVeterinario": "11999999999",
  "emailVeterinario": "teste@email.com",
  "statusVeterinario": "Ativo",
  "dataCadastroVeterinario": "2026-05-23T21:00:00"
}
```

---

## 👤 Cadastro de Tutor

```json
{
  "nomeTutor": "João Silva",
  "cpfTutor": "123.456.789-00",
  "telefoneTutor": "(11) 99999-8888",
  "emailTutor": "joao.silva@email.com",
  "cepTutor": "01001-000",
  "ruaTutor": "Avenida Paulista",
  "numeroTutor": "1000",
  "complementoTutor": "Apto 52",
  "bairroTutor": "Bela Vista",
  "cidadeTutor": "São Paulo",
  "estadoTutor": "SP",
  "dataCadastroTutor": "2026-05-24T01:03:42.967Z"
}
```

---

## 🐶 Cadastro de Pet

> ⚠️ **Certifique-se de usar um `idTutor` existente.**

```json
{
  "idTutor": 1,
  "nomePet": "Rex",
  "especiePet": "Cachorro",
  "racaPet": "Golden Retriever",
  "sexoPet": "Macho",
  "dataNascimentoPet": "2024-01-15T00:00:00Z",
  "pesoPet": 25.5,
  "corPet": "Dourado",
  "dataCadastroPet": "2026-05-24T01:03:42.967Z"
}
```

---

## 📋 Cadastro de Histórico de Atendimento (MongoDB)

```json
{
  "idPet": 1,
  "descricaoAtendimento": "Consulta de rotina e aplicação de vacina antirrábica.",
  "dataAtendimento": "2026-10-21T23:45:00Z"
}
```

---


---

## ✨ Principais Recursos

- 🐾 Gerenciamento de Pets
- 👤 Gerenciamento de Tutores
- 🩺 Gerenciamento de Veterinários
- 📋 Histórico de atendimentos com MongoDB
- 🗄️ Persistência híbrida Oracle + MongoDB
- 🔐 Autenticação JWT
- 📄 Paginação
- ↕️ Ordenação dinâmica
- 🔎 Filtros e busca textual
- 🔗 HATEOAS
- 🩺 Health Checks
- 📝 Logging estruturado com Serilog
- 📊 OpenTelemetry
- 📚 Swagger / OpenAPI
- 🧪 Testes unitários e de integração
- ⚠️ Tratamento global de erros
- 🌐 API RESTful