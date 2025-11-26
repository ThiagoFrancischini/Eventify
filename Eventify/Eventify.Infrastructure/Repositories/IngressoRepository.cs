using Eventify.Core.Entities;
using Eventify.Core.Filtros;
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

        public async Task<Ingresso?> GetByCodigo(string codigo)
        {
            return await _context.Ingressos
                .Include(i => i.Evento)
                .Include(i => i.UsuarioCompra)
                .Include(i => i.CategoriaIngresso)
                .FirstOrDefaultAsync(i => i.Codigo.ToUpper() == codigo.ToUpper());
        }

        public async Task<List<Ingresso>> GetAll(FiltroIngresso filtro)
        {
            var query = _context.Ingressos
                .Include(i => i.Evento)
                    .ThenInclude(e => e.Endereco)
                .Include(i => i.UsuarioCompra)
                .Include(i => i.CategoriaIngresso)
                .AsQueryable();

            if (filtro.EventoId.HasValue)
            {
                query = query.Where(x => x.EventoId == filtro.EventoId.Value);
            }

            if (filtro.UsuarioCompraId.HasValue)
            {
                query = query.Where(x => x.UsuarioCompraId == filtro.UsuarioCompraId.Value);
            }

            if (filtro.CategoriaIngressoId.HasValue)
            {
                query = query.Where(x => x.CategoriaIngressoId == filtro.CategoriaIngressoId.Value);
            }

            if (filtro.Valido.HasValue)
            {
                query = query.Where(x => x.Valido == filtro.Valido.Value);
            }

            if (filtro.DataCompraInicio.HasValue)
            {
                query = query.Where(x => x.DataCompra >= filtro.DataCompraInicio.Value);
            }

            if (filtro.DataCompraFim.HasValue)
            {
                query = query.Where(x => x.DataCompra <= filtro.DataCompraFim.Value);
            }

            if (!string.IsNullOrEmpty(filtro.Codigo))
            {
                query = query.Where(x => x.Codigo.Contains(filtro.Codigo));
            }

            if (filtro.Usado.HasValue)
            {
                if (filtro.Usado.Value)
                {
                    query = query.Where(x => x.DataUso != null);
                }
                else
                {
                    query = query.Where(x => x.DataUso == null);
                }
            }

            if (filtro.OrderByDataCompraDesc)
            {
                query = query.OrderByDescending(x => x.DataCompra);
            }
            else if (filtro.OrderByDataCompra)
            {
                query = query.OrderBy(x => x.DataCompra);
            }
            else
            {
                query = query.OrderByDescending(x => x.DataCompra);
            }

            return await query.ToListAsync();
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