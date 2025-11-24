using Eventify.Core.Entities;
using Eventify.Core.Interfaces.Repositories;
using Eventify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Infrastructure.Repositories
{
    public class CategoriasIngressoRepository : ICategoriasIngressoRepository
    {
        private readonly EventifyDbContext _context;

        public CategoriasIngressoRepository(EventifyDbContext context)
        {
            _context = context;
        }

        public async Task<CategoriaIngresso?> GetById(Guid id)
        {
            return await _context.CategoriasIngresso
                .Include(c => c.Evento)
                .Include(c => c.Ingressos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<CategoriaIngresso>> GetAll()
        {
            return await _context.CategoriasIngresso
                .Include(c => c.Evento)
                .Include(c => c.Ingressos)
                .ToListAsync();
        }

        public async Task<List<CategoriaIngresso>> GetByEvent(Guid eventoId)
        {
            return await _context.CategoriasIngresso
                .Include(c => c.Evento)
                .Include(c => c.Ingressos)
                .Where(c => c.EventoId == eventoId)
                .ToListAsync();
        }

        public async Task Salvar(CategoriaIngresso categoria)
        {
            var categoriaExistente = await _context.CategoriasIngresso.FindAsync(categoria.Id);

            if (categoriaExistente == null)
            {
                await _context.CategoriasIngresso.AddAsync(categoria);
            }
            else
            {
                _context.Entry(categoriaExistente).CurrentValues.SetValues(categoria);
            }
        }

        public async Task Remover(Guid id)
        {
            var categoria = await _context.CategoriasIngresso.FindAsync(id);
            if (categoria != null)
            {
                _context.CategoriasIngresso.Remove(categoria);
            }
        }
    }
}