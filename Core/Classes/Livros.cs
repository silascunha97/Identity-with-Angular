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
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public GeneroLivros Genero { get; set; }
        public int QtdLivros { get; set; }
        
        public ICollection<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();
    }
}