using BTZ_Transports.Negocio.Enums;
using System.ComponentModel.DataAnnotations;

namespace BTZ_Transports.API.ViewModels
{
    public class MotoristaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NumeroCNH { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CategoriaCNH { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataDeNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Status { get; set; }
    }
}
