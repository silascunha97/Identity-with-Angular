using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityWithAngular.Core.Classes
{
    public class Discente
    {
        
        public int Id { get; set; }
        public string? UserId { get; set; } = string.Empty;
        // FK para IdentityUser
        public IdentityUser? User { get; set; } = null!;

        public int Semestre { get; set; }
        public int IdTurma { get; set; }
        public DateTime AnoNasc { get; set; }

        // Regras do aluno
        public bool PodePegarLivro => IdTurma > 0;

    }
}