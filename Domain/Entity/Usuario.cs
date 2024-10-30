using Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; } = string.Empty;
        public Funcao Funcao { get; set; } = Funcao.Comum;
    }
}
