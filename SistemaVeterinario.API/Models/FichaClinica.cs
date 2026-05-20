using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
	[Table("TB_FICHA_CLINICA")]
	public class FichaClinica
	{
		[Key]
		[Column("ID_FICHA_CLINICA")]
		public decimal IdFichaClinica { get; set; }

		[Required]
		[Column("ID_PET")]
		public decimal IdPet { get; set; }

		[ForeignKey("IdPet")]
		public Pet Pet { get; set; }

		[Column("TIPO_SANGUINEO")]
		[StringLength(10)]
		public string TipoSanguineo { get; set; }

		[Column("ALERGIAS_FICHA_CLINICA")]
		[StringLength(300)]
		public string AlergiasFichaClinica { get; set; }

		[Column("DOENCAS_CRONICAS_FICHA_CLINIC")]
		[StringLength(300)]
		public string DoencasCronicasFichaClinica { get; set; }

		[Column("OBSERVACOES_FICHA_CLINICA")]
		[StringLength(300)]
		public string ObservacoesFichaClinica { get; set; }

		[Column("DATA_CRIACAO_FICHA_CLINICA")]
		public DateTime? DataCriacaoFichaClinica { get; set; } = DateTime.Now;
	}
}