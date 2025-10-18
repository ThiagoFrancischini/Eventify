using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify.Core.Entities;
using Eventify.Core.Filtros;

namespace Eventify.Core.Interfaces.Repositories
{
    public interface IEventoRepository
    {
        Task<List<Evento>> GetEventos(FiltroEvento filtro);
        Task<Evento?> GetById(Guid id);
        Task Salvar(Evento evento);
        Task Remover(Guid id);
    }
}
