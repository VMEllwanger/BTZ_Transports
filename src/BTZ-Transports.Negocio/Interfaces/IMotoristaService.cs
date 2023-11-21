using BTZ_Transports.Negocio.Models;

namespace BTZ_Transports.Negocio.Interfaces
{
    public interface IMotoristaService
    {
        Task Adicionar(Motorista motorista);
        Task<IEnumerable<Motorista>> ObterMotoristas();
        Task<Motorista> ObterPorId(Guid Id);
        Task<Motorista> ObterPorCPF(string CPF);
        Task Atualizar(Motorista motorista);
        Task Remover(Guid id);
    }
}