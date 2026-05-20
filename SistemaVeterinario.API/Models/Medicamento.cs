using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
    [Table("TB_MEDICAMENTO")]
    public class Medicamento
    {
        [Key]
        [Column("ID_MEDICAMENTO")]
        public decimal IdMedicamento { get; set; }

        [Required]
        [Column("ID_PET")]
        public decimal IdPet { get; set; }

        [ForeignKey("IdPet")]
        public Pet Pet { get; set; }

        [Required]
        [Column("NOME_MEDICAMENTO")]
        [StringLength(150)]
        public string NomeMedicamento { get; set; }

        [Column("DOSAGEM_MEDICAMENTO")]
        [StringLength(100)]
        public string DosagemMedicamento { get; set; }

        [Column("FREQUENCIA_MEDICAMENTO")]
        [StringLength(100)]
        public string FrequenciaMedicamento { get; set; }

        [Required]
        [Column("DATA_INICIO_MEDICAMENTO")]
        public DateTime DataInicioMedicamento { get; set; }

        [Column("DATA_FIM_MEDICAMENTO")]
        public DateTime? DataFimMedicamento { get; set; }

        [Column("STATUS_MEDICAMENTO")]
        [StringLength(20)]
        public string StatusMedicamento { get; set; } // ATIVO / FINALIZADO

        [Column("OBSERVACAO_MEDICAMENTO")]
        [StringLength(300)]
        public string ObservacaoMedicamento { get; set; }
    }
}