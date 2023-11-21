using BTZ_Transports.Negocio.Models;

namespace BTZ_Transports.Negocio.Interfaces
{
    public interface ICombustivelRepository : IDisposable
    {
        Task<Combustivel> ObterPorId(int id);
        Task<List<Combustivel>> ObterTodos();
    }
}
