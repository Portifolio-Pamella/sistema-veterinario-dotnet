using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaVeterinario.API.Models; // ADICIONE ESTA LINHA

namespace SistemaVeterinario.API.Models
{
    public class PetClinica
    {
        [Key]
        [Column("ID_PET_CLINICA")]
        public decimal IdPetClinica { get; set; }

        [Required]
        [Column("ID_CLINICA")]
        public decimal IdClinica { get; set; }

        [ForeignKey("IdClinica")]
        public Clinica Clinica { get; set; } = null!; // Agora o compilador reconhece 'Clinica'

        [Required]
        [Column("ID_PET")]
        public decimal IdPet { get; set; }

        [ForeignKey("IdPet")]
        public Pet Pet { get; set; } = null!;

        [Required]
        [Column("DATA_VINCULO_PET_CLINICA")]
        public DateTime DataVinculoPetClinica { get; set; } = DateTime.Now;
    }
}