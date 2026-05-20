using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
    [Table("TB_TUTOR")]
    public class Tutor
    {
        [Key]
        [Column("ID_TUTOR")]
        public decimal IdTutor { get; set; }

        [Required]
        [Column("NOME_TUTOR")]
        [StringLength(100)]
        public string NomeTutor { get; set; }

        [Required]
        [Column("CPF_TUTOR")]
        [StringLength(14)]
        public string CpfTutor { get; set; }

        [Required]
        [Column("TELEFONE_TUTOR")]
        [StringLength(20)]
        public string TelefoneTutor { get; set; }

        [Required]
        [Column("EMAIL_TUTOR")]
        [StringLength(150)]
        public string EmailTutor { get; set; }

        [Required]
        [Column("CEP_TUTOR")]
        [StringLength(10)]
        public string CepTutor { get; set; }

        [Required]
        [Column("RUA_TUTOR")]
        [StringLength(150)]
        public string RuaTutor { get; set; }

        [Required]
        [Column("NUMERO_TUTOR")]
        [StringLength(20)]
        public string NumeroTutor { get; set; }

        [Column("COMPLEMENTO_TUTOR")]
        [StringLength(100)]
        public string ComplementoTutor { get; set; }

        [Required]
        [Column("BAIRRO_TUTOR")]
        [StringLength(100)]
        public string BairroTutor { get; set; }

        [Required]
        [Column("CIDADE_TUTOR")]
        [StringLength(100)]
        public string CidadeTutor { get; set; }

        [Required]
        [Column("ESTADO_TUTOR")]
        [StringLength(100)]
        public string EstadoTutor { get; set; }

        [Required]
        [Column("DATA_CADASTRO_TUTOR")]
        public DateTime DataCadastroTutor { get; set; } = DateTime.Now;
    }
}