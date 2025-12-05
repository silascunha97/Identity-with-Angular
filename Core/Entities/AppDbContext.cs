using IdentityWithAngular.Core.Classes;
using IdentityWithAngular.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityWithAngular.Core.Entities
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
        public DbSet<Livros> Livros { get; set; } 
        public DbSet<Emprestimo> Emprestimos { get; set; } 
    

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Livros>()
                .HasMany(l => l.Emprestimos) // Livro has many Emprestimos
                .WithOne(e => e.Livro) // Emprestimo has one Livro
                .HasForeignKey(e => e.LivroId) // FK in Emprestimo
                .OnDelete(DeleteBehavior.Restrict); // Optional: specify delete behavior
        }
    
    }
}