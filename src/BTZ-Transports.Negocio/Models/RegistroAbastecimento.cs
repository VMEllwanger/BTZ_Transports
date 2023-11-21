namespace BTZ_Transports.Negocio.Models
{
    public class RegistroAbastecimento : Entity
    {
        public int CombustivelId { get; set; }
        public Guid MotoristaId { get; set; }
        public Guid VeiculoId { get; set; }
        public decimal ValorTotal { get;set; }
        public DateTime Data { get; set; }
        public decimal QuantidadeAbastecida { get; set; }

        /* EF Relations */
        public Combustivel Combustivel { get; set; }
        public Motorista Motorista { get; set; }
        public Veiculo Veiculo { get; set; }
                 
        public  void somarValorTotal(decimal valorPorLitro)
        {
            ValorTotal = QuantidadeAbastecida * valorPorLitro;
        }
    }
}
