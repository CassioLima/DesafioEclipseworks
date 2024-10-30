using Domain;
using Domain.Entity;
using Flunt.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Command
{
    public class TarefaCommandHandler : Notifiable<Notification>,
                                        IRequest<CommandResult>,
                                        IRequestHandler<TarefaCriarComand, CommandResult>,
                                        IRequestHandler<TarefaRemoverProjetoComand, CommandResult>,
                                        IRequestHandler<TarefaAtualizarComand, CommandResult>,
                                        IRequestHandler<TarefaAdicionarProjetoComand, CommandResult>,
                                        IRequestHandler<TarefaAdicionarComentarioComand, CommandResult>


    {
        private readonly INotificationContext _notificationContext;
        private readonly IRepositoryBase<Tarefa> repository;
        private readonly IRepositoryBase<Projeto> repositoryProjeto;
        private readonly IRepositoryBase<TarefaHistoricoAtualizacao> repositoryTarefaHistoricoAtualizacao;
        private readonly IRepositoryBase<TarefaComentario> repositoryTarefaComentario;

        public TarefaCommandHandler(INotificationContext notificationContext, IRepositoryBase<Tarefa> repository, IRepositoryBase<Projeto> repositoryProjeto, IRepositoryBase<TarefaHistoricoAtualizacao> repositoryTarefaHistoricoAtualizacao, IRepositoryBase<TarefaComentario> repositoryTarefaComentario)
        {
            _notificationContext = notificationContext;
            this.repository = repository;
            this.repositoryProjeto = repositoryProjeto;
            this.repositoryTarefaHistoricoAtualizacao = repositoryTarefaHistoricoAtualizacao;
            this.repositoryTarefaComentario = repositoryTarefaComentario;
        }

        public async Task<CommandResult> Handle(TarefaCriarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid == null)
            {
                _notificationContext.AddNotification(request.Notifications);
                return new CommandResult();
            }

            var projeto = repositoryProjeto.GetById((int)request.ProjetoId);
            if (projeto == null)
            {
                _notificationContext.AddNotification("ProjetoId", "Projeto não encontrado!");
                return new CommandResult();
            }

            if (projeto.Tarefas.Count == 20)
            {
                _notificationContext.AddNotification("Tarefas", "Cada projeto tem um limite máximo de 20 tarefas!");
                return new CommandResult();
            }

            Tarefa tarefa = new Tarefa(request.Prioridade);
            tarefa.DataVencimento = request.DataVencimento;
            tarefa.Descricao = request.Descricao;
            tarefa.ProjetoId = request.ProjetoId;
            tarefa.Status = request.Status;
            tarefa.Titulo = request.Titulo;
            tarefa.UsuarioId = request.UsuarioId;

            this.repository.Save(tarefa);

            return new CommandResult(true, "", null);
        }

        public async Task<CommandResult> Handle(TarefaRemoverProjetoComand request, CancellationToken cancellationToken)
        {

            this.repository.Delete(request.Id);

            return new CommandResult(true, "", null);
        }

        public async Task<CommandResult> Handle(TarefaAdicionarComentarioComand request, CancellationToken cancellationToken)
        {
            TarefaComentario comentario = new TarefaComentario()
            {
                TarefaId = request.TarefaId,
                UsuarioId = request.UsuarioId,
                Comententario = request.Comententario
            };
            this.repositoryTarefaComentario.Save(comentario);

            return new CommandResult(true, "", null);
        }

        public async Task<CommandResult> Handle(TarefaAtualizarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid == null)
            {
                _notificationContext.AddNotification(request.Notifications);
                return new CommandResult();
            }

            var projeto = repositoryProjeto.GetById((int)request.ProjetoId);
            if (projeto == null)
            {
                _notificationContext.AddNotification("ProjetoId", "Projeto não encontrado!");
                return new CommandResult();
            }

            Tarefa tarefa = repository.GetById(request.Id);
            tarefa.DataVencimento = request.DataVencimento;
            tarefa.Descricao = request.Descricao;
            tarefa.ProjetoId = request.ProjetoId;
            tarefa.Status = request.Status;
            tarefa.Titulo = request.Titulo;
            tarefa.UsuarioId = request.UsuarioId;
            this.repository.Update(tarefa);


            //Implementação do Log
            TarefaHistoricoAtualizacao tarefaHistorico = new TarefaHistoricoAtualizacao()
            {
                TarefaId = tarefa.Id,
                DataModificacao = DateTime.Now,
                UsuarioId = request.UsuarioId,
                Log = string.Join(", ", tarefa.ObterPropriedadesModificadas().Select(x => $"Propriedade alterada: {x.Key}, Novo valor: {x.Value}"))
            };
            this.repositoryTarefaHistoricoAtualizacao.Save(tarefaHistorico);

            return new CommandResult(true, "", null);
        }

        public async Task<CommandResult> Handle(TarefaAdicionarProjetoComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid == null)
            {
                _notificationContext.AddNotification(request.Notifications);
                return new CommandResult();
            }

            var projeto = repositoryProjeto.GetById((int)request.ProjetoId);
            if (projeto == null)
            {
                _notificationContext.AddNotification("ProjetoId", "Projeto não encontrado!");
                return new CommandResult();
            }

            if (projeto.Tarefas.Count == 20)
            {
                _notificationContext.AddNotification("Tarefas", "Cada projeto tem um limite máximo de 20 tarefas!");
                return new CommandResult();
            }

            Tarefa tarefa = new Tarefa(request.Prioridade);
            tarefa.DataVencimento = request.DataVencimento;
            tarefa.Descricao = request.Descricao;
            tarefa.ProjetoId = request.ProjetoId;
            tarefa.Status = request.Status;
            tarefa.Titulo = request.Titulo;
            tarefa.UsuarioId = request.UsuarioId;

            this.repository.Save(tarefa);

            return new CommandResult(true, "", null);
        }
    }
}
