using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using BTZ_Transports.Negocio.Models.Validacao;
using System;

namespace BTZ_Transports.Negocio.Servicos
{
    public class VeiculoService : BaseService, IVeiculoService
    {
        private readonly IRepository<Veiculo> _repository;

 
        public VeiculoService(INotificador notificador, IRepository<Veiculo> repository) : base(notificador)
        {
            _repository = repository;
        }

        public async Task Adicionar(Veiculo veiculo)
        {
            if (!ExecutarValidacao(new VeiculoValidacao(), veiculo)) return; 
            await _repository.Adicionar(veiculo);
        }

        public async Task Atualizar(Veiculo veiculo)
        {
            if (!ExecutarValidacao(new VeiculoValidacao(), veiculo)) return; 

            await _repository.Atualizar( veiculo);
        }
         
        public async Task<Veiculo> ObterPorId(Guid Id)
        {
            return await _repository.ObterPorId(Id);
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculos()
        {
            return await _repository.ObterTodos();
        }

        public async Task Remover(Guid id)
        {
            var motorista = await _repository.ObterPorId(id);
            if (motorista == null)
            {
                Notificar("Veiculo não encontrado.");
                return;
            }
            await _repository.Remover(id);
        }
    }
}
