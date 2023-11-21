using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using BTZ_Transports.Negocio.Models.Validacao;

namespace BTZ_Transports.Negocio.Servicos
{
    public class MotoristaService : BaseService, IMotoristaService
    {
        private readonly IMotoristaRepository _repository;

        public MotoristaService(IMotoristaRepository repository, INotificador notificador) : base(notificador)
        {
            _repository = repository;
        }
        public async Task Adicionar(Motorista motorista)
        {
            if (!ExecutarValidacao(new MotoristaValidacao(), motorista)) return;

            var motoristaExitente = await ObterPorCPF(motorista.CPF);
            if (motoristaExitente != null)
            {
                Notificar("Já existe um motorista com o CPF infomado.");
                return;
            }

            await _repository.Adicionar(motorista);
        }

        public async Task Atualizar(Motorista motorista)
        {
            if (!ExecutarValidacao(new MotoristaValidacao(), motorista)) return;
            if (_repository.ObterPorId(motorista.Id).Result == null)
            {
                Notificar("Motorista não encontrado.");
                return;
            }

            if (_repository.Buscar(m => m.CPF == motorista.CPF && m.Id != motorista.Id).Result.Any())
            {
                Notificar("Já existe um motorista com o CPF infomado.");
                 return;
            }

            await _repository.Atualizar( motorista);
        }

        public async Task<IEnumerable<Motorista>> ObterMotoristas()
        {
            return await _repository.ObterTodos();
        }

        public async Task<Motorista> ObterPorCPF(string CPF)
        {
            var motorista = await _repository.Buscar(m => m.CPF == CPF);
            return motorista.FirstOrDefault();
        }

        public async Task<Motorista> ObterPorId(Guid Id)
        {
            return await _repository.ObterPorId(Id);
        }

        public async Task Remover(Guid id)
        {
            var motorista = await _repository.ObterPorId(id);
            if(motorista == null)
            {
                Notificar("Motorista não encontrado.");
                return; 
            }

            await _repository.Remover(id);
        } 
    }
}
