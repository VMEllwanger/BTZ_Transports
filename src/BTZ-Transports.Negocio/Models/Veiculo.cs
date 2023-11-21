namespace BTZ_Transports.Negocio.Models
{
    public class Veiculo : Entity
    {
        public string Nome { get; set; }
        public int CombustivelId { get; set; }
        public string Fabricante { get; set; }
        public int AnoDeFabricacao { get; set; }
        public decimal CapacidadeMaximaDoTanque { get; set; }
        public string? Observacao { get; set; }

        /* EF Relations */
        public Combustivel Combustivel { get; set; }
        public IEnumerable<RegistroAbastecimento> RegistroAbastecimentos { get; set; }
    }
}
