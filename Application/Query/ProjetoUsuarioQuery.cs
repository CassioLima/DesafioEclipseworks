using MediatR;
using Domain.Entity;

namespace Application
{
    public class ProjetoUsuarioQuery : IRequest<CommandResult>
    {
        public ProjetoUsuarioQuery(){}

        public ProjetoUsuarioQuery(int idUsuario)
        {
            IdUsuario = idUsuario;
        }

        public int IdUsuario { get; set; }
    }
    public class ProjetoUsuarioResult
    {
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public ICollection<Tarefa>? Tarefas { get; set; }


        public static ProjetoUsuarioResult Map(Projeto projetoUsuario)
        {
            ProjetoUsuarioResult result = new();
            result.Nome = projetoUsuario.Nome;
            result.Tarefas = projetoUsuario.Tarefas;
            result.Usuario = projetoUsuario.Usuario?.Nome;
            return result;
        }


    }
}
