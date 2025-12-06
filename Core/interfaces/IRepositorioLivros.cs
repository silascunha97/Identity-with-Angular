using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityWithAngular.Core.Classes;

namespace IdentityWithAngular.Core.interfaces
{
    public interface IRepositorioLivros
    {
        Task<IEnumerable<Livros>> GetAllLivrosAsync();
        Task<Livros?> GetLivroByIdAsync(int id);
        Task AddLivroAsync(Livros livro);
        Task UpdateLivroAsync(Livros livro);
        Task DeleteLivroAsync(int id);
    }
}