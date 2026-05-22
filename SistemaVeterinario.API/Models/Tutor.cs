using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tutor
{
    [Key]
    [Column("ID_TUTOR")]
    public decimal IdTutor { get; set; }

    [Required]
    [Column("NOME_TUTOR")]
    [StringLength(100)]
    public string NomeTutor { get; set; } = string.Empty;

    [Required]
    [Column("CPF_TUTOR")]
    [StringLength(14)]
    public string CpfTutor { get; set; } = string.Empty;

    [Required]
    [Column("TELEFONE_TUTOR")]
    [StringLength(20)]
    public string TelefoneTutor { get; set; } = string.Empty;

    [Required]
    [Column("EMAIL_TUTOR")]
    [StringLength(150)]
    public string EmailTutor { get; set; } = string.Empty;

    [Required]
    [Column("CEP_TUTOR")]
    [StringLength(10)]
    public string CepTutor { get; set; } = string.Empty;

    [Required]
    [Column("RUA_TUTOR")]
    [StringLength(150)]
    public string RuaTutor { get; set; } = string.Empty;

    [Required]
    [Column("NUMERO_TUTOR")]
    [StringLength(20)]
    public string NumeroTutor { get; set; } = string.Empty;

    [Column("COMPLEMENTO_TUTOR")]
    [StringLength(100)]
    public string ComplementoTutor { get; set; } = string.Empty;

    [Required]
    [Column("BAIRRO_TUTOR")]
    [StringLength(100)]
    public string BairroTutor { get; set; } = string.Empty;

    [Required]
    [Column("CIDADE_TUTOR")]
    [StringLength(100)]
    public string CidadeTutor { get; set; } = string.Empty;

    [Required]
    [Column("ESTADO_TUTOR")]
    [StringLength(100)]
    public string EstadoTutor { get; set; } = string.Empty;

    [Required]
    [Column("DATA_CADASTRO_TUTOR")]
    public DateTime DataCadastroTutor { get; set; } = DateTime.Now;
}