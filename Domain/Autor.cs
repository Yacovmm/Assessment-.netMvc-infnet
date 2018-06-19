using System.Collections.Generic;

namespace Domain
{
    public class Autor
    {
        public Autor()
        {
            Livros = new List<Livro>();
        }

        public int AutorId { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string DataNascimento { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }

    }
}