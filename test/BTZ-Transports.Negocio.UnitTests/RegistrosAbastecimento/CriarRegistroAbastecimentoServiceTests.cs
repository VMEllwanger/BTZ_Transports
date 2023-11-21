using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using BTZ_Transports.Negocio.Notificacoes;
using BTZ_Transports.Negocio.Servicos;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BTZ_Transports.Negocio.UnitTests.RegistrosAbastecimento
{
    public class CriarRegistroAbastecimentoServiceTests
    {
        private readonly RegistroAbastecimentoService _registroAbastecimentoService;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IRepository<RegistroAbastecimento>> _repositoryMock;
        private readonly Mock<IRepository<Veiculo>> _veiculoRepositoryMock;
        private readonly Mock<IMotoristaRepository> _motoristaRepositoryMock;
        private readonly Mock<ICombustivelRepository> _combustivelRepositoryMock;

        public CriarRegistroAbastecimentoServiceTests()
        {
            _notificadorMock = new Mock<INotificador>();
            _veiculoRepositoryMock = new Mock<IRepository<Veiculo>>();
            _motoristaRepositoryMock = new Mock<IMotoristaRepository>();
            _repositoryMock = new Mock<IRepository<RegistroAbastecimento>>();
            _combustivelRepositoryMock = new Mock<ICombustivelRepository>();
            _registroAbastecimentoService = new RegistroAbastecimentoService(_repositoryMock.Object, _veiculoRepositoryMock.Object, _motoristaRepositoryMock.Object, _combustivelRepositoryMock.Object, _notificadorMock.Object);
        }

        [Fact]
        public async Task Adicionar_DeveNotificarSeVeiculoNaoCadastrado()
        {
            // Arrange
            var registroAbastecimento = new RegistroAbastecimento { VeiculoId = Guid.NewGuid() }; 
            _veiculoRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.VeiculoId)).ReturnsAsync((Veiculo)null);

            // Act
            await _registroAbastecimentoService.Adicionar(registroAbastecimento);

            // Assert

            _notificadorMock.Verify(n => n.Handle(It.Is<Notificacao>(notificacao =>
            notificacao.Mensagem == "Veiculo informado não cadastrado")), Times.Once);
        }

        [Fact]
        public async Task Adicionar_DeveNotificarSeCombustivelInformadoErrado()
        {
            // Arrange
            var registroAbastecimento = new RegistroAbastecimento { VeiculoId = Guid.NewGuid(), CombustivelId = 1 };

            _veiculoRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.VeiculoId)).ReturnsAsync(new Veiculo { CombustivelId = 2 });

            var combustivelRepositoryMock = new Mock<ICombustivelRepository>();
            combustivelRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.CombustivelId)).ReturnsAsync((Combustivel)null);


            // Act
            await _registroAbastecimentoService.Adicionar(registroAbastecimento);

            // Assert 
            _notificadorMock.Verify(n => n.Handle(It.Is<Notificacao>(notificacao =>
            notificacao.Mensagem == "Combustivel informado errado")), Times.Once);
        }

        [Fact]
        public async Task Adicionar_DeveNotificarSeQuantidadeAbastecidaMaiorQueCapacidadeDoTanque()
        {
            // Arrange
            var registroAbastecimento = new RegistroAbastecimento
            {
                VeiculoId = Guid.NewGuid(),
                CombustivelId = 1,
                QuantidadeAbastecida = 100
            };

            _veiculoRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.VeiculoId)).ReturnsAsync(new Veiculo { CombustivelId = 1, CapacidadeMaximaDoTanque = 50 });
            _combustivelRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.CombustivelId)).ReturnsAsync(new Combustivel());

            // Act
            await _registroAbastecimentoService.Adicionar(registroAbastecimento);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.Is<Notificacao>(notificacao =>
            notificacao.Mensagem == "Capacidade Máxima do tanque do Veiculo é inferior a quantidade abastecida informada")), Times.Once);
        }

        [Fact]
        public async Task Adicionar_DeveNotificarSeCombustivelNaoCadastrado()
        {
            // Arrange
            var registroAbastecimento = new RegistroAbastecimento { VeiculoId = Guid.NewGuid(), CombustivelId = 4 };

            _veiculoRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.VeiculoId)).ReturnsAsync(new Veiculo { CombustivelId = 4, CapacidadeMaximaDoTanque = 50 });
            _combustivelRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.CombustivelId)).ReturnsAsync((Combustivel)null);
             
            // Act
            await _registroAbastecimentoService.Adicionar(registroAbastecimento);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.Is<Notificacao>(notificacao =>
                 notificacao.Mensagem == "Combustivel informado não cadastrado")), Times.Once);
        }

        [Fact]
        public async Task Adicionar_DeveNotificarSeMotoristaNaoCadastrado()
        {
            // Arrange
            var registroAbastecimento = new RegistroAbastecimento { VeiculoId = Guid.NewGuid(), CombustivelId = 1 };

            _veiculoRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.VeiculoId)).ReturnsAsync(new Veiculo { CombustivelId = 1, CapacidadeMaximaDoTanque = 50 });
            _combustivelRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.CombustivelId)).ReturnsAsync(new Combustivel());
            var motoristaRepositoryMock = new Mock<IMotoristaRepository>();
            motoristaRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.MotoristaId)).ReturnsAsync((Motorista)null);

            // Act
            await _registroAbastecimentoService.Adicionar(registroAbastecimento);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.Is<Notificacao>(notificacao =>
                 notificacao.Mensagem == "Motorista informado não cadastrado")), Times.Once);
        }

        [Fact]
        public async Task Adicionar_DeveAdicionarRegistroAbastecimentoSeTodasAsValidacoesPassarem()
        {
            // Arrange
            var registroAbastecimento = new RegistroAbastecimento
            {
                VeiculoId = Guid.NewGuid(),
                CombustivelId = 1,
                QuantidadeAbastecida = 50,
                MotoristaId = Guid.NewGuid()
            };

            _veiculoRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.VeiculoId)).ReturnsAsync(new Veiculo { CombustivelId = registroAbastecimento.CombustivelId, CapacidadeMaximaDoTanque = 100 });

            _combustivelRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.CombustivelId)).ReturnsAsync(new Combustivel { Preco = 2.5m });
            _motoristaRepositoryMock.Setup(r => r.ObterPorId(registroAbastecimento.MotoristaId)).ReturnsAsync(new Motorista());
             
            // Act
            await _registroAbastecimentoService.Adicionar(registroAbastecimento);

            // Assert 
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<RegistroAbastecimento>()), Times.Once);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Never);
        }
    }
}