using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInmuebleRepository
    {
        Task<Inmueble> GetByIdAsync(int id);

        Task<IEnumerable<Inmueble>> GetAllAsync();

        Task AddAsync(Inmueble inmueble);

        void Update(Inmueble inmueble);
    }
}
