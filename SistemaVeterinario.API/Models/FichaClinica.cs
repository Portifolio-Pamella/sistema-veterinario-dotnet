using SistemaVeterinario.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class FichaClinica
{
    [Key]
    [Column("ID_FICHA_CLINICA")]
    public decimal IdFichaClinica { get; set; }

    [Required]
    [Column("ID_PET")]
    public decimal IdPet { get; set; }

    [ForeignKey("IdPet")]
    public Pet Pet { get; set; } = null!;

    [Column("TIPO_SANGUINEO")]
    [StringLength(10)]
    public string TipoSanguineo { get; set; } = string.Empty;

    [Column("ALERGIAS_FICHA_CLINICA")]
    [StringLength(300)]
    public string AlergiasFichaClinica { get; set; } = string.Empty;

    [Column("DOENCAS_CRONICAS_FICHA_CLINIC")]
    [StringLength(300)]
    public string DoencasCronicasFichaClinica { get; set; } = string.Empty;

    [Column("OBSERVACOES_FICHA_CLINICA")]
    [StringLength(300)]
    public string ObservacoesFichaClinica { get; set; } = string.Empty;

    [Column("DATA_CRIACAO_FICHA_CLINICA")]
    public DateTime? DataCriacaoFichaClinica { get; set; } = DateTime.Now;
}