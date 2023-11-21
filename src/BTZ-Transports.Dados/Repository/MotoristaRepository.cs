using BTZ_Transports.Dados.Context;
using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;

namespace BTZ_Transports.Dados.Repository
{
    public class MotoristaRepository : Repository<Motorista>, IMotoristaRepository
    {
        public MotoristaRepository(BTZContext context) : base(context)
        {
        }

        async Task<Motorista> IMotoristaRepository.ObterMotoristaPorCNH(string NumeroCNH)
        {
            return Buscar(m => m.NumeroCNH == NumeroCNH).Result.FirstOrDefault();
        }

        async Task<Motorista> IMotoristaRepository.ObterMotoristaPorCpf(string CPF)
        {
            return Buscar(m => m.CPF == CPF).Result.FirstOrDefault(); ;
        }
    }
}
