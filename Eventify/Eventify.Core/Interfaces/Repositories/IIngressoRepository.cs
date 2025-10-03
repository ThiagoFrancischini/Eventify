using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify.Core.Entities;

namespace Eventify.Core.Interfaces.Repositories
{
    public interface IIngressoRepository
    {
        Task<List<Ingresso>> GetIngressos();
        Task<Ingresso?> GetById(Guid id);
        Task<Ingresso?> GetByCodigo(string codigo);
        Task<List<Ingresso>> GetByUsuario(Guid usuarioId);
        Task Salvar(Ingresso ingresso);
        Task Remover(Guid id);
    }
}
