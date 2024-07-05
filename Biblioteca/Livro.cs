using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Livro
    {
        private int id;
        private String nome_livro;
        private String autor_livro;
        private DateTime data_publicação;

        public Livro()
        {
            this.id = 0;
            this.nome_livro = "";
            this.autor_livro = "";
            this.data_publicação = DateTime.Now;
        }

        public int Id { get => id; set => id = value; }
        public string Nome_livro { get => nome_livro; set => nome_livro = value; }
        public string Autor_livro { get => autor_livro; set => autor_livro = value; }
        public DateTime Data_publicação { get => data_publicação; set => data_publicação = value; }
    }
}
