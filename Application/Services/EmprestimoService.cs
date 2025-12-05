using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityWithAngular.Core.Classes;
using IdentityWithAngular.Core.Entities;

namespace IdentityWithAngular.Application.Services
{
    public class EmprestimoService
    {
        private readonly AppDbContext _context;

        public EmprestimoService(AppDbContext context)
        {
            _context = context;
        }

         public async Task<Emprestimo> CriarEmprestimo(int livroId)
        {
            var livro = await _context.Livros.FindAsync(livroId);
            if (livro == null || livro.QtdLivros <= 0)
            {
                throw new Exception("Livro não encontrado ou sem estoque disponível.");
            }

            livro.QtdLivros--; // Decrementa a quantidade de livros disponíveis

            var emprestimo = new Emprestimo 
            { 
                LivroId = livroId, 
                DataEmprestimo = DateTime.Now 
            };
            _context.Emprestimos.Add(emprestimo);
            await _context.SaveChangesAsync();

            return emprestimo;
        }
        
    }
}