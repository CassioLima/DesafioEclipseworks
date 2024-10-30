using MediatR;
using Domain;
using Domain.Entity;
using Flunt.Notifications;

namespace Application
{
    public class ProjetoHandler : Notifiable<Notification>, 
                                  IRequestHandler<ProjetoQuery, CommandResult>,
                                  IRequestHandler<ProjetoUsuarioQuery, CommandResult>
    {
        private readonly IRepositoryBase<Projeto> repository;
        private readonly IRepositoryBase<Tarefa> repositoryTarefa;
        private readonly IRepositoryBase<Usuario> repositoryUsuario;
        public ProjetoHandler(IRepositoryBase<Projeto> repository, IRepositoryBase<Tarefa> repositoryTarefa, IRepositoryBase<Usuario> repositoryUsuario)
        {
            this.repository = repository;
            this.repositoryTarefa = repositoryTarefa;
            this.repositoryUsuario = repositoryUsuario;
        }

        public async Task<CommandResult> Handle(ProjetoUsuarioQuery request, CancellationToken cancellationToken)
        {
            var result = new List<ProjetoResult>();

            var projetos = repository.GetAll().Where(x => x.UsuarioId == request.IdUsuario);

            foreach (var projeto in projetos)
            {
                var tarefas = repositoryTarefa.GetAll().Where(x => x.ProjetoId == projeto.Id).ToList();
                var usuario = repositoryUsuario.GetById(projeto.UsuarioId);

                projeto.Tarefas = tarefas;
                projeto.Usuario = usuario;
                ProjetoResult view = ProjetoResult.Map(projeto);
                result.Add(view);
            }
            return new CommandResult(true, null, result);
        }

        public async Task<CommandResult> Handle(ProjetoQuery request, CancellationToken cancellationToken)
        {
            var result = new List<ProjetoResult>();

            var projetos = repository.GetAll();
            foreach (var projeto in projetos)
            {
                var tarefas = repositoryTarefa.GetAll().Where(x => x.ProjetoId == projeto.Id).ToList();
                var usuario = repositoryUsuario.GetById(projeto.UsuarioId);

                projeto.Tarefas = tarefas;
                projeto.Usuario = usuario;
                ProjetoResult view = ProjetoResult.Map(projeto);
                result.Add(view);
            }
            return new CommandResult(true, null, result);
        }
    }
}