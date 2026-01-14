using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUsuarioRepository Usuarios { get; }
        public IInmuebleRepository Inmuebles { get; }
        public IFotoRepository Fotos { get; }
        public IRolRepository Roles { get; }
        public IAmbienteRepository Ambientes { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Usuarios = new UsuarioRepository(_context);
            Inmuebles = new InmuebleRepository(_context);
            Fotos = new FotoRepository(_context);
            Roles = new RolRepository(_context);
            Ambientes = new AmbienteRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
