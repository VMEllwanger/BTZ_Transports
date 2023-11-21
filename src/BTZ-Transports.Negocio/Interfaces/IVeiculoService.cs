using BTZ_Transports.Negocio.Models;

namespace BTZ_Transports.Negocio.Interfaces
{
    public interface IVeiculoService
    {
        Task Adicionar(Veiculo veiculo);
        Task<IEnumerable<Veiculo>> ObterVeiculos();
        Task<Veiculo> ObterPorId(Guid Id);
        Task Atualizar(Veiculo veiculo);
        Task Remover(Guid id);
    }
}
