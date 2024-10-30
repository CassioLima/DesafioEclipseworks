using MediatR;
using Domain.Entity;
using Enums;

namespace Application
{
    public class UsuarioQuery : IRequest<CommandResult>
    {
        public UsuarioQuery()
        {

        }
    }
    public class UsuarioResult
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty;


        public static UsuarioResult Map(Usuario usuario)
        {
            UsuarioResult result = new();
            result.Id = usuario.Id.ToString();
            result.Nome = usuario.Nome;
            result.Funcao = usuario.Funcao.ToString();
            return result;
        }


    }
}
