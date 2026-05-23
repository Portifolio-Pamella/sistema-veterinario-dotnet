Configuração do Banco de DadosEste projeto utiliza Oracle Database 21c. Para que a API funcione corretamente, siga os passos abaixo:1. Execução do DDL: Execute o script dll_oracle.ddl fornecido na pasta /database para criar as tabelas e relacionamentos.
2. Criação de Sequences: O Entity Framework Core utiliza sequences do Oracle para gerenciar os IDs das entidades. Execute os seguintes comandos no seu SQL Developer ou console SQL:  SQLCREATE SEQUENCE SEQ_TB_VETERINARIO START WITH 1 INCREMENT BY 1 NOCACHE;
CREATE SEQUENCE SEQ_TB_PET START WITH 1 INCREMENT BY 1 NOCACHE;
CREATE SEQUENCE SEQ_TB_CLINICA START WITH 1 INCREMENT BY 1 NOCACHE;
CREATE SEQUENCE SEQ_TB_TUTOR START WITH 1 INCREMENT BY 1 NOCACHE;
CREATE SEQUENCE SEQ_TB_VETERINARIO START WITH 1 INCREMENT BY 1 NOCACHE
Conexão: Certifique-se de que a ConnectionString no seu appsettings.json está apontando para o seu banco com as permissões de leitura/escrita necessárias.

-- Garante que a sequência exista
CREATE SEQUENCE SEQ_TB_VETERINARIO START WITH 1 INCREMENT BY 1 NOCACHE;

-- Ajusta a sequência para o próximo valor correto
ALTER SEQUENCE SEQ_TB_VETERINARIO RESTART START WITH 1;


post tutor
[
  {
    "idTutor": 1,
    "nomeTutor": "Tutor",
    "cpfTutor": "99988877766",
    "telefoneTutor": "11999999999",
    "emailTutor": "fulano.teste@email.com",
    "cepTutor": "01001-000",
    "ruaTutor": "Avenida Exemplo",
    "numeroTutor": "123",
    "complementoTutor": "Bloco A",
    "bairroTutor": "Centro",
    "cidadeTutor": "São Paulo",
    "estadoTutor": "SP",
    "dataCadastroTutor": "2026-05-22T04:19:28"
  },
  {
    "idTutor": 0,
    "nomeTutor": "Carlos Eduardo Oliveira",
    "cpfTutor": "12345678901",
    "telefoneTutor": "11987654321",
    "emailTutor": "carlos.oliveira@email.com",
    "cepTutor": "01001000",
    "ruaTutor": "Avenida Paulista",
    "numeroTutor": "1000",
    "complementoTutor": "Apto 42",
    "bairroTutor": "Bela Vista",
    "cidadeTutor": "São Paulo",
    "estadoTutor": "SP",
    "dataCadastroTutor": "2026-05-22T03:59:43"
  }
]
post pet
[
  {
    "idPet": 0,
    "idTutor": 0,
    "tutor": {
      "idTutor": 0,
      "nomeTutor": "Carlos Eduardo Oliveira",
      "cpfTutor": "12345678901",
      "telefoneTutor": "11987654321",
      "emailTutor": "carlos.oliveira@email.com",
      "cepTutor": "01001000",
      "ruaTutor": "Avenida Paulista",
      "numeroTutor": "1000",
      "complementoTutor": "Apto 42",
      "bairroTutor": "Bela Vista",
      "cidadeTutor": "São Paulo",
      "estadoTutor": "SP",
      "dataCadastroTutor": "2026-05-22T03:59:43"
    },
    "nomePet": "Olaf",
    "especiePet": "string",
    "racaPet": "string",
    "sexoPet": "string",
    "dataNascimentoPet": "2026-05-22T04:33:02",
    "pesoPet": 0,
    "corPet": "string",
    "dataCadastroPet": "2026-05-22T04:33:02"
  }
]

post clinica 
{
  "idClinica": 0,
  "nomeFantasiaClinica": "Pet Saúde Total",
  "razaoSocialClinica": "Pet Saúde Total Serviços Veterinários Ltda",
  "cnpjClinica": "12345678000100",
  "telefoneClinica": "1144445555",
  "emailClinica": "contato@petsaudetotal.com",
  "cepClinica": "02001000",
  "ruaClinica": "Rua dos Animais",
  "numeroClinica": "99",
  "complementoClinica": "Galpão A",
  "bairroClinica": "Vila Nova",
  "cidadeClinica": "São Paulo",
  "estadoClinica": "SP",
  "dataCadastroClinica": "2026-05-22T04:51:47.606Z"
}