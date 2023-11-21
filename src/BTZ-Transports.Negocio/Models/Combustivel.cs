namespace BTZ_Transports.Negocio.Models
{
    public class Combustivel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        /* EF Relations */
        public IEnumerable<RegistroAbastecimento> RegistroAbastecimentos { get; set; }
        public IEnumerable<Veiculo> Veiculos { get; set; }
    }
}
