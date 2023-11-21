using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using BTZ_Transports.Negocio.Notificacoes;
using BTZ_Transports.Negocio.Servicos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace BTZ_Transports.Negocio.UnitTests.RegistrosAbastecimento
{
    public class ObterRegistroAbastecimentoServiceTests
    {
        private readonly RegistroAbastecimentoService _registroAbastecimentoService;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IRepository<RegistroAbastecimento>> _repositoryMock;
        private readonly Mock<IRepository<Veiculo>> _veiculoRepositoryMock;
        private readonly Mock<IMotoristaRepository> _motoristaRepositoryMock;
        private readonly Mock<ICombustivelRepository> _combustivelRepositoryMock;

        public ObterRegistroAbastecimentoServiceTests()
        {
            _notificadorMock = new Mock<INotificador>();
            _veiculoRepositoryMock = new Mock<IRepository<Veiculo>>();
            _motoristaRepositoryMock = new Mock<IMotoristaRepository>();
            _repositoryMock = new Mock<IRepository<RegistroAbastecimento>>();
            _combustivelRepositoryMock = new Mock<ICombustivelRepository>();
            _registroAbastecimentoService = new RegistroAbastecimentoService(_repositoryMock.Object, _veiculoRepositoryMock.Object,
                                                                            _motoristaRepositoryMock.Object, _combustivelRepositoryMock.Object,
                                                                            _notificadorMock.Object);
        }

        [Fact]
        public async Task ObterPorMotoristaId_DeveRetornarRegistrosQuandoMotoristaExiste()
        {
            // Arrange
            var motoristaId = Guid.NewGuid();
            var registros = new List<RegistroAbastecimento>
            {
                new RegistroAbastecimento { MotoristaId = motoristaId },
                new RegistroAbastecimento { MotoristaId = motoristaId }
            };
            _motoristaRepositoryMock.Setup(r => r.ObterPorId(motoristaId)).ReturnsAsync(new Motorista());
            _repositoryMock.Setup(r => r.Buscar(It.IsAny<Expression<Func<RegistroAbastecimento, bool>>>()))
                                                 .ReturnsAsync(registros);

            // Act
            var result = await _registroAbastecimentoService.ObterPorMotoristaId(motoristaId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task ObterPorMotoristaId_DeveNotificarQuandoMotoristaNaoExiste()
        {
            // Arrange
            var motoristaId = Guid.NewGuid();
            var registros = new List<RegistroAbastecimento>
            {
                new RegistroAbastecimento { MotoristaId = motoristaId },
                new RegistroAbastecimento { MotoristaId = motoristaId }
            };
            _motoristaRepositoryMock.Setup(r => r.ObterPorId(motoristaId)).ReturnsAsync((Motorista)null);
            _repositoryMock.Setup(r => r.Buscar(It.IsAny<Expression<Func<RegistroAbastecimento, bool>>>()))
                                                 .ReturnsAsync(registros);

            // Act
            var result = await _registroAbastecimentoService.ObterPorMotoristaId(motoristaId);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.Is<Notificacao>(notificacao =>
              notificacao.Mensagem == "Motorista informado não cadastrado")), Times.Once);
        }

        [Fact]
        public async Task ObterPorVeiculoId_DeveRetornarRegistrosQuandoVeiculoExiste()
        {
            // Arrange
            var veiculoId = Guid.NewGuid();
            var registros = new List<RegistroAbastecimento>
            {
                new RegistroAbastecimento { VeiculoId = veiculoId },
                new RegistroAbastecimento { VeiculoId = veiculoId }
            };
            _veiculoRepositoryMock.Setup(r => r.ObterPorId(veiculoId)).ReturnsAsync(new Veiculo());

            _repositoryMock.Setup(r => r.Buscar(It.IsAny<Expression<Func<RegistroAbastecimento, bool>>>()))
                                                 .ReturnsAsync(registros);

            // Act
            var result = await _registroAbastecimentoService.ObterPorVeiculoId(veiculoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task ObterPorVeiculoId_DeveNotificarQuandoVeiculoNaoExiste()
        {
            // Arrange
            var veiculoId = Guid.NewGuid();
            var registros = new List<RegistroAbastecimento>
            {
                new RegistroAbastecimento { VeiculoId = veiculoId },
                new RegistroAbastecimento { VeiculoId = veiculoId }
            };
            _veiculoRepositoryMock.Setup(r => r.ObterPorId(veiculoId)).ReturnsAsync((Veiculo)null);
            _repositoryMock.Setup(r => r.Buscar(It.IsAny<Expression<Func<RegistroAbastecimento, bool>>>()))
                                                 .ReturnsAsync(registros);

            // Act
            var result = await _registroAbastecimentoService.ObterPorVeiculoId(veiculoId);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.Is<Notificacao>(notificacao =>
              notificacao.Mensagem == "Veiculo informado não cadastrado")), Times.Once);
        }

        [Fact]
        public async Task ObterRegistroAbastecimentos_DeveRetornarTodosRegistros()
        {
            // Arrange
            var registros = new List<RegistroAbastecimento>
            {
                new RegistroAbastecimento(),
                new RegistroAbastecimento(),
                new RegistroAbastecimento()
            };
            _repositoryMock.Setup(r => r.ObterTodos()).ReturnsAsync(registros);

            // Act
            var result = await _registroAbastecimentoService.ObterRegistroAbastecimentos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task ObterRegistroAbastecimentos_DeveRetornarRegistroPorId()
        {
            // Arrange
            var registroAbastecimento = new RegistroAbastecimento
            {
                Id = Guid.NewGuid(),
                VeiculoId = Guid.NewGuid(),
                CombustivelId = 1,
                QuantidadeAbastecida = 50,
                MotoristaId = Guid.NewGuid(),
            };

            _repositoryMock.Setup(r => r.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(registroAbastecimento);

            // Act
            var result = await _registroAbastecimentoService.ObterPorId(registroAbastecimento.Id);

            // Assert
            Assert.NotNull(result); 
        }
    }
}

