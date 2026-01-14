using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRolRepository
    {
        Task<Rol> GetByIdAsync(int id);

        Task<Rol> GetByNameAsync(string nombreRol);

        Task<IEnumerable<Rol>> GetAllAsync();
    }
}
