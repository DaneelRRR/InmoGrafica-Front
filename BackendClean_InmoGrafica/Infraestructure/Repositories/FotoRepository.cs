using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FotoRepository : IFotoRepository
    {
        private readonly ApplicationDbContext _context;

        public FotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Foto foto)
        {
            await _context.Fotos.AddAsync(foto);
        }

        public async Task<Foto> GetByIdAsync(int id)
        {
            return await _context.Fotos.FindAsync(id);
        }

        public async Task<IEnumerable<Foto>> GetByInmuebleIdAsync(int inmuebleId)
        {
            return await _context.Fotos
                .Where(f => f.InmuebleId == inmuebleId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Foto>> GetByInmuebleYEstadoAsync(int inmuebleId, bool esEditada)
        {
            return await _context.Fotos
                .Where(f => f.InmuebleId == inmuebleId && f.EsEditada == esEditada)
                .ToListAsync();
        }

        public void Update(Foto foto)
        {
            _context.Fotos.Update(foto);
        }
    }
}
