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
    public class RemoverVeiculosServiceTest
    {
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IRepository<Veiculo>> _repositoryMock;
        private readonly VeiculoService _veiculoService;

        public RemoverVeiculosServiceTest()
        {
            _notificadorMock = new Mock<INotificador>();
            _repositoryMock = new Mock<IRepository<Veiculo>>();
            _veiculoService = new VeiculoService(_notificadorMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task Remover_DeveNotificarQuandoVeiculoNaoForEncontrado()
        {
            // Arrange
            var veiculoId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.ObterPorId(veiculoId)).ReturnsAsync((Veiculo)null);

            // Act
            await _veiculoService.Remover(veiculoId);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
            _repositoryMock.Verify(r => r.Remover(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task Remover_DeveRemoverVeiculoQuandoEncontrado()
        {
            // Arrange
            var veiculoId = Guid.NewGuid();
            var veiculoExistente = new Veiculo { Id = veiculoId };
            _repositoryMock.Setup(r => r.ObterPorId(veiculoId)).ReturnsAsync(veiculoExistente);

            // Act
            await _veiculoService.Remover(veiculoId);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Never);
            _repositoryMock.Verify(r => r.Remover(veiculoId), Times.Once);
        }
    }
}