using Eventify.Core.Entities;
using Eventify.Core.Interfaces.Repositories;
using Eventify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Infrastructure.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly EventifyDbContext _context;
        public EstadoRepository(EventifyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Estado>> GetEstados()
        {
            return await _context.Estados.ToListAsync();
        }
    }
}
