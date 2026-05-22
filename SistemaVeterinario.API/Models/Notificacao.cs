using SistemaVeterinario.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Notificacao
{
    [Key]
    [Column("ID_NOTIFICACAO")]
    public decimal IdNotificacao { get; set; }

    [Required]
    [Column("ID_TUTOR")]
    public decimal IdTutor { get; set; }

    [ForeignKey("IdTutor")]
    public Tutor Tutor { get; set; } = null!;

    [Required]
    [Column("ID_PET")]
    public decimal IdPet { get; set; }

    [ForeignKey("IdPet")]
    public Pet Pet { get; set; } = null!;

    [Column("TITULO_NOTIFICACAO")]
    [StringLength(150)]
    public string TituloNotificacao { get; set; } = string.Empty;

    [Required]
    [Column("MENSAGEM_NOTIFICACAO")]
    [StringLength(300)]
    public string MensagemNotificacao { get; set; } = string.Empty;

    [Column("TIPO_NOTIFICACAO")]
    [StringLength(50)]
    public string TipoNotificacao { get; set; } = string.Empty;

    [Required]
    [Column("DATA_ENVIO_NOTIFICACAO")]
    public DateTime DataEnvioNotificacao { get; set; }

    [Column("STATUS_ENVIO_NOTIFICACAO")]
    [StringLength(20)]
    public string StatusEnvioNotificacao { get; set; } = "PENDENTE";
}