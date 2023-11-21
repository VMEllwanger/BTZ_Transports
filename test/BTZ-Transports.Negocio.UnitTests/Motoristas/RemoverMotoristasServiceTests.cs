using BTZ_Transports.Negocio.Enums;
using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using BTZ_Transports.Negocio.Notificacoes;
using BTZ_Transports.Negocio.Servicos;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BTZ_Transports.Negocio.UnitTests.Motoristas
{
    public class RemoverMotoristasServiceTests
    {
        private readonly MotoristaService _motoristaService;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IMotoristaRepository> _repositoryMock;

        public RemoverMotoristasServiceTests()
        {
            _notificadorMock = new Mock<INotificador>();
            _repositoryMock = new Mock<IMotoristaRepository>();
            _motoristaService = new MotoristaService(_repositoryMock.Object, _notificadorMock.Object);
        }

        [Fact]
        public async Task Remover_DeveNotificarQuandoMotoristaNaoForEncontrado()
        {
            // Arrange
            var motoristaId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.ObterPorId(motoristaId)).ReturnsAsync((Motorista)null);

            // Act
            await _motoristaService.Remover(motoristaId);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
            _repositoryMock.Verify(r => r.Remover(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task Remover_DeveRemoverMotoristaQuandoEncontrado()
        {
            // Arrange
            var motoristaId = Guid.NewGuid();
            var motoristaExistente = new Motorista { Id = motoristaId };
            _repositoryMock.Setup(r => r.ObterPorId(motoristaId)).ReturnsAsync(motoristaExistente);

            // Act
            await _motoristaService.Remover(motoristaId);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Never);
            _repositoryMock.Verify(r => r.Remover(motoristaId), Times.Once);
        }
    }
}
