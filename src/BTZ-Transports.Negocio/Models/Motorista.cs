using BTZ_Transports.Negocio.Enums;

namespace BTZ_Transports.Negocio.Models
{
    public class Motorista : Entity
    { 
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string NumeroCNH { get; set; }
        public string CategoriaCNH { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public StatusMotorista Status { get; set; }

        /* EF Relations */
        public IEnumerable<RegistroAbastecimento> RegistroAbastecimentos { get; set; }
    }
}
