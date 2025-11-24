using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify.Core.Entities;
using Eventify.Core.Filtros;

namespace Eventify.Core.Interfaces
{
    public interface IIngressoService
    {
        Task<Ingresso?> ObterIngressoPorIdAsync(Guid id);
        Task<Ingresso?> ObterIngressoPorCodigoAsync(string codigo);
        Task<List<Ingresso>> ObterTodosIngressosAsync(FiltroIngresso? filtro = null);
        Task<List<Ingresso>> ObterIngressosPorEventoAsync(Guid eventoId);
        Task<List<Ingresso>> ObterIngressosPorUsuarioAsync(Guid usuarioId);
        Task<List<Ingresso>> ObterIngressosPorCategoriaAsync(Guid categoriaId);
        Task<Ingresso?> CriarIngressoAsync(Ingresso ingresso);
        Task<List<Ingresso>?> CriarIngressosEmLoteAsync(List<Ingresso> ingressos);
        Task<Ingresso?> AtualizarIngressoAsync(Ingresso ingresso);
        Task<bool> ValidarIngressoAsync(Guid ingressoId);
        Task<bool> InvalidarIngressoAsync(Guid ingressoId);
        Task<bool> UsarIngressoAsync(Guid ingressoId);
        Task<bool> DeletarIngressoAsync(Guid id);
        Task<int> ContarIngressosVendidosPorEventoAsync(Guid eventoId);
        Task<int> ContarIngressosVendidosPorCategoriaAsync(Guid categoriaId);
        Task<bool> VerificarIngressoValidoAsync(string codigo);
    }
}