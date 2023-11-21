using BTZ_Transports.Negocio.Enums;
using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using BTZ_Transports.Negocio.Notificacoes;
using BTZ_Transports.Negocio.Servicos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace BTZ_Transports.Negocio.UnitTests.Motoristas
{
    public class CriarMotoristaServiceTests
    {
        private readonly MotoristaService _motoristaService;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IMotoristaRepository> _repositoryMock;

        public CriarMotoristaServiceTests()
        {
            _notificadorMock = new Mock<INotificador>();
            _repositoryMock = new Mock<IMotoristaRepository>();
            _motoristaService = new MotoristaService(_repositoryMock.Object, _notificadorMock.Object);
        }

        [Fact]
        public async Task Adicionar_DeveNotificarQuandoMotoristaComCPFExistente()
        {
            // Arrange
            string cpf = "81835863094";
            var motorista = new Motorista { 
                Id = Guid.NewGuid(),
                CPF = "81835863094",
                Nome = "César Isaac ",
                NumeroCNH = "15470651319",
                CategoriaCNH = "B",
                DataDeNascimento = new DateTime(1990,07,15),
                Status = StatusMotorista.Ativo
            };

            var motoristaExistente = new Motorista
            {
                Id = Guid.NewGuid(),
                CPF = "81835863094",
                Nome = "Gabriel Novaes",
                NumeroCNH = "01610651319",
                CategoriaCNH = "B",
                DataDeNascimento = new DateTime(1990, 07, 15),
                Status = StatusMotorista.Ativo
            };

            _repositoryMock.Setup(r => r.Buscar(It.IsAny<Expression<Func<Motorista, bool>>>()))
                           .ReturnsAsync(new List<Motorista> { motoristaExistente });

            // Act
            await _motoristaService.Adicionar(motorista);

            // Assert
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<Motorista>()), Times.Never);
        }

        [Fact]
        public async Task Adicionar_NaoDeveNotificarQuandoMotoristaComCPFNaoExistente()
        {
            // Arrange
            var motorista = new Motorista
            {
                Id = Guid.NewGuid(),
                CPF = "79915135564",
                Nome = "César Isaac Gabriel Novaes",
                NumeroCNH = "01610651319",
                CategoriaCNH = "B",
                DataDeNascimento = new DateTime(1990, 07, 15),
                Status = StatusMotorista.Ativo
            };

            _repositoryMock.Setup(r => r.ObterMotoristaPorCpf(It.IsAny<string>())).ReturnsAsync((Motorista)null); 

            // Act
            await _motoristaService.Adicionar(motorista);
 
            // Assert
            _repositoryMock.Verify(r => r.Adicionar(motorista), Times.Once);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Never);
        }

        //TODO : Criar teste para validar CPF
    }
}
