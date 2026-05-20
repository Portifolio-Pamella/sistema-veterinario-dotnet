using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
    [Table("TB_CONSULTA")]
    public class Consulta
    {
        [Key]
        [Column("ID_CONSULTA")]
        public decimal IdConsulta { get; set; }

        [Required]
        [Column("ID_CLINICA")]
        public decimal IdClinica { get; set; }

        [ForeignKey("IdClinica")]
        public Clinica Clinica { get; set; }

        [Required]
        [Column("ID_VETERINARIO")]
        public decimal IdVeterinario { get; set; }

        [ForeignKey("IdVeterinario")]
        public Veterinario Veterinario { get; set; }

        [Required]
        [Column("ID_PET")]
        public decimal IdPet { get; set; }

        [ForeignKey("IdPet")]
        public Pet Pet { get; set; }

        [Required]
        [Column("DATA_CONSULTA")]
        public DateTime DataConsulta { get; set; }

        [Required]
        [Column("MOTIVO_CONSULTA")]
        [StringLength(200)]
        public string MotivoConsulta { get; set; }

        [Required]
        [Column("SINTOMAS_CONSULTA")]
        [StringLength(300)]
        public string SintomasConsulta { get; set; }

        [Required]
        [Column("DIAGNOSTICO_CONSULTA")]
        [StringLength(300)]
        public string DiagnosticoConsulta { get; set; }

        [Column("RETORNO_CONSULTA")]
        public DateTime? RetornoConsulta { get; set; }

        [Column("STATUS_CONSULTA")]
        [StringLength(50)]
        public string StatusConsulta { get; set; } = "AGENDADA";

        [Column("OBSERVACOES_CONSULTA")]
        [StringLength(300)]
        public string ObservacoesConsulta { get; set; }
    }
}