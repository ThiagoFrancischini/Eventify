using Eventify.Core.Entities;
using Eventify.Core.Interfaces.Repositories;
using Eventify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Infrastructure.Repositories
{
    public class IngressoRepository : IIngressoRepository
    {
        private readonly EventifyDbContext _context;

        public IngressoRepository(EventifyDbContext context)
        {
            _context = context;
        }

        public async Task<Ingresso?> GetById(Guid id)
        {
            return await _context.Ingressos
                .Include(i => i.Evento)
                .Include(i => i.UsuarioCompra)
                .Include(i => i.CategoriaIngresso)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Ingresso>> GetAll()
        {
            return await _context.Ingressos
                .Include(i => i.Evento)
                .Include(i => i.UsuarioCompra)
                .Include(i => i.CategoriaIngresso)
                .ToListAsync();
        }

        public async Task<List<Ingresso>> GetByEvent(Guid eventoId)
        {
            return await _context.Ingressos
                .Include(i => i.Evento)
                .Include(i => i.UsuarioCompra)
                .Include(i => i.CategoriaIngresso)
                .Where(i => i.EventoId == eventoId)
                .ToListAsync();
        }

        public async Task<List<Ingresso>> GetByUser(Guid usuarioId)
        {
            return await _context.Ingressos
                .Include(i => i.Evento)
                .Include(i => i.UsuarioCompra)
                .Include(i => i.CategoriaIngresso)
                .Where(i => i.UsuarioCompraId == usuarioId)
                .ToListAsync();
        }

        public async Task<List<Ingresso>> GetByCategory(Guid categoriaId)
        {
            return await _context.Ingressos
                .Include(i => i.Evento)
                .Include(i => i.UsuarioCompra)
                .Include(i => i.CategoriaIngresso)
                .Where(i => i.CategoriaIngressoId == categoriaId)
                .ToListAsync();
        }

        public async Task Salvar(Ingresso ingresso)
        {
            var ingressoExistente = await _context.Ingressos.FindAsync(ingresso.Id);

            if (ingressoExistente == null)
            {
                await _context.Ingressos.AddAsync(ingresso);
            }
            else
            {
                _context.Entry(ingressoExistente).CurrentValues.SetValues(ingresso);
            }
        }

        public async Task Remover(Guid id)
        {
            var ingresso = await _context.Ingressos.FindAsync(id);
            if (ingresso != null)
            {
                _context.Ingressos.Remove(ingresso);
            }
        }
    }
}