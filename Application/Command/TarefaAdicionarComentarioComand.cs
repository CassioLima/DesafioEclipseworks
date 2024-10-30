using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Command
{
    public class TarefaAdicionarComentarioComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public TarefaAdicionarComentarioComand(int idTarefa, int idUsuario, String comententario)
        {
            TarefaId = idTarefa;
            UsuarioId = idUsuario;
            Comententario = comententario;
        }

        public int TarefaId { get; set; }
        public int UsuarioId { get; set; }
        public string Comententario { get; set; }
    }
}
