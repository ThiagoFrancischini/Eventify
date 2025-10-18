using Eventify.Core.Entities;
using Eventify.Core.Filtros;

namespace Eventify.Core.Interfaces.Services
{
    public interface IEventoService
    {
        Task CriarOuAtualizarEventoAsync(Evento evento);
        Task<Evento?> ObterEventoPorIdAsync(Guid id);
        Task<List<Evento>> ObterTodosEventosAsync(FiltroEvento filtro);
        Task RemoverEventoAsync(Guid id);
    }
}
