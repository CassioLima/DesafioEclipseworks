using Application;
using Domain;
using Domain.Entity;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace TesteQueryHandler
{
    public class UsuarioHandlerTests
    {
        private readonly Mock<IRepositoryBase<Usuario>> _mockRepository;
        private readonly UsuarioHandler _handler;

        public UsuarioHandlerTests()
        {
            _mockRepository = new Mock<IRepositoryBase<Usuario>>();
            _handler = new UsuarioHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_UsuarioQuery_RetornaUsuariosComSucesso()
        {
            // Arrange
            var usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nome = "Usuario 1" },
            new Usuario { Id = 2, Nome = "Usuario 2" }
        };

            _mockRepository.Setup(repo => repo.GetAll()).Returns(usuarios.AsQueryable());

            var request = new UsuarioQuery();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Content);
            var resultData = (List<UsuarioResult>)result.Content;
            Assert.Equal(2, resultData.Count);
            _mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_UsuarioQuery_SemUsuarios()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAll()).Returns(Enumerable.Empty<Usuario>().AsQueryable());

            var request = new UsuarioQuery();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Content);
            var resultData = (List<UsuarioResult>)result.Content;
            Assert.Empty(resultData);
            _mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}