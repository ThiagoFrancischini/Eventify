using Eventify.Core.Entities;

namespace Eventify.Core.Interfaces.Services
{
    public interface IEventoService
    {
        Task CriarOuAtualizarEventoAsync(Evento evento);
        Task<Evento?> ObterEventoPorIdAsync(Guid id);
        Task<List<Evento>> ObterEventosPorCategoriaAsync(string categoria);
        Task<List<Evento>> ObterEventosPorDataAsync(DateTime data);
        Task<List<Evento>> ObterTodosEventosAsync();
        Task RemoverEventoAsync(Guid id);
    }
}
