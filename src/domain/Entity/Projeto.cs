using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Projeto : EntityBase
    {
        public string Nome { get; set; } = default!;
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public ICollection<Tarefa>? Tarefas { get; set; }
        public Usuario Usuario { get; set; }
    }
}
