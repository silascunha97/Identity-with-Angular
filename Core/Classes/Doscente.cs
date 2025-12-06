using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityWithAngular.Classes
{
    public class Doscente
    {
        public int Id { get; set; }
        public string? UserId { get; set; } = null!;
        // FK para IdentityUser
        public IdentityUser? User { get; set; } = null!;

        public DateTime HorarioTrabalho { get; set; }
        
    }
}