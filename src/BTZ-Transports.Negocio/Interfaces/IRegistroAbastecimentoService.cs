using BTZ_Transports.Negocio.Models;

namespace BTZ_Transports.Negocio.Interfaces
{
    public interface IRegistroAbastecimentoService
    {
        Task Adicionar(RegistroAbastecimento registroAbastecimento);
        Task<IEnumerable<RegistroAbastecimento>> ObterRegistroAbastecimentos();
        Task<RegistroAbastecimento> ObterPorId(Guid Id);
        Task<IEnumerable<RegistroAbastecimento>> ObterPorVeiculoId(Guid VeiculoId);
        Task<IEnumerable<RegistroAbastecimento>> ObterPorMotoristaId(Guid MotoristaId);
    }
}
