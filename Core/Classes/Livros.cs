using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityWithAngular.Core.Enums;

namespace IdentityWithAngular.Core.Classes
{
    public class Livros
    {

        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public GeneroLivros Genero { get; set; }
        public int QtdLivros { get; set; } // total de livros disponíveis
        
         public int QuantidadeDisponivel { get; private set; }

        public ICollection<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();


         public void RegistrarExemplar() { /* se usar exemplar físico */ }

        public bool EstaDisponivel() => QuantidadeDisponivel > 0;

        public void RetirarExemplar()
        {
            if (!EstaDisponivel())
                throw new InvalidOperationException("Livro indisponível");
            QuantidadeDisponivel--;
        }

        public void DevolverExemplar()
        {
            QuantidadeDisponivel++;
        }

    }
}