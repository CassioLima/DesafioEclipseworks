using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class TarefaComentario : EntityBase
    {
        [ForeignKey("Tarefa")]
        public int TarefaId { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public string Comententario { get; set; }
    }
}
