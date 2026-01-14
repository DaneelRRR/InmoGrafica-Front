using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFotoRepository
    {
        Task<Foto> GetByIdAsync(int id);

        Task AddAsync(Foto foto);

        void Update(Foto foto);

        Task<IEnumerable<Foto>> GetByInmuebleIdAsync(int inmuebleId);

        Task<IEnumerable<Foto>> GetByInmuebleYEstadoAsync(int inmuebleId, bool esEditada);
    }
}
