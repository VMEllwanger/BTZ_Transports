using BTZ_Transports.Negocio.Models;

namespace BTZ_Transports.Negocio.Interfaces
{
    public interface IMotoristaRepository : IRepository<Motorista> 
    {
        Task<Motorista> ObterMotoristaPorCpf(string CPF);
        Task<Motorista> ObterMotoristaPorCNH(string CNH); 
    }
}
