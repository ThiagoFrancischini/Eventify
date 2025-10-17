using Eventify.Core.Entities;
using Eventify.Core.Interfaces.Repositories;
using Eventify.Core.Interfaces.Services;

namespace Eventify.Core.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<List<Evento>> ObterTodosEventosAsync()
        {
            return await _eventoRepository.GetEventos();
        }

        public async Task<Evento?> ObterEventoPorIdAsync(Guid id)
        {
            return await _eventoRepository.GetById(id);
        }

        public async Task<List<Evento>> ObterEventosPorCategoriaAsync(string categoria)
        {
            return await _eventoRepository.GetByCategoria(categoria);
        }

        public async Task<List<Evento>> ObterEventosPorDataAsync(DateTime data)
        {
            // Ajuste para considerar apenas a data, se necessário
            return await _eventoRepository.GetByData(data.Date);
        }

        public async Task CriarOuAtualizarEventoAsync(Evento evento)
        {
            if (evento.DataTermino < evento.DataInicio)
            {
                throw new InvalidOperationException("A data de término do evento não pode ser anterior à data de início.");
            }

            await _eventoRepository.Salvar(evento);
        }

        public async Task RemoverEventoAsync(Guid id)
        {
            await _eventoRepository.Remover(id);
        }
    }
}