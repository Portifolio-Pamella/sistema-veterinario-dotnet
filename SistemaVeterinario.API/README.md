# Sistema Veterinário API

## Sobre o Projeto

Este projeto é uma API RESTful desenvolvida em **ASP.NET Core** para o gerenciamento de uma clínica veterinária. O sistema permite o cadastro e controle de **Tutores**, seus **Pets** e os **Veterinários** responsáveis, garantindo a organização de dados e a integridade das informações através de um banco de dados **Oracle**.

### Tecnologias Utilizadas

* **ASP.NET Core (Minimal/Controllers):** Estrutura da API.
* **Entity Framework Core:** ORM para interação com o banco de dados.
* **Oracle Database:** Banco de dados relacional.
* **Swagger (OpenAPI):** Documentação e interface de testes.
* **Migrations:** Controle de versão do esquema do banco de dados.

---

## Integrantes

* Lucas Matsubara Reis | RM565020
* João Pedro Camilo | RM562005
* Pamella Christiny | RM565206

---

## Como Executar

1. **Clone o repositório:** `git clone git@github.com:Portifolio-Pamella/sistema-veterinario-dotnet.git`
2. **Configuração:** No arquivo `appsettings.json`, atualize a `OracleConnection` com as credenciais do seu banco de dados.
3. **Banco de Dados:** Execute o comando abaixo no terminal (na pasta do projeto) para criar as tabelas e aplicar as Triggers/Sequences:
```bash
dotnet ef database update

```


4. **Execução:** Rode o projeto: `dotnet run`.
5. **Acesso:** Abra o navegador em: [http://localhost:5262/](https://www.google.com/search?q=http://localhost:5262/)

---

## Documentação das Rotas

### Veterinário (`/api/Veterinario`)

* `GET /api/Veterinario`: Lista todos os veterinários.
* `GET /api/Veterinario/{id}`: Busca um veterinário específico.
* `GET /api/Veterinario/especialidade/{especialidade}`: Filtra veterinários por especialidade.
* `POST /api/Veterinario`: Cadastra um veterinário.
* `PUT /api/Veterinario/{id}`: Atualiza dados de um veterinário.
* `DELETE /api/Veterinario/{id}`: Remove um veterinário.

### Tutor (`/api/Tutor`)

* `GET /api/Tutor`: Lista todos os tutores.
* `GET /api/Tutor/{id}`: Busca um tutor pelo ID.
* `GET /api/Tutor/cidade/{cidade}`: Filtra tutores por cidade.
* `POST /api/Tutor`: Cadastra um novo tutor.

### Pet (`/api/Pet`)

* `GET /api/Pet`: Lista todos os pets.
* `GET /api/Pet/especie/{especie}`: Filtra pets por espécie.
* `POST /api/Pet`: Cadastra um pet. **Nota:** O ID do Tutor deve existir no banco.
* `PUT /api/Pet/{id}`: Atualiza um pet.

---

## Exemplos para Teste (JSON)

### Cadastrar Veterinário

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

### Cadastrar Tutor

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

### Cadastrar Pet

*(Certifique-se de que o `idTutor` abaixo corresponda ao ID gerado após o cadastro do tutor acima)*

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