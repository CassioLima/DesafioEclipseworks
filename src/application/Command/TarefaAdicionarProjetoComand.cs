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
    public class TarefaAdicionarProjetoComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public TarefaAdicionarProjetoComand(int projetoId, int usuarioId, string titulo, string descricao, DateTime dataVencimento, Status status, Prioridade prioridade)
        {
            ProjetoId = projetoId;
            UsuarioId = usuarioId;
            Titulo = titulo;
            Descricao = descricao;
            DataVencimento = dataVencimento;
            Status = status;
            Prioridade = prioridade;

            AddNotifications(new Contract<TarefaAdicionarProjetoComand>()
                .Requires()
                .IsGreaterThan(ProjetoId, 0, "ProjetoId", "Projeto não informado!")
                .IsGreaterThan(UsuarioId, 0, "UsuarioId", "Usuário não informado!")
                .IsNullOrWhiteSpace(Titulo, "Titulo", "Título não informado!")
                .IsNullOrWhiteSpace(Descricao, "Descricao", "Descrição não informada!")
                .IsMinValue(DataVencimento, "DataVencimento", "Data de vencimento não informada!")

            );

        }

        public int ProjetoId { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; } 
        public string Descricao { get; set; } 
        public DateTime DataVencimento { get; set; }
        public Status Status { get; set; }
        public Prioridade Prioridade { get; set; }
    }
}
