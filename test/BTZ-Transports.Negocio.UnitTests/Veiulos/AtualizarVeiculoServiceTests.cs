using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using BTZ_Transports.Negocio.Notificacoes;
using BTZ_Transports.Negocio.Servicos;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BTZ_Transports.Negocio.UnitTests.Veiulos
{
    public class AtualizarVeiculoServiceTests
    { 
        private readonly VeiculoService _veiculoService;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IRepository<Veiculo>> _repositoryMock;

        public AtualizarVeiculoServiceTests()
        {
            _notificadorMock = new Mock<INotificador>();
            _repositoryMock = new Mock<IRepository<Veiculo>>();
            _veiculoService = new VeiculoService(_notificadorMock.Object, _repositoryMock.Object);
        }
 
        [Fact]
        public async Task AtualizarVeiculo_ComVeiculoValido_DeveAtualizarVeiculo()
        {
            // Arrange
            var veiculo = new Veiculo
            {
                Id = Guid.NewGuid(),
                Nome = "VeiculoTeste",
                CombustivelId = 1,
                Fabricante = "FabricanteTeste",
                AnoDeFabricacao = 2022,
                CapacidadeMaximaDoTanque = 100,
                Observacao = "ObservacaoTeste"
            };

            // Act
            await _veiculoService.Atualizar(veiculo);

            // Assert
            _repositoryMock.Verify(r => r.Atualizar(veiculo), Times.Once);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Never);
        }

        [Fact]
        public async Task AtualizarVeiculo_ComVeiculoInvalido_NaoDeveAtualizarVeiculo()
        {
            // Arrange
            var veiculo = new Veiculo { };

            // Act
            await _veiculoService.Atualizar(veiculo);

            // Assert
            _repositoryMock.Verify(r => r.Atualizar(It.IsAny<Veiculo>()), Times.Never);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Exactly(6));
        } 
    }
}