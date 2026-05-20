# Sistema Veterinário API

API RESTful desenvolvida para o gerenciamento de clínicas veterinárias, tutores, pets e consultas.

## Tecnologias
- .NET 10.0
- Entity Framework Core
- Oracle Database
- Swagger (OpenAPI)

## 🛠️ Como executar
1. Clone o repositório.
2. Certifique-se de ter o .NET 10.0+ instalado.
3. No arquivo `appsettings.json`, configure a string de conexão do seu banco Oracle:
   ```json
   "ConnectionStrings": {
     "OracleConnection": "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=SEU_RM;Password=SUA_SENHA;"
   }