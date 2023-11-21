using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BTZ_Transports.API.ViewModels
{
    public class VeiculoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }


        [DisplayName("Combustivel")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int CombustivelId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Fabricante { get; set; }

        [DisplayName("Ano De Fabricação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int AnoDeFabricacao { get; set; }

        [DisplayName("Capacidade Maxima Do Tanque")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public double CapacidadeMaximaDoTanque { get; set; }

     
        public string? Observacao { get; set; } = "";
         
    }
}
