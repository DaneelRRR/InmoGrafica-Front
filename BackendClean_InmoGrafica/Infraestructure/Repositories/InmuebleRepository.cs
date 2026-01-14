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
    public class InmuebleRepository : IInmuebleRepository
    {
        private readonly ApplicationDbContext _context;

        public InmuebleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Inmueble inmueble)
        {
            await _context.Inmuebles.AddAsync(inmueble);
        }

        public async Task<IEnumerable<Inmueble>> GetAllAsync()
        {
            return await _context.Inmuebles
                .Include(i => i.Fotos)
                    .ThenInclude(f => f.Ambiente) 
                .ToListAsync();
        }

        public async Task<Inmueble> GetByIdAsync(int id)
        {
            return await _context.Inmuebles
                .Include(i => i.Fotos)
                    .ThenInclude(f => f.Ambiente)
                .Include(i => i.Fotos)
                    .ThenInclude(f => f.Usuario)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(Inmueble inmueble)
        {
            _context.Inmuebles.Update(inmueble);
        }
    }
}
