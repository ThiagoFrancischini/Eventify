using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify.Core.Entities;

namespace Eventify.Core.Interfaces.Repositories
{
    public interface ICategoriasIngressoRepository
    {
        Task<List<CategoriaIngresso>> GetCategoriasIngresso();
        Task<CategoriaIngresso?> GetById(Guid id);
        Task Salvar(CategoriaIngresso categoria);
        Task Remover(Guid id);
    }
}
