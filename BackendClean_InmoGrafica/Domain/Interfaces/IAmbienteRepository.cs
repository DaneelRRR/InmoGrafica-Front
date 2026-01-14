using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAmbienteRepository
    {
        Task<Ambiente> GetByIdAsync(int id);

        Task<IEnumerable<Ambiente>> GetAllAsync();
    }
}