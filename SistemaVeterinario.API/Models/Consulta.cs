using SistemaVeterinario.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Consulta
{
    [Key]
    [Column("ID_CONSULTA")]
    public decimal IdConsulta { get; set; }

    [Required]
    [Column("ID_CLINICA")]
    public decimal IdClinica { get; set; }

    [ForeignKey("IdClinica")]
    public Clinica Clinica { get; set; } = null!;

    [Required]
    [Column("ID_VETERINARIO")]
    public decimal IdVeterinario { get; set; }

    [ForeignKey("IdVeterinario")]
    public Veterinario Veterinario { get; set; } = null!;

    [Required]
    [Column("ID_PET")]
    public decimal IdPet { get; set; }

    [ForeignKey("IdPet")]
    public Pet Pet { get; set; } = null!;

    [Required]
    [Column("DATA_CONSULTA")]
    public DateTime DataConsulta { get; set; }

    [Required]
    [Column("MOTIVO_CONSULTA")]
    [StringLength(200)]
    public string MotivoConsulta { get; set; } = string.Empty;

    [Required]
    [Column("SINTOMAS_CONSULTA")]
    [StringLength(300)]
    public string SintomasConsulta { get; set; } = string.Empty;

    [Required]
    [Column("DIAGNOSTICO_CONSULTA")]
    [StringLength(300)]
    public string DiagnosticoConsulta { get; set; } = string.Empty;

    [Column("RETORNO_CONSULTA")]
    public DateTime? RetornoConsulta { get; set; }

    [Column("STATUS_CONSULTA")]
    [StringLength(50)]
    public string StatusConsulta { get; set; } = "AGENDADA";

    [Column("OBSERVACOES_CONSULTA")]
    [StringLength(300)]
    public string ObservacoesConsulta { get; set; } = string.Empty;
}