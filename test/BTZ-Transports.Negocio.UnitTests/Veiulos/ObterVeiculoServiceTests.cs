using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using BTZ_Transports.Negocio.Servicos;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BTZ_Transports.Negocio.UnitTests.Veiulos
{
    public class ObterVeiculoServiceTests
    {
        private readonly VeiculoService _veiculoService;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IRepository<Veiculo>> _repositoryMock;

        public ObterVeiculoServiceTests()
        {
            _notificadorMock = new Mock<INotificador>();
            _repositoryMock = new Mock<IRepository<Veiculo>>();
            _veiculoService = new VeiculoService(_notificadorMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarVeiculoQuandoExistir()
        {
            // Arrange
            var veiculoId = Guid.NewGuid();
            var veiculoExistente = new Veiculo { Id = veiculoId, Nome = "Carro1" };

            _repositoryMock.Setup(r => r.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(veiculoExistente);

            // Act
            var resultado = await _veiculoService.ObterPorId(veiculoId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(veiculoExistente, resultado);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNullQuandoVeiculoNaoExistir()
        {
            // Arrange
            var veiculoId = Guid.NewGuid();

            _repositoryMock.Setup(r => r.ObterPorId(It.IsAny<Guid>())).ReturnsAsync((Veiculo)null);

            // Act
            var resultado = await _veiculoService.ObterPorId(veiculoId);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ObterVeiculos_DeveRetornarListaDeVeiculos()
        {
            // Arrange
            var veiculos = new List<Veiculo>
            {
                new Veiculo { Id = Guid.NewGuid(), Nome = "Carro1" },
                new Veiculo { Id = Guid.NewGuid(), Nome = "Carro2" },
            };

            _repositoryMock.Setup(r => r.ObterTodos()).ReturnsAsync(veiculos);

            // Act
            var resultado = await _veiculoService.ObterVeiculos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(veiculos, resultado);
        }
    }
}