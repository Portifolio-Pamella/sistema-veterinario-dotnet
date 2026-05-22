using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
    [Table("TB_ACOMPANHAMENTO")]
    public class Acompanhamento
    {
        [Key]
        [Column("ID_ACOMPANHAMENTO")]
        public decimal IdAcompanhamento { get; set; }

        [Required]
        [Column("ID_PET")]
        public decimal IdPet { get; set; }

        [ForeignKey("IdPet")]
        public Pet Pet { get; set; } = null!; // null! avisa ao EF que não será nulo

        [Required]
        [Column("ID_VETERINARIO")]
        public decimal IdVeterinario { get; set; }

        [ForeignKey("IdVeterinario")]
        public Veterinario Veterinario { get; set; } = null!;

        [Column("NOME_ACOMPANHAMENTO")]
        [StringLength(150)]
        public string NomeAcompanhamento { get; set; } = string.Empty;

        [Column("DESCRICAO_ACOMPANHAMENTO")]
        [StringLength(500)]
        public string DescricaoAcompanhamento { get; set; } = string.Empty;

        [Required]
        [Column("DATA_INICIO_ACOMPANHAMENTO")]
        public DateTime DataInicioAcompanhamento { get; set; }

        [Column("DATA_FIM_ACOMPANHAMENTO")]
        public DateTime? DataFimAcompanhamento { get; set; }

        [Required]
        [Column("STATUS_ACOMPANHAMENTO")]
        [StringLength(20)]
        public string StatusAcompanhamento { get; set; } = "ATIVO"; // Valor padrão útil
    }
}