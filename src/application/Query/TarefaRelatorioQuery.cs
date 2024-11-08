using MediatR;
using Domain;
using Domain.Entity;
using Enums;

namespace Application
{
    public class TarefaRelatorioQuery : IRequest<CommandResult>
    {
        public TarefaRelatorioQuery(){}

        public TarefaRelatorioQuery(int idUsuario)
        {
            IdUsuario = idUsuario;
        }

        public int IdUsuario { get; set; }
    }
    public class TarefaRelatorioResult
    {
        public string Usuario { get; set; } = string.Empty;
        public int TarefasComcluidas { get; set; } = 0;

    }
}

