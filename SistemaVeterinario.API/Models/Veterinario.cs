using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
    [Table("TB_VETERINARIO")]
    public class Veterinario
    {
        [Key]
        [Column("ID_VETERINARIO")]
        public decimal IdVeterinario { get; set; }

        [Required]
        [Column("ID_CLINICA")]
        public decimal IdClinica { get; set; }

        [ForeignKey("IdClinica")]
        public Clinica Clinica { get; set; }

        [Required]
        [Column("NOME_VETERINARIO")]
        [StringLength(150)]
        public string NomeVeterinario { get; set; }

        [Required]
        [Column("CRM_VETERINARIO")]
        [StringLength(30)]
        public string CrmVeterinario { get; set; }

        [Column("ESPECIALIDADE_VETERINARIO")]
        [StringLength(100)]
        public string EspecialidadeVeterinario { get; set; }

        [Required]
        [Column("TELEFONE_VETERINARIO")]
        [StringLength(20)]
        public string TelefoneVeterinario { get; set; }

        [Column("EMAIL_VETERINARIO")]
        [StringLength(150)]
        public string EmailVeterinario { get; set; }

        [Required]
        [Column("STATUS_VETERINARIO")]
        [StringLength(20)]
        public string StatusVeterinario { get; set; } // ATIVO / INATIVO

        [Required]
        [Column("DATA_CADASTRO_VETERINARIO")]
        public DateTime DataCadastroVeterinario { get; set; } = DateTime.Now;
    }
}