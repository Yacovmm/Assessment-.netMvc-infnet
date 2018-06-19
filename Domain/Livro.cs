using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Livro
    {
        public int LivroId { get; set; }

        public string Titulo { get; set; }

        public string Isbn { get; set; }

        public int Ano { get; set; }

        public int AutorId { get; set; }

        public virtual Autor Autor { get; set; }

        public virtual ICollection<Autor> Autors { get; set; }

        

    }
}
