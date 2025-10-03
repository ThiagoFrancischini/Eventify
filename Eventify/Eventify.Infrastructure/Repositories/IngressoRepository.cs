using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<Ingresso>> GetIngressos()
        {
            return await _context.Ingressos
                .Include(i => i.CategoriaIngresso)
                .Include(i => i.Evento)
                .Include(i => i.UsuarioCompra)
                .ToListAsync();
        }

        public async Task<Ingresso?> GetById(Guid id)
        {
            return await _context.Ingressos
                .Include(i => i.CategoriaIngresso)
                .Include(i => i.Evento)
                .Include(i => i.UsuarioCompra)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task Salvar(Ingresso ingresso)
        {
            if (_context.Ingressos.Any(i => i.Id == ingresso.Id))
                _context.Ingressos.Update(ingresso);
            else
                _context.Ingressos.Add(ingresso);

            await _context.SaveChangesAsync();
        }

        public async Task<Ingresso?> GetByCodigo(string codigo)
        {
            return await _context.Ingressos
                .Include(i => i.CategoriaIngresso)
                .Include(i => i.Evento)
                .Include(i => i.UsuarioCompra)
                .FirstOrDefaultAsync(i => i.Codigo == codigo);
        }

        public async Task<List<Ingresso>> GetByUsuario(Guid usuarioId)
        {
            return await _context.Ingressos
                .Include(i => i.CategoriaIngresso)
                .Include(i => i.Evento)
                .Where(i => i.UsuarioCompraId == usuarioId)
                .ToListAsync();
        }

        public async Task Remover(Guid id)
        {
            var ingresso = await _context.Ingressos.FindAsync(id);
            if (ingresso != null)
            {
                _context.Ingressos.Remove(ingresso);
                await _context.SaveChangesAsync();
            }
        }
    }
}