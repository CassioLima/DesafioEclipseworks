using Application;
using Domain;
using Enums;



namespace TesteQueryHandler
{
    public class TarefaHandlerTests
    {
        private readonly Mock<IRepositoryBase<Tarefa>> _mockRepositoryTarefa;
        private readonly Mock<IRepositoryBase<Usuario>> _mockRepositoryUsuario;
        private readonly Mock<INotificationContext> _mockNotificationContext;
        private readonly TarefaHandler _handler;

        public TarefaHandlerTests()
        {
            _mockRepositoryTarefa = new Mock<IRepositoryBase<Tarefa>>();
            _mockRepositoryUsuario = new Mock<IRepositoryBase<Usuario>>();
            _mockNotificationContext = new Mock<INotificationContext>();

            _handler = new TarefaHandler(
                _mockRepositoryTarefa.Object,
                _mockRepositoryUsuario.Object,
                _mockNotificationContext.Object
            );
        }

        [Fact]
        public async Task Handle_TarefaQuery_Sucesso()
        {
            // Arrange
            var tarefas = new List<Tarefa>
        {
            new Tarefa { Id = 1, Titulo = "Tarefa 1" },
            new Tarefa { Id = 2, Titulo = "Tarefa 2" }
        };

            _mockRepositoryTarefa.Setup(repo => repo.GetAll()).Returns(tarefas.AsQueryable());

            var request = new TarefaQuery();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Content);
            var resultData = (List<TarefaResult>)result.Content;
            Assert.Equal(2, resultData.Count);
            _mockRepositoryTarefa.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_TarefaProjetoQuery_Sucesso()
        {
            // Arrange
            var tarefas = new List<Tarefa>
        {
            new Tarefa { Id = 1, ProjetoId = 1, Titulo = "Tarefa Projeto 1" },
            new Tarefa { Id = 2, ProjetoId = 1, Titulo = "Tarefa Projeto 2" }
        };

            _mockRepositoryTarefa.Setup(repo => repo.GetAll()).Returns(tarefas.AsQueryable());

            var request = new TarefaProjetoQuery { IdProjeto = 1 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Content);
            var resultData = (List<TarefaProjetoResult>)result.Content;
            Assert.Equal(2, resultData.Count);
            _mockRepositoryTarefa.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_TarefaRelatorioQuery_UsuarioNaoGerente()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Usuario Teste", Funcao = Funcao.Comum };
            _mockRepositoryUsuario.Setup(repo => repo.GetById(1)).Returns(usuario);

            var request = new TarefaRelatorioQuery { IdUsuario = 1 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            _mockNotificationContext.Verify(nc => nc.AddNotification("Usuario", $"O Usuário {usuario.Nome} não tem a função de gerente!"), Times.Once);
        }

        [Fact]
        public async Task Handle_TarefaRelatorioQuery_UsuarioNaoInformado()
        {
            // Arrange
            var request = new TarefaRelatorioQuery { IdUsuario = 0 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            _mockNotificationContext.Verify(nc => nc.AddNotification("IdUsuario", "Usuário não informado"), Times.Once);
        }

        [Fact]
        public async Task Handle_TarefaRelatorioQuery_Sucesso()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Gerente", Funcao = Funcao.Gerente };
            var tarefas = new List<Tarefa>
            {
                new Tarefa { Id = 1, UsuarioId = 1, DataVencimento = DateTime.Now.AddDays(-5), Status = Enums.Status.Concluida },
                new Tarefa { Id = 2, UsuarioId = 1, DataVencimento = DateTime.Now.AddDays(-10), Status = Enums.Status.Concluida }
            };

            _mockRepositoryUsuario.Setup(repo => repo.GetById(1)).Returns(usuario);
            _mockRepositoryTarefa.Setup(repo => repo.GetAll()).Returns(tarefas.AsQueryable());

            var request = new TarefaRelatorioQuery { IdUsuario = 1 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Content);
            var resultData = (TarefaRelatorioResult)result.Content;
            Assert.Equal("Gerente", resultData.Usuario);
            Assert.Equal(2, resultData.TarefasComcluidas);
        }
    }
}