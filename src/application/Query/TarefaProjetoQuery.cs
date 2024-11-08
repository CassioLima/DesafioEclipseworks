using MediatR;
using Domain;
using Domain.Entity;
using Enums;

namespace Application
{
    public class TarefaProjetoQuery : IRequest<CommandResult>
    {
        public TarefaProjetoQuery()
        {

        }

        public TarefaProjetoQuery(int idProjeto)
        {
            IdProjeto = idProjeto;
        }

        public int IdProjeto { get; set; }
    }
    public class TarefaProjetoResult
    {
        public string Id { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataVencimento { get; set; }
        public string Status { get; set; }
        public string Prioridade { get; set; }
        public IEnumerable<TarefaComentario>? Comentarios { get; set; }

        public static TarefaProjetoResult Map(Tarefa tarefa)
        {
            TarefaProjetoResult result = new();
            result.Id = tarefa.Id.ToString();
            result.Titulo = tarefa.Titulo;
            result.Descricao = tarefa.Descricao;
            result.DataVencimento = tarefa.DataVencimento;
            result.Status = tarefa.Status.ToString();
            result.Prioridade = tarefa.Prioridade.ToString();
            result.Comentarios = tarefa.Comentarios;
            return result;
        }

        public static List<TarefaProjetoResult> Map(List<Tarefa> tarefas)
        {
            List<TarefaProjetoResult> Listresult = new();

            foreach (var item in tarefas)
            {
                TarefaProjetoResult result = new();
                result.Id = item.Id.ToString();
                result.Titulo = item.Titulo;
                result.Descricao = item.Descricao;
                result.DataVencimento = item.DataVencimento;
                result.Status = item.Status.ToString();
                result.Prioridade = item.Prioridade.ToString();
                result.Comentarios = item.Comentarios;
                Listresult.Add(result);
            }

            return Listresult;
        }

    }
}
