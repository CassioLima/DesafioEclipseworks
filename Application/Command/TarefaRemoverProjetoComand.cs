using Enums;
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
    public class TarefaRemoverProjetoComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public TarefaRemoverProjetoComand(int tarefaId)
        {
            Id = tarefaId;

            AddNotifications(new Contract<TarefaRemoverProjetoComand>()
                .Requires()
                .IsGreaterThan(tarefaId, 0, "TarefaId", "Id não informado!")
            );

        }

        public int Id { get; set; }
    }
}
