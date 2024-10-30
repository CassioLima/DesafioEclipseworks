using Application;
using Application.Command;
using Domain;


namespace TesteCommandHandler
{
    public class TarefaCommandHandlerTests
    {
        private readonly Mock<IRepositoryBase<Tarefa>> _mockRepository;
        private readonly Mock<IRepositoryBase<Projeto>> _mockRepositoryProjeto;
        private readonly Mock<IRepositoryBase<TarefaHistoricoAtualizacao>> _mockRepositoryHistorico;
        private readonly Mock<IRepositoryBase<TarefaComentario>> _mockRepositoryComentario;
        private readonly Mock<INotificationContext> _mockNotificationContext;

        private readonly TarefaCommandHandler _handler;

        public TarefaCommandHandlerTests()
        {
            _mockRepository = new Mock<IRepositoryBase<Tarefa>>();
            _mockRepositoryProjeto = new Mock<IRepositoryBase<Projeto>>();
            _mockRepositoryHistorico = new Mock<IRepositoryBase<TarefaHistoricoAtualizacao>>();
            _mockRepositoryComentario = new Mock<IRepositoryBase<TarefaComentario>>();
            _mockNotificationContext = new Mock<INotificationContext>();

            _handler = new TarefaCommandHandler(
                _mockNotificationContext.Object,
                _mockRepository.Object,
                _mockRepositoryProjeto.Object,
                _mockRepositoryHistorico.Object,
                _mockRepositoryComentario.Object
            );
        }

        [Fact]
        public async Task Handle_CriarTarefa_Sucesso()
        {
            // Arrange
            var command = new TarefaCriarComand
            (
                1,
                1,
                "Nova Tarefa",
                "Descrição da Tarefa",
                DateTime.Now.AddDays(10),
                Status.Pendente,
                Prioridade.Alta
            );

            var projeto = new Projeto { Id = 1, Tarefas = new List<Tarefa>() };
            _mockRepositoryProjeto.Setup(repo => repo.GetById(command.ProjetoId)).Returns(projeto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            _mockRepository.Verify(repo => repo.Save(It.IsAny<Tarefa>()), Times.Once);
        }

        [Fact]
        public async Task Handle_RemoverTarefa_Sucesso()
        {
            // Arrange
            var command = new TarefaRemoverProjetoComand(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            _mockRepository.Verify(repo => repo.Delete(command.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_AdicionarComentario_Sucesso()
        {
            // Arrange
            var command = new TarefaAdicionarComentarioComand
            (
                1,
                1,
                "Comentário de teste"
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            _mockRepositoryComentario.Verify(repo => repo.Save(It.IsAny<TarefaComentario>()), Times.Once);
        }

        [Fact]
        public async Task Handle_AtualizarTarefa_Sucesso()
        {
            // Arrange
            var command = new TarefaAtualizarComand
            (
                1,
                1,
                "Tarefa Atualizada",
                "Descrição atualizada",
                DateTime.Now.AddDays(5),
                Status.Concluida,
                Prioridade.Media
            );

            var projeto = new Projeto { Id = 1 };
            var tarefa = new Tarefa { Id = command.Id, ProjetoId = command.ProjetoId };

            _mockRepositoryProjeto.Setup(repo => repo.GetById(command.ProjetoId)).Returns(projeto);
            _mockRepository.Setup(repo => repo.GetById(command.Id)).Returns(tarefa);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            _mockRepository.Verify(repo => repo.Update(tarefa), Times.Once);
            _mockRepositoryHistorico.Verify(repo => repo.Save(It.IsAny<TarefaHistoricoAtualizacao>()), Times.Once);
        }

        [Fact]
        public async Task Handle_CriarTarefa_ErroProjetoNaoEncontrado()
        {
            // Arrange
            var command = new TarefaCriarComand
            (
                1,
                1,
                "Nova Tarefa",
                "Descrição da Tarefa",
                DateTime.Now.AddDays(10),
                Status.Pendente,
                Prioridade.Alta
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            _mockNotificationContext.Verify(nc => nc.AddNotification("ProjetoId", "Projeto não encontrado!"), Times.Once);
        }

        [Fact]
        public async Task Handle_CriarTarefa_ErroLimiteTarefas()
        {
            // Arrange
            var command = new TarefaCriarComand
            (
                1,
                1,
                "Nova Tarefa",
                "Descrição da Tarefa",
                DateTime.Now.AddDays(10),
                Status.Pendente,
                Prioridade.Alta
            );
            var projeto = new Projeto { Id = 1, Tarefas = new List<Tarefa>(new Tarefa[20]) }; // Limite de 20 tarefas

            _mockRepositoryProjeto.Setup(repo => repo.GetById(command.ProjetoId)).Returns(projeto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            _mockNotificationContext.Verify(nc => nc.AddNotification("Tarefas", "Cada projeto tem um limite máximo de 20 tarefas!"), Times.Once);
        }
    }

}