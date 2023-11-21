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
    public class CriarVeiculoServiceTests
    {
        private readonly VeiculoService _veiculoService;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IRepository<Veiculo>> _repositoryMock;

        public CriarVeiculoServiceTests()
        {
            _notificadorMock = new Mock<INotificador>();
            _repositoryMock = new Mock<IRepository<Veiculo>>();
            _veiculoService = new VeiculoService(_notificadorMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task AdicionarVeiculo_ComVeiculoValido_DeveAdicionarVeiculo()
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
            await _veiculoService.Adicionar(veiculo);

            // Assert
            _repositoryMock.Verify(r => r.Adicionar(veiculo), Times.Once);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Never);
        }

        [Fact]
        public async Task AdicionarVeiculo_ComVeiculoValido_DeveAdicionarVeiculoSemObservacao()
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
            };

            // Act
            await _veiculoService.Adicionar(veiculo);

            // Assert
            _repositoryMock.Verify(r => r.Adicionar(veiculo), Times.Once);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Never);
        }

        [Fact]
        public async Task AdicionarVeiculo_ComVeiculoInvalido_NaoDeveAdicionarVeiculoComObjetoVazio()
        {
            // Arrange
            var veiculo = new Veiculo { };

            // Act
            await _veiculoService.Adicionar(veiculo);

            // Assert
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<Veiculo>()), Times.Never);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Exactly(6));
        }

        [Fact]
        public async Task AdicionarVeiculo_ComVeiculoInvalido_NaoDeveAdicionarVeiculoCasoCapacidadeTanqueSejaIgualaZero()
        {
            // Arrange
            var veiculo = new Veiculo
            {
                Id = Guid.NewGuid(),
                Nome = "VeiculoTeste",
                CombustivelId = 1,
                Fabricante = "FabricanteTeste",
                AnoDeFabricacao = 2022,
                CapacidadeMaximaDoTanque = 0,
                Observacao = "ObservacaoTeste"
            };

            // Act
            await _veiculoService.Adicionar(veiculo);

            // Assert
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<Veiculo>()), Times.Never);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Exactly(2));
        }

        [Fact]
        public async Task AdicionarVeiculo_ComVeiculoInvalido_NaoDeveAdicionarVeiculoCasoNomeSejaNulo()
        {
            // Arrange
            var veiculo = new Veiculo
            {
                Id = Guid.NewGuid(),
                CombustivelId = 1,
                Fabricante = "FabricanteTeste",
                AnoDeFabricacao = 2022,
                CapacidadeMaximaDoTanque = 200,
                Observacao = "ObservacaoTeste"
            };

            // Act
            await _veiculoService.Adicionar(veiculo);

            // Assert
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<Veiculo>()), Times.Never);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
        }

        [Fact]
        public async Task AdicionarVeiculo_ComVeiculoInvalido_NaoDeveAdicionarVeiculoCasoFabricanteSejaNulo()
        {
            // Arrange
            var veiculo = new Veiculo
            {
                Id = Guid.NewGuid(),
                Nome = "VeiculoTeste",
                CombustivelId = 1,
                Fabricante = "FabricanteTeste",
                CapacidadeMaximaDoTanque = 100,
                Observacao = "ObservacaoTeste"
            };

            // Act
            await _veiculoService.Adicionar(veiculo);

            // Assert
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<Veiculo>()), Times.Never);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
        }

        [Fact]
        public async Task AdicionarVeiculo_ComVeiculoInvalido_NaoDeveAdicionarVeiculoCasoCombustivelSejaNulo()
        {
            // Arrange
            var veiculo = new Veiculo
            {
                Id = Guid.NewGuid(),
                Nome = "VeiculoTeste",
                Fabricante = "FabricanteTeste",
                AnoDeFabricacao = 2022,
                CapacidadeMaximaDoTanque = 100,
                Observacao = "ObservacaoTeste"
            };

            // Act
            await _veiculoService.Adicionar(veiculo);

            // Assert
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<Veiculo>()), Times.Never);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
        }

 

        [Fact]
        public async Task AdicionarVeiculo_ComVeiculoInvalido_NaoDeveAdicionarVeiculoCasoAnoDeFabricacaoSejaNulo()
        {
            // Arrange
            var veiculo = new Veiculo
            {
                Id = Guid.NewGuid(),
                Nome = "VeiculoTeste",
                CombustivelId = 1,
                AnoDeFabricacao = 2022,
                CapacidadeMaximaDoTanque = 100,
                Observacao = "ObservacaoTeste"
            };

            // Act
            await _veiculoService.Adicionar(veiculo);

            // Assert
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<Veiculo>()), Times.Never);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
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