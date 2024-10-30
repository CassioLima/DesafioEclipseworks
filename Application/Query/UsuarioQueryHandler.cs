using MediatR;
using Domain;
using Domain.Entity;

namespace Application
{
    public class UsuarioHandler : IRequestHandler<UsuarioQuery, CommandResult>
    {
        private readonly IRepositoryBase<Usuario> repository;
        public UsuarioHandler(IRepositoryBase<Usuario> repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(UsuarioQuery request, CancellationToken cancellationToken)
        {
            var result = new List<UsuarioResult>();
            
            var usuarios = repository.GetAll();

            foreach (var usuario in usuarios)
            {
                UsuarioResult view = UsuarioResult.Map(usuario);
                result.Add(view);
            }
            return new CommandResult(true, null, result);
        }
    }
}