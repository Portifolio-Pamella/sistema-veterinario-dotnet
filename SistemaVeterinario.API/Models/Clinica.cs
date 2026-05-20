using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
    [Table("TB_CLINICA")]
    public class Clinica
    {
        [Key]
        [Column("ID_CLINICA")]
        public decimal IdClinica { get; set; }

        [Required]
        [Column("NOME_FANTASIA_CLINICA")]
        [StringLength(100)]
        public string NomeFantasiaClinica { get; set; }

        [Required]
        [Column("RAZAO_SOCIAL_CLINICA")]
        [StringLength(150)]
        public string RazaoSocialClinica { get; set; }

        [Required]
        [Column("CNPJ_CLINICA")]
        [StringLength(18)]
        public string CnpjClinica { get; set; }

        [Required]
        [Column("TELEFONE_CLINICA")]
        [StringLength(20)]
        public string TelefoneClinica { get; set; }

        [Required]
        [Column("EMAIL_CLINICA")]
        [StringLength(150)]
        public string EmailClinica { get; set; }

        [Required]
        [Column("CEP_CLINICA")]
        [StringLength(10)]
        public string CepClinica { get; set; }

        [Required]
        [Column("RUA_CLINICA")]
        [StringLength(150)]
        public string RuaClinica { get; set; }

        [Required]
        [Column("NUMERO_CLINICA")]
        [StringLength(20)]
        public string NumeroClinica { get; set; }

        [Column("COMPLEMENTO_CLINICA")]
        [StringLength(100)]
        public string ComplementoClinica { get; set; }

        [Required]
        [Column("BAIRRO_CLINICA")]
        [StringLength(100)]
        public string BairroClinica { get; set; }

        [Required]
        [Column("CIDADE_CLINICA")]
        [StringLength(100)]
        public string CidadeClinica { get; set; }

        [Required]
        [Column("ESTADO_CLINICA")]
        [StringLength(100)]
        public string EstadoClinica { get; set; }

        [Required]
        [Column("DATA_CADASTRO_CLINICA")]
        public DateTime DataCadastroClinica { get; set; } = DateTime.Now;
    }
}