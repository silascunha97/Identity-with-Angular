using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithAngular.Core.Classes
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }

    // FK
        public int LivroId { get; set; }

    // Navegação
        public Livros Livro { get; set; } = null!;
    }
}