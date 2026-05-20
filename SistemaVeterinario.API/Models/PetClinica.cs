using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVeterinario.API.Models
{
    [Table("TB_PET_CLINICA")]
    public class PetClinica
    {
        [Key]
        [Column("ID_PET_CLINICA")]
        public decimal IdPetClinica { get; set; }

        [Required]
        [Column("ID_CLINICA")]
        public decimal IdClinica { get; set; }

        [ForeignKey("IdClinica")]
        public Clinica Clinica { get; set; }

        [Required]
        [Column("ID_PET")]
        public decimal IdPet { get; set; }

        [ForeignKey("IdPet")]
        public Pet Pet { get; set; }

        [Required]
        [Column("DATA_VINCULO_PET_CLINICA")]
        public DateTime DataVinculoPetClinica { get; set; } = DateTime.Now;
    }
}