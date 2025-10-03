using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify.Core.Entities;

namespace Eventify.Core.Interfaces.Repositories
{
    public interface IEventoRepository
    {
        Task<List<Evento>> GetEventos();
        Task<Evento?> GetById(Guid id);
        Task<List<Evento>> GetByCategoria(string categoria);
        Task<List<Evento>> GetByData(DateOnly data);
        Task Salvar(Evento evento);
        Task Remover(Guid id);
    }
}
