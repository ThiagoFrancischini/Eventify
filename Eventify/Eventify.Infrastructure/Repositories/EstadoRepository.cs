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
            return await _context
                .Estados
                .OrderBy(x => x.Sigla)
                .ToListAsync();
        }

        public async Task<Estado?> GetById(Guid id)
        {
            return await _context.Estados.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Salvar(Estado estado)
        {
            var existingState = await _context.Estados.FindAsync(estado.Id);

            if (existingState == null)
            {
                await _context.Estados.AddAsync(estado);
            }
            else
            {
                _context.Entry(existingState).CurrentValues.SetValues(estado);
            }
        }
    }
}
