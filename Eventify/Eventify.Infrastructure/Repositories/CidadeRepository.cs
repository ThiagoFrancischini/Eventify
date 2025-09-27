using Eventify.Core.Entities;
using Eventify.Core.Interfaces.Repositories;
using Eventify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Infrastructure.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly EventifyDbContext _context;

        public CidadeRepository(EventifyDbContext context)
        {
            _context = context;
        }

        public async Task<Cidade?> GetById(Guid id)
        {
            return await _context.Cidades
                .Include(c => c.Estado)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Cidade>> GetCidadesPorEstado(Guid estadoId)
        {
            return await _context.Cidades
                .Where(c => c.EstadoId == estadoId)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task Salvar(Cidade cidade)
        {
            var existingCity = await _context.Cidades.FindAsync(cidade.Id);

            if (existingCity == null)
            {
                await _context.Cidades.AddAsync(cidade);
            }
            else
            {
                _context.Entry(existingCity).CurrentValues.SetValues(cidade);
            }
        }
    }
}
