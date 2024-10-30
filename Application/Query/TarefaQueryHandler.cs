using MediatR;
using Domain;
using Domain.Entity;
using Flunt.Notifications;

namespace Application
{
    public class TarefaHandler : Notifiable<Notification>,
                                 IRequestHandler<TarefaQuery, CommandResult>,
                                 IRequestHandler<TarefaProjetoQuery, CommandResult>,
                                 IRequestHandler<TarefaRelatorioQuery, CommandResult>
    {
        private readonly IRepositoryBase<Tarefa> repository;
        private readonly IRepositoryBase<Usuario> repositoryUsuario;
        private readonly INotificationContext _notificationContext;

        public TarefaHandler(IRepositoryBase<Tarefa> repository, IRepositoryBase<Usuario> repositoryUsuario, INotificationContext notificationContext)
        {
            this.repository = repository;
            this.repositoryUsuario = repositoryUsuario;
            _notificationContext = notificationContext;
        }

        public async Task<CommandResult> Handle(TarefaQuery request, CancellationToken cancellationToken)
        {
            var result = new List<TarefaResult>();

            var tarefas = repository.GetAll();

            foreach (var tarefa in tarefas)
            {
                TarefaResult view = TarefaResult.Map(tarefa);
                result.Add(view);
            }
            return new CommandResult(true, null, result);
        }

        public async Task<CommandResult> Handle(TarefaProjetoQuery request, CancellationToken cancellationToken)
        {
            var result = new List<TarefaProjetoResult>();

            var tarefas = repository.GetAll().Where(x => x.ProjetoId == request.IdProjeto).ToList();

            foreach (var tarefa in tarefas)
            {
                TarefaProjetoResult view = TarefaProjetoResult.Map(tarefa);
                result.Add(view);
            }
            return new CommandResult(true, null, result);
        }

        public async Task<CommandResult> Handle(TarefaRelatorioQuery request, CancellationToken cancellationToken)
        {
            //Verifica se o usuário é gerente
            if (request.IdUsuario > 0)
            {
                var usuario = repositoryUsuario.GetById(request.IdUsuario);

                if (usuario.Funcao != Enums.Funcao.Gerente)
                {
                    _notificationContext.AddNotification("Usuario", $"O Usuário {usuario.Nome} não tem a função de gerente!");
                    return new CommandResult();
                }
            }
            else
            {
                _notificationContext.AddNotification("IdUsuario", "Usuário não informado");
                return new CommandResult();
            }


            var tarefas = repository.GetAll().Where(x => x.UsuarioId == request.IdUsuario 
                        && x.DataVencimento >= DateTime.Now.AddDays(-30) 
                        && x.Status == Enums.Status.Concluida ).ToList();

            var result = new TarefaRelatorioResult()
            {
                Usuario = this.repositoryUsuario.GetById(request.IdUsuario).Nome,
                TarefasComcluidas = tarefas.Count()
            };

            return new CommandResult(true, null, result);
        }
    }
}