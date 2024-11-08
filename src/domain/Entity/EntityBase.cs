using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public bool IsNew() { return Id == 0; }
    }
}
