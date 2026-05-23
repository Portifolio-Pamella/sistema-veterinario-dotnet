using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
    [Table("TB_VETERINARIO")]
    public class Veterinario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Essencial
        [Column("ID_VETERINARIO")]
        public decimal IdVeterinario { get; set; }

        [Required]
        [Column("NOME_VETERINARIO")]
        public string NomeVeterinario { get; set; } = string.Empty;

        [Required]
        [Column("CRM_VETERINARIO")]
        public string CrmVeterinario { get; set; } = string.Empty;

        [Required]
        [Column("ESPECIALIDADE_VETERINARIO")]
        public string EspecialidadeVeterinario { get; set; } = string.Empty;

        [Column("TELEFONE_VETERINARIO")]
        public string? TelefoneVeterinario { get; set; }

        [Column("EMAIL_VETERINARIO")]
        public string? EmailVeterinario { get; set; }

        [Column("STATUS_VETERINARIO")]
        public string? StatusVeterinario { get; set; }

        [Required]
        [Column("DATA_CADASTRO_VETERINARIO")]
        public DateTime DataCadastroVeterinario { get; set; } = DateTime.Now;
    }
}