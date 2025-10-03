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
    public class CategoriasIngressoRepository : ICategoriasIngressoRepository
    {
        private readonly EventifyDbContext _context;

        public CategoriasIngressoRepository(EventifyDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaIngresso>> GetCategoriasIngresso()
        {
            return await _context.CategoriasIngresso
                .Include(c => c.Ingressos)
                .ToListAsync();
        }

        public async Task<CategoriaIngresso?> GetById(Guid id)
        {
            return await _context.CategoriasIngresso
                .Include(c => c.Ingressos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Salvar(CategoriaIngresso categoria)
        {
            if (_context.CategoriasIngresso.Any(c => c.Id == categoria.Id))
                _context.CategoriasIngresso.Update(categoria);
            else
                _context.CategoriasIngresso.Add(categoria);

            await _context.SaveChangesAsync();
        }

        public async Task Remover(Guid id)
        {
            var categoria = await _context.CategoriasIngresso.FindAsync(id);
            if (categoria != null)
            {
                _context.CategoriasIngresso.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}