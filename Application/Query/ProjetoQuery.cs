using MediatR;
using Domain.Entity;

namespace Application
{
    public class ProjetoQuery : IRequest<CommandResult>
    {
        public ProjetoQuery()
        {

        }
    }
    public class ProjetoResult
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public ICollection<TarefaResult>? Tarefas { get; set; }


        public static ProjetoResult Map(Projeto projeto)
        {
            ProjetoResult result = new();
            result.Id = projeto.Id.ToString();
            result.Nome = projeto.Nome;
            result.Usuario = projeto.Usuario.Nome;
            result.Tarefas = TarefaResult.Map(projeto.Tarefas?.ToList());
            return result;
        }


    }
}
