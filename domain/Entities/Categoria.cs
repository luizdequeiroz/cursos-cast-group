using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace domain.Entities
{
    public class Categoria : Entity
    {
        [NotMapped]
        public int Codigo
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
            }
        }
        public string Descricao { get; set; }

        public IEnumerable<Curso> Cursos { get; set; }
    }
}
