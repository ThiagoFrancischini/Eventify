using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify.Core.Entities;
using Eventify.Core.Filtros;

namespace Eventify.Core.Interfaces.Repositories
{
    public interface IIngressoRepository
    {
        Task<Ingresso?> GetById(Guid id);
        Task<Ingresso?> GetByCodigo(string codigo);
        Task<List<Ingresso>> GetByEvent(Guid eventoId);
        Task<List<Ingresso>> GetByUser(Guid usuarioId);
        Task<List<Ingresso>> GetByCategory(Guid categoriaId);
        Task<List<Ingresso>> GetAll(FiltroIngresso filtro);
        Task Salvar(Ingresso ingresso);
        Task Remover(Guid id);
    }
}