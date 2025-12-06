using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityWithAngular.Core.Classes;

namespace IdentityWithAngular.Core.interfaces
{
    public interface IRepositorioEmprestimos
    {
        Task<IEnumerable<Emprestimo>> GetAllEmprestimosAsync();
        Task<Emprestimo?> GetEmprestimoByIdAsync(int id);
        Task AddEmprestimoAsync(Emprestimo emprestimo);
        Task UpdateEmprestimoAsync(Emprestimo emprestimo);
        Task DeleteEmprestimoAsync(int id);
    }
}