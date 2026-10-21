```markdown
# Sistema Veterinário API

Este projeto é uma API RESTful desenvolvida em **ASP.NET Core** para o gerenciamento de uma clínica veterinária. O sistema permite o controle de **Tutores**, seus **Pets** e os **Veterinários** responsáveis, garantindo a integridade dos dados através de uma arquitetura em camadas e integração com banco de dados **Oracle**. 

A aplicação conta também com recursos avançados de observabilidade corporativa, incluindo **Health Checks**, **Logging Estruturado com Serilog**, **Tracing/Métricas com OpenTelemetry**, **Autenticação JWT**, além de uma suíte completa de **testes unitários e de integração**.

## Integrantes
- **Lucas Matsubara Reis** | RM565020
- **João Pedro Camilo** | RM562005
- **Pamella Christiny** | RM565206

## Tecnologias Utilizadas
- **ASP.NET Core 10.0**
- **Entity Framework Core** (ORM)
- **Oracle Database**
- **Swagger (OpenAPI)** para documentação
- **OpenTelemetry** para rastreamento distribuído e métricas de desempenho
- **Serilog** para logging estruturado
- **JWT (JSON Web Token)** para autenticação e segurança
- **xUnit & Moq** para testes automatizados

---

## Pré-requisitos
Para executar o projeto, certifique-se de ter instalado:
- [.NET SDK 10.0](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Oracle Database](https://www.oracle.com/br/database/technologies/oracle-database-software-downloads.html) (Xe, 19c ou superior)
- [SQL Developer](https://www.oracle.com/br/database/sqldeveloper/technologies/download.html)

## Como Executar
1. **Clone o repositório:**
   ```bash
   git clone git@github.com:Portifolio-Pamella/sistema-veterinario-dotnet.git

```

2. **Configuração do Banco:**
Crie um usuário no seu Oracle. No arquivo `appsettings.json`, atualize a `OracleConnection` com suas credenciais:
```json
"ConnectionStrings": {
  "OracleConnection": "User Id=USUARIO;Password=SENHA;Data Source=localhost:1521/xe;"
}

```


3. **Aplicar Migrations:**
No terminal, na pasta do projeto, execute:
```bash
dotnet ef database update

```


Isso criará as tabelas e executará as Triggers e Sequences necessárias.
4. **Executar:**
```bash
dotnet run --project .\SistemaVeterinario.API\SistemaVeterinario.API.csproj

```


Acesse: `http://localhost:5262/swagger`

## Como Executar os Testes

O projeto possui uma suíte organizada de testes unitários (com xUnit e Moq) e de integração (utilizando `WebApplicationFactory`). Para rodar todos os testes da solução, execute o comando na raiz do repositório:

```bash
dotnet test

```

## Monitoramento e Health Checks

A API dispõe de endpoints dedicados para verificação de saúde e disponibilidade da aplicação e de sua conectividade com o banco de dados:

* **Endpoints de Health Check**: Utilize os caminhos configurados na API (`/health/live` e `/health/ready`) para checar se o serviço e o Oracle estão operacionais.
* **OpenTelemetry & Logging**: O sistema coleta métricas de desempenho (tempo de resposta, taxa de erros, rastreamento de requisições entre camadas) e logs estruturados exibidos diretamente no console.

---

## Autenticação e Segurança (JWT)

Os endpoints de gerenciamento da API são protegidos por tokens **JWT (JSON Web Token)**. Para acessar as rotas de Tutores, Pets e Veterinários, é necessário autenticar-se previamente.

### Como usar a Autenticação:

1. **Gerar o Token**: Acesse a documentação do Swagger e faça uma requisição `POST` no endpoint `/api/Auth/login` enviando o JSON de credenciais padrão:
```json
{
  "username": "admin",
  "password": "123456"
}

```


2. **Copiar o Token**: Na resposta da requisição de login, o sistema retornará um token JWT. Copie a string gerada.
3. **Autorizar no Swagger**:
* Clique no botão **Authorize** (ícone de cadeado) localizado no canto superior direito da página do Swagger.
* No campo de inserção, digite a palavra **`Bearer `** seguida de um espaço e cole o seu token copiado (ex: `Bearer eyJhbGciOi...`).
* Clique em **Authorize** e feche a janela.


4. Agora você terá acesso liberado para executar as operações protegidas da API.

---

## Paginação, Ordenação, Filtros e HATEOAS

As consultas de listagem (`GET`) de **Veterinários**, **Tutores** e **Pets** contam com suporte completo a:

* **Paginação**: Controlada via parâmetros de query string `pageNumber` e `pageSize`.
* **Ordenação Dinâmica**: Suporte ao parâmetro `sortBy` e `ascending` (true/false).
* **Filtros**: Filtros específicos por entidade e busca textual global utilizando o parâmetro `search`.
* **HATEOAS**: As respostas seguem o modelo paginado (`PagedResponse<T>`) contendo os itens, metadados de paginação e links dinâmicos (`self`, `next_page`, `prev_page`), além de links individuais de navegação em cada registro.

---

## Arquitetura do Projeto

O sistema foi construído seguindo uma Arquitetura em Camadas (Data, Models, Repositories, Services, DTOs e Controllers):

* **Persistência**: Utiliza EF Core com mapeamento fluido. As chaves primárias são geridas pelo banco de dados através de Triggers e Sequences, garantindo autonomia do SGBD.
* **API**: Desenvolvida com o padrão REST, com tratamento global de erros para assegurar retornos HTTP semânticos (200, 201, 204, 400, 401, 404).

## Documentação das Rotas

### Autenticação (`/api/Auth`)

* **POST `/login**`: Autentica o usuário e retorna o token JWT de acesso.

### Veterinário (`/api/Veterinario`) *(Protegido)*

* **GET**: Lista todos os veterinários (suporta paginação, ordenação, filtros por `especialidade` e busca por `search`).
* **POST**: Cadastra um novo veterinário.
* **GET `/{id}**`: Busca um veterinário específico.
* **PUT `/{id}**`: Atualiza um registro.
* **DELETE `/{id}**`: Remove um registro.

### Tutor (`/api/Tutor`) *(Protegido)*

* **GET**: Lista todos os tutores (suporta paginação, ordenação, filtros por `cidade` e busca por `search`).
* **POST**: Cadastra um novo tutor.
* **GET `/{id}**`: Busca um tutor por ID.
* **PUT `/{id}**`: Atualiza um registro de tutor.
* **DELETE `/{id}**`: Remove um registro de tutor.

### Pet (`/api/Pet`) *(Protegido)*

* **GET**: Lista todos os pets (suporta paginação, ordenação, filtros por `especie` e busca por `search`).
* **POST**: Cadastra um novo pet vinculado a um tutor.
* **GET `/{id}**`: Busca um pet específico por ID.
* **PUT `/{id}**`: Atualiza os dados do pet.
* **DELETE `/{id}**`: Remove um registro de pet.

---

## Exemplos de JSON para Teste

### Cadastro de Veterinário

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

### Cadastro de Tutor

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

### Cadastro de Pet

*(Certifique-se de usar um `idTutor` existente)*

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

```

```