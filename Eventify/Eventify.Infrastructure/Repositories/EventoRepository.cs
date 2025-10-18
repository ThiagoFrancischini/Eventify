using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify.Core.Entities;
using Eventify.Core.Filtros;
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

        public async Task<List<Evento>> GetEventos(FiltroEvento filtro)
        {
            var query = _context.Eventos
                .Include(e => e.CategoriasIngressos)
                    .ThenInclude(c => c.Ingressos)
                .Include(e => e.Endereco)
                    .ThenInclude(end => end.Cidade)
                .Include(e => e.UsuarioCriacao)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Titulo))
            {
                query = query.Where(e => e.Titulo.ToUpper().Contains(filtro.Titulo.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(filtro.Categoria))
            {
                query = query.Where(e => e.Categoria == filtro.Categoria);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Status))
            {
                query = query.Where(e => e.Status == filtro.Status);
            }

            if (filtro.EstadoId.HasValue)
            {
                query = query.Where(e => e.Endereco != null &&
                                         e.Endereco.Cidade != null &&
                                         e.Endereco.Cidade.EstadoId == filtro.EstadoId.Value);
            }

            if (filtro.CidadeId.HasValue)
            {
                query = query.Where(e => e.Endereco != null &&
                                         e.Endereco.CidadeId == filtro.CidadeId.Value);
            }

            if (filtro.DataMin.HasValue)
            {
                query = query.Where(e => e.DataInicio.Date >= filtro.DataMin.Value.Date);
            }

            if (filtro.DataMax.HasValue)
            {
                query = query.Where(e => e.DataInicio.Date <= filtro.DataMax.Value.Date);
            }

            if (filtro.OrderByMaisVendidos)
            {
                query = query.OrderByDescending(e =>
                    _context.Ingressos.Count(i => i.EventoId == e.Id)
                );
            }
            else
            {
                query = query.OrderBy(e => e.DataInicio);
            }

            return await query.ToListAsync();
        }

        public async Task<Evento?> GetById(Guid id)
        {
            return await _context.Eventos
                .Include(e => e.CategoriasIngressos)
                    .ThenInclude(c => c.Ingressos)
                .Include(e => e.UsuarioCriacao)
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
                .Include(e => e.UsuarioCriacao)
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