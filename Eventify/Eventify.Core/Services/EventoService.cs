using Eventify.Core.Entities;
using Eventify.Core.Filtros;
using Eventify.Core.Interfaces.Repositories;
using Eventify.Core.Interfaces.Services;

namespace Eventify.Core.Services
{
    public class EventoService : IEventoService
    {
        private readonly IUnitOfWork uow;

        public EventoService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<List<Evento>> ObterTodosEventosAsync(FiltroEvento filtro)
        {
            return await uow.EventoRepository.GetEventos(filtro);
        }

        public async Task<Evento?> ObterEventoPorIdAsync(Guid id)
        {
            return await uow.EventoRepository.GetById(id);
        }

        public async Task CriarOuAtualizarEventoAsync(Evento evento)
        {
            if (evento.DataTermino < evento.DataInicio)
            {
                throw new InvalidOperationException("A data de término do evento não pode ser anterior à data de início.");
            }            

            await uow.EventoRepository.Salvar(evento);
        }

        public async Task RemoverEventoAsync(Guid id)
        {
            var ingressos = await uow.IngressoRepository.GetAll(new FiltroIngresso
            {
                EventoId = id,
            });

            foreach(var ingresso in ingressos)
            {
                ingresso.Valido = false;
                await uow.IngressoRepository.Remover(ingresso.Id);
            }

            await uow.EventoRepository.Remover(id);
        }
    }
}