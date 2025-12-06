using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityWithAngular.Core.Classes;
using IdentityWithAngular.Core.interfaces;

namespace IdentityWithAngular.Application.Services
{
    public class BibliotecarioService 
    {
        private readonly IRepositorioLivros _repoLivros;
        private readonly IRepositorioUsuarios _repoUsuarios;
        private readonly IRepositorioEmprestimos _repoEmprestimos;
        // Supondo uma Unidade de Trabalho ou padrão similar para salvar as alterações.
        // Se seus repositórios salvam as alterações individualmente, você pode remover isso.
        // Por enquanto, vou assumir que cada chamada de atualização do repositório salva a si mesma.

        // Construtor corrigido
        public BibliotecarioService(
            IRepositorioLivros repoLivros,
            IRepositorioUsuarios repoUsuarios,
            IRepositorioEmprestimos repoEmprestimos)
        {
            _repoLivros = repoLivros;
            _repoUsuarios = repoUsuarios;
            _repoEmprestimos = repoEmprestimos;
        }
        
        // Corrigido para ser assíncrono e usar await
        public async Task<Emprestimo> EmprestarAsync(int livroId, string usuarioId)
        {
            var livro = await _repoLivros.GetLivroByIdAsync(livroId) 
                ?? throw new Exception("Livro não encontrado");
            
        // Supondo que você adicionará lógica de usuário posteriormente. Por enquanto, apenas verificamos a existência.
            var usuario = await _repoUsuarios.GetUsuarioByIdAsync(usuarioId) 
                ?? throw new Exception("Usuário não encontrado");
    
            livro.RetirarExemplar(); // Use o método na entidade Livros

            var emprestimo = new Emprestimo { LivroId = livroId, DataEmprestimo = DateTime.UtcNow };
            await _repoEmprestimos.AddEmprestimoAsync(emprestimo);
            await _repoLivros.UpdateLivroAsync(livro); // Manter a alteração na quantidade de livros

            return emprestimo;
        }

        public async Task DevolverAsync(int emprestimoId)
        {
            var emprestimo = await _repoEmprestimos.GetEmprestimoByIdAsync(emprestimoId)
                ?? throw new Exception("Empréstimo não encontrado");
            emprestimo.DataDevolucao = DateTime.UtcNow;
            await _repoEmprestimos.UpdateEmprestimoAsync(emprestimo);
        }
    }
}