using BTZ_Transports.Negocio.Enums;
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

namespace BTZ_Transports.Negocio.UnitTests.Motoristas
{
    public class AtualizarMotoristaTests
    {
        private readonly MotoristaService _motoristaService;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IMotoristaRepository> _repositoryMock;

        public AtualizarMotoristaTests()
        {
            _notificadorMock = new Mock<INotificador>();
            _repositoryMock = new Mock<IMotoristaRepository>();
            _motoristaService = new MotoristaService(_repositoryMock.Object, _notificadorMock.Object);
        }

        [Fact]
        public async Task Atualizar_DeveNotificarQuandoMotoristaNaoEncontrado()
        {
            // Arrange
            var motorista = new Motorista
            {
                Id = Guid.NewGuid(),
                CPF = "81835863094",
                Nome = "César Isaac Gabriel Novaes",
                NumeroCNH = "01610651319",
                CategoriaCNH = "B",
                DataDeNascimento = new DateTime(1990, 07, 15),
                Status = StatusMotorista.Ativo
            };

            _repositoryMock.Setup(r => r.ObterPorId(motorista.Id)).ReturnsAsync((Motorista)null);
             
            // Act
            await _motoristaService.Atualizar(motorista);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.Is<Notificacao>(notificacao =>
                notificacao.Mensagem == "Motorista não encontrado.")), Times.Once);

            _repositoryMock.Verify(r => r.Atualizar(It.IsAny<Motorista>()), Times.Never);
        }

        [Fact]
        public async Task Atualizar_DeveNotificarQuandoMotoristaComCPFJaExiste()
        {
            // Arrange
            var motorista = new Motorista
            {
                Id = Guid.NewGuid(),
                CPF = "81835863094",
                Nome = "César Isaac Gabriel Novaes",
                NumeroCNH = "01610651319",
                CategoriaCNH = "B",
                DataDeNascimento = new DateTime(1990, 07, 15),
                Status = StatusMotorista.Ativo
            };

            var motoristaExistente = new Motorista
            {
                Id = Guid.NewGuid(),
                CPF = "81835863094",
            };

            _repositoryMock.Setup(r => r.ObterPorId(motorista.Id)).ReturnsAsync(new Motorista());
            _repositoryMock.Setup(r => r.Buscar(It.IsAny<Expression<Func<Motorista, bool>>>()))
                           .ReturnsAsync(new List<Motorista> { motoristaExistente });

            // Act
            await _motoristaService.Atualizar(motorista);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.Is<Notificacao>(notificacao =>
                notificacao.Mensagem == "Já existe um motorista com o CPF infomado.")), Times.Once);

            _repositoryMock.Verify(r => r.Atualizar(It.IsAny<Motorista>()), Times.Never);
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarMotoristaQuandoValido()
        {
            // Arrange
            var motorista = new Motorista
            {
                Id = Guid.NewGuid(),
                CPF = "81835863094",
                Nome = "César Isaac Gabriel Novaes",
                NumeroCNH = "01610651319",
                CategoriaCNH = "B",
                DataDeNascimento = new DateTime(1990, 07, 15),
                Status = StatusMotorista.Ativo
            };

            var mockRepository = new Mock<IMotoristaRepository>();
            mockRepository.Setup(r => r.ObterPorId(motorista.Id)).ReturnsAsync(new Motorista());
            mockRepository.Setup(r => r.Buscar(It.IsAny<Expression<Func<Motorista, bool>>>()))
                          .ReturnsAsync(Enumerable.Empty<Motorista>());

            var mockNotificador = new Mock<INotificador>();

            var motoristaService = new MotoristaService(mockRepository.Object, mockNotificador.Object);

            // Act
            await motoristaService.Atualizar(motorista);

            // Assert
            mockNotificador.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Never);

            mockRepository.Verify(r => r.Atualizar(motorista), Times.Once);
        }

        //TODO : Criar teste para validar CPF
    }
}