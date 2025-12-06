using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityWithAngular.Core.interfaces
{
    public interface IRepositorioUsuarios
    {
        Task<IEnumerable<IdentityUser>> GetAllUsuariosAsync();
        Task<IdentityUser?> GetUsuarioByIdAsync(string id);
        Task AddUsuarioAsync(IdentityUser usuario);
        Task UpdateUsuarioAsync(IdentityUser usuario);
        Task DeleteUsuarioAsync(string id);
    }
}