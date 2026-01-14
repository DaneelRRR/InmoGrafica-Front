using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IInmuebleRepository Inmuebles { get; }
        IFotoRepository Fotos { get; }
        IUsuarioRepository Usuarios { get; }
        IRolRepository Roles { get; }
        IAmbienteRepository Ambientes { get; }

        Task<int> SaveChangesAsync();
    }
}
