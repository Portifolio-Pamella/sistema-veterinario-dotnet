using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaVeterinario.API.Models;

public class Veterinario
{
    [Key]
    [Column("ID_VETERINARIO")]
    public decimal IdVeterinario { get; set; }

    [Required]
    [Column("ID_CLINICA")]
    public decimal IdClinica { get; set; }

    [ForeignKey("IdClinica")]
    public Clinica Clinica { get; set; } = null!;

    [Required]
    [Column("NOME_VETERINARIO")]
    [StringLength(150)]
    public string NomeVeterinario { get; set; } = string.Empty;

    [Required]
    [Column("CRM_VETERINARIO")]
    [StringLength(30)]
    public string CrmVeterinario { get; set; } = string.Empty;

    [Column("ESPECIALIDADE_VETERINARIO")]
    [StringLength(100)]
    public string EspecialidadeVeterinario { get; set; } = string.Empty;

    [Required]
    [Column("TELEFONE_VETERINARIO")]
    [StringLength(20)]
    public string TelefoneVeterinario { get; set; } = string.Empty;

    [Column("EMAIL_VETERINARIO")]
    [StringLength(150)]
    public string EmailVeterinario { get; set; } = string.Empty;

    [Required]
    [Column("STATUS_VETERINARIO")]
    [StringLength(20)]
    public string StatusVeterinario { get; set; } = "ATIVO";

    [Required]
    [Column("DATA_CADASTRO_VETERINARIO")]
    public DateTime DataCadastroVeterinario { get; set; } = DateTime.Now;
}