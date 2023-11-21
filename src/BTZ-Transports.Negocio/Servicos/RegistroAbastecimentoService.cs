using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;

namespace BTZ_Transports.Negocio.Servicos
{
    public class RegistroAbastecimentoService : BaseService, IRegistroAbastecimentoService
    {
        private readonly IRepository<RegistroAbastecimento> _registroAbastecimentoRepository;
        private readonly IRepository<Veiculo> _veiculoRepository;
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly ICombustivelRepository _combustivelRepository;

        public RegistroAbastecimentoService(IRepository<RegistroAbastecimento> registroAbastecimentoRepository,
                                                IRepository<Veiculo> veiculoRepository,
                                                IMotoristaRepository motoristaRepository,
                                                ICombustivelRepository combustivelRepository,
                                                INotificador notificador) : base(notificador)
        {
            _registroAbastecimentoRepository = registroAbastecimentoRepository;
            _veiculoRepository = veiculoRepository;
            _motoristaRepository = motoristaRepository;
            _combustivelRepository = combustivelRepository;
        }

        public async Task Adicionar(RegistroAbastecimento registroAbastecimento)
        {
            var veiculo = await _veiculoRepository.ObterPorId(registroAbastecimento.VeiculoId);

            if (veiculo == null)
            {
                Notificar("Veiculo informado não cadastrado");
                return;
            }

            if (veiculo.CombustivelId != registroAbastecimento.CombustivelId)
            {
                Notificar("Combustivel informado errado");
                return;
            }

            if (veiculo.CapacidadeMaximaDoTanque < registroAbastecimento.QuantidadeAbastecida)
            {
                Notificar("Capacidade Máxima do tanque do Veiculo é inferior a quantidade abastecida informada");
                return;
            }

            var combustivel = await _combustivelRepository.ObterPorId(registroAbastecimento.CombustivelId);

            if (combustivel == null)
            {
                Notificar("Combustivel informado não cadastrado");
                return;
            }

            var motorista = await _motoristaRepository.ObterPorId(registroAbastecimento.MotoristaId);

            if (motorista == null)
            {
                Notificar("Motorista informado não cadastrado");
                return;
            }

            registroAbastecimento.somarValorTotal(combustivel.Preco);

            await _registroAbastecimentoRepository.Adicionar(registroAbastecimento);
        }

        public async Task<RegistroAbastecimento> ObterPorId(Guid Id)
        {
            return await _registroAbastecimentoRepository.ObterPorId(Id);
        }

        public async Task<IEnumerable<RegistroAbastecimento>> ObterPorMotoristaId(Guid MotoristaId)
        {
            var motorista = await _motoristaRepository.ObterPorId(MotoristaId);
            if (motorista == null)
            {
                Notificar("Motorista informado não cadastrado");
                return Enumerable.Empty<RegistroAbastecimento>();
            }

            return await _registroAbastecimentoRepository.Buscar(ra => ra.MotoristaId == MotoristaId);
        }

        public async Task<IEnumerable<RegistroAbastecimento>> ObterPorVeiculoId(Guid VeiculoId)
        {
            var veiculo = await _veiculoRepository.ObterPorId(VeiculoId);
            if (veiculo == null)
            {
                Notificar("Veiculo informado não cadastrado");
                return Enumerable.Empty<RegistroAbastecimento>();
            }

            return await _registroAbastecimentoRepository.Buscar(ra => ra.VeiculoId == VeiculoId);
        }

        public async Task<IEnumerable<RegistroAbastecimento>> ObterRegistroAbastecimentos()
        {
            return await _registroAbastecimentoRepository.ObterTodos();
        }
    }
}
