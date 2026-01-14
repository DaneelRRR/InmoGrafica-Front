using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByIdAsync(int id);

        Task<Usuario> GetByEmailAsync(string email);

        Task AddAsync(Usuario usuario);

        Task<bool> ExistsByEmailAsync(string email);
    }
}
