using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entity
{
    public class TarefaHistoricoAtualizacao: EntityBase
    {
        [ForeignKey("Tarefa")]
        public int TarefaId { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public DateTime DataModificacao { get; set; } 
        public string Log { get; set; }
    }
}
