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
    public class AmbienteRepository : IAmbienteRepository
    {
        private readonly ApplicationDbContext _context;

        public AmbienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ambiente>> GetAllAsync()
        {
            return await _context.Ambientes.ToListAsync();
        }

        public async Task<Ambiente> GetByIdAsync(int id)
        {
            return await _context.Ambientes.FindAsync(id);
        }
    }
}