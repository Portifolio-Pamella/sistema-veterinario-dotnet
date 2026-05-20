using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
    [Table("TB_PET")]
    public class Pet
    {
        [Key]
        [Column("ID_PET")]
        public decimal IdPet { get; set; }

        [Required]
        [Column("ID_TUTOR")]
        public decimal IdTutor { get; set; }

        [ForeignKey("IdTutor")]
        public Tutor Tutor { get; set; }

        [Required]
        [Column("NOME_PET")]
        [StringLength(100)]
        public string NomePet { get; set; }

        [Required]
        [Column("ESPECIE_PET")]
        [StringLength(50)]
        public string EspeciePet { get; set; }

        [Required]
        [Column("RACA_PET")]
        [StringLength(80)]
        public string RacaPet { get; set; }

        [Required]
        [Column("SEXO_PET")]
        [StringLength(10)]
        public string SexoPet { get; set; }

        [Required]
        [Column("DATA_NASCIMENTO_PET")]
        public DateTime DataNascimentoPet { get; set; }

        [Required]
        [Column("PESO_PET")]
        public decimal PesoPet { get; set; }

        [Required]
        [Column("COR_PET")]
        [StringLength(50)]
        public string CorPet { get; set; }

        [Required]
        [Column("DATA_CADASTRO_PET")]
        public DateTime DataCadastroPet { get; set; } = DateTime.Now;
    }
}