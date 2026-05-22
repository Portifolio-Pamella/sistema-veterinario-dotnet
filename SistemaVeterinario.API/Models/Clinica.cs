using System.ComponentModel.DataAnnotations;

namespace SistemaVeterinario.API.Models
{
    public class Clinica
    {
        [Key]
        public decimal IdClinica { get; set; }

        [Required, StringLength(100)]
        public string NomeFantasiaClinica { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string RazaoSocialClinica { get; set; } = string.Empty;

        [Required, StringLength(18)]
        public string CnpjClinica { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string TelefoneClinica { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string EmailClinica { get; set; } = string.Empty;

        [Required, StringLength(10)]
        public string CepClinica { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string RuaClinica { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string NumeroClinica { get; set; } = string.Empty;

        [StringLength(100)]
        public string ComplementoClinica { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string BairroClinica { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string CidadeClinica { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string EstadoClinica { get; set; } = string.Empty;

        [Required]
        public DateTime DataCadastroClinica { get; set; } = DateTime.Now;
    }
}