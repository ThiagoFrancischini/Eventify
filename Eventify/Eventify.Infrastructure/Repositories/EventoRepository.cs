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
    public class EventoRepository : IEventoRepository
    {
        private readonly EventifyDbContext _context;

        public EventoRepository(EventifyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Evento>> GetEventos()
        {
            return await _context.Eventos
                .Include(e => e.CategoriasIngressos)
                .ThenInclude(c => c.Ingressos)
                .ToListAsync();
        }

        public async Task<Evento?> GetById(Guid id)
        {
            return await _context.Eventos
                .Include(e => e.CategoriasIngressos)
                .ThenInclude(c => c.Ingressos)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Salvar(Evento evento)
        {
            if (_context.Eventos.Any(e => e.Id == evento.Id))
                _context.Eventos.Update(evento);
            else
                _context.Eventos.Add(evento);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Evento>> GetByCategoria(string categoria)
        {
            return await _context.Eventos
                .Include(e => e.CategoriasIngressos)
                .Where(e => e.Categoria == categoria)
                .ToListAsync();
        }

        public async Task<List<Evento>> GetByData(DateTime data)
        {
            return await _context.Eventos
                .Include(e => e.CategoriasIngressos)
                .Where(e => e.DataInicio == data)
                .ToListAsync();
        }

        public async Task Remover(Guid id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }
        }

    }
}