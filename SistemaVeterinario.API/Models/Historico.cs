using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
    [Table("TB_HISTORICO")]
    public class Historico
    {
        [Key]
        [Column("ID_HISTORICO")]
        public decimal IdHistorico { get; set; }

        [Required]
        [Column("ID_PET")]
        public decimal IdPet { get; set; }

        [ForeignKey("IdPet")]
        public Pet Pet { get; set; }

        [Column("DESCRICAO_HISTORICO")]
        [StringLength(500)]
        public string DescricaoHistorico { get; set; }

        [Required]
        [Column("DATA_REGISTRO_HISTORICO")]
        public DateTime DataRegistroHistorico { get; set; }

        [Column("TIPO_EVENTO")]
        [StringLength(100)]
        public string TipoEvento { get; set; } // ACOMPANHAMENTO, CONSULTA, MEDICAMENTO, etc.
    }
}