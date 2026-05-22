using SistemaVeterinario.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Medicamento
{
    [Key]
    [Column("ID_MEDICAMENTO")]
    public decimal IdMedicamento { get; set; }

    [Required]
    [Column("ID_PET")]
    public decimal IdPet { get; set; }

    [ForeignKey("IdPet")]
    public Pet Pet { get; set; } = null!;

    [Required]
    [Column("NOME_MEDICAMENTO")]
    [StringLength(150)]
    public string NomeMedicamento { get; set; } = string.Empty;

    [Column("DOSAGEM_MEDICAMENTO")]
    [StringLength(100)]
    public string DosagemMedicamento { get; set; } = string.Empty;

    [Column("FREQUENCIA_MEDICAMENTO")]
    [StringLength(100)]
    public string FrequenciaMedicamento { get; set; } = string.Empty;

    [Required]
    [Column("DATA_INICIO_MEDICAMENTO")]
    public DateTime DataInicioMedicamento { get; set; }

    [Column("DATA_FIM_MEDICAMENTO")]
    public DateTime? DataFimMedicamento { get; set; }

    [Column("STATUS_MEDICAMENTO")]
    [StringLength(20)]
    public string StatusMedicamento { get; set; } = "ATIVO";

    [Column("OBSERVACAO_MEDICAMENTO")]
    [StringLength(300)]
    public string ObservacaoMedicamento { get; set; } = string.Empty;
}