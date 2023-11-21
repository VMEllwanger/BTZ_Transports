using System.ComponentModel.DataAnnotations;

namespace BTZ_Transports.API.ViewModels
{
    public class RegistroAbastecimentoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int CombustivelId { get; set; } 

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid MotoristaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid VeiculoId { get; set; }

         
        public decimal? ValorTotal { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal QuantidadeAbastecida { get; set; }
    }
}
