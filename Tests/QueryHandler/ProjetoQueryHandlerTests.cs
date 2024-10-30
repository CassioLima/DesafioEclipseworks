using Application;
using Domain;


namespace TesteQueryHandler
{
    public class ProjetoQueryHandlerTests
    {
        private readonly Mock<IRepositoryBase<Projeto>> _mockRepositoryProjeto;
        private readonly Mock<IRepositoryBase<Tarefa>> _mockRepositoryTarefa;
        private readonly Mock<IRepositoryBase<Usuario>> _mockRepositoryUsuario;
        private readonly ProjetoHandler _handler;

        public ProjetoQueryHandlerTests()
        {
            _mockRepositoryProjeto = new Mock<IRepositoryBase<Projeto>>();
            _mockRepositoryTarefa = new Mock<IRepositoryBase<Tarefa>>();
            _mockRepositoryUsuario = new Mock<IRepositoryBase<Usuario>>();

            _handler = new ProjetoHandler(
                _mockRepositoryProjeto.Object,
                _mockRepositoryTarefa.Object,
                _mockRepositoryUsuario.Object
            );
        }

        [Fact]
        public async Task Handle_ProjetoUsuarioQuery_Sucesso()
        {
            // Arrange
            var usuarioId = 1;
            var request = new ProjetoUsuarioQuery { IdUsuario = usuarioId };

            var projetos = new List<Projeto>
        {
            new Projeto { Id = 1, UsuarioId = usuarioId },
            new Projeto { Id = 2, UsuarioId = usuarioId }
        };

            var tarefas = new List<Tarefa>
        {
            new Tarefa { Id = 1, ProjetoId = 1 },
            new Tarefa { Id = 2, ProjetoId = 2 }
        };

            var usuario = new Usuario { Id = usuarioId, Nome = "Teste" };

            _mockRepositoryProjeto.Setup(repo => repo.GetAll()).Returns(projetos.AsQueryable());
            _mockRepositoryTarefa.Setup(repo => repo.GetAll()).Returns(tarefas.AsQueryable());
            _mockRepositoryUsuario.Setup(repo => repo.GetById(usuarioId)).Returns(usuario);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Content);
            var resultData = (List<ProjetoResult>)result.Content;
            Assert.Equal(2, resultData.Count);
            _mockRepositoryProjeto.Verify(repo => repo.GetAll(), Times.Once);
            _mockRepositoryTarefa.Verify(repo => repo.GetAll(), Times.AtLeastOnce);
            _mockRepositoryUsuario.Verify(repo => repo.GetById(usuarioId), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Handle_ProjetoQuery_Sucesso()
        {
            // Arrange
            var request = new ProjetoQuery();

            var projetos = new List<Projeto>
        {
            new Projeto { Id = 1, UsuarioId = 1 },
            new Projeto { Id = 2, UsuarioId = 2 }
        };

            var tarefas = new List<Tarefa>
        {
            new Tarefa { Id = 1, ProjetoId = 1 },
            new Tarefa { Id = 2, ProjetoId = 2 }
        };

            var usuario1 = new Usuario { Id = 1, Nome = "Usuario1" };
            var usuario2 = new Usuario { Id = 2, Nome = "Usuario2" };

            _mockRepositoryProjeto.Setup(repo => repo.GetAll()).Returns(projetos.AsQueryable());
            _mockRepositoryTarefa.Setup(repo => repo.GetAll()).Returns(tarefas.AsQueryable());
            _mockRepositoryUsuario.Setup(repo => repo.GetById(1)).Returns(usuario1);
            _mockRepositoryUsuario.Setup(repo => repo.GetById(2)).Returns(usuario2);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Content);
            var resultData = (List<ProjetoResult>)result.Content;
            Assert.Equal(2, resultData.Count);
            _mockRepositoryProjeto.Verify(repo => repo.GetAll(), Times.Once);
            _mockRepositoryTarefa.Verify(repo => repo.GetAll(), Times.AtLeastOnce);
            _mockRepositoryUsuario.Verify(repo => repo.GetById(It.IsAny<int>()), Times.AtLeastOnce);
        }
    }
}