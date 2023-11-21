using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using BTZ_Transports.Negocio.Servicos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace BTZ_Transports.Negocio.UnitTests.Motoristas
{
    public class BuscarMotoristasServiceTests
    {
        private readonly MotoristaService _motoristaService;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IMotoristaRepository> _repositoryMock;
        public BuscarMotoristasServiceTests()
        {
            _notificadorMock = new Mock<INotificador>();
            _repositoryMock = new Mock<IMotoristaRepository>();
            _motoristaService = new MotoristaService(_repositoryMock.Object, _notificadorMock.Object);
        }


        [Fact]
        public async Task ObterMotoristas_DeveRetornarMotoristas()
        {
            // Arrange
            var motoristasEsperados = new List<Motorista>
            {
                new Motorista { Id = Guid.NewGuid(), Nome = "Motorista 1" },
                new Motorista { Id = Guid.NewGuid(), Nome = "Motorista 2" }
            };

            _repositoryMock.Setup(r => r.ObterTodos()).ReturnsAsync(motoristasEsperados);

            // Act
            var resultado = await _motoristaService.ObterMotoristas();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(motoristasEsperados.Count, resultado.Count());
        }

        [Fact]
        public async Task ObterPorCPF_DeveRetornarMotorista()
        {
            // Arrange
            var motoristaEsperado = new Motorista { Id = Guid.NewGuid(), Nome = "Motorista 1", CPF = "12345678901" };

            _repositoryMock.Setup(r => r.Buscar(It.IsAny<Expression<Func<Motorista, bool>>>())).ReturnsAsync(new List<Motorista> { motoristaEsperado });


            // Act
            var resultado = await _motoristaService.ObterPorCPF("12345678901");

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(motoristaEsperado.Id, resultado.Id);
        }

        [Fact]
        public async Task ObterPorCPF_DeveRetornarNuloSeNaoEncontrarMotorista()
        {
            // Arrange
            var repositoryMock = new Mock<IMotoristaRepository>();
            repositoryMock.Setup(r => r.Buscar(It.IsAny<Expression<Func<Motorista, bool>>>()))
                          .ReturnsAsync(new List<Motorista>());
             
            // Act
            var resultado = await _motoristaService.ObterPorCPF("12345678901");

            // Assert
            Assert.Null(resultado);
        }
    }
}
