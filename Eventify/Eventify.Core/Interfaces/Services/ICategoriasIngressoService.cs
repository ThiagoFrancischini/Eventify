using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eventify.Core.Entities;

namespace Eventify.Core.Interfaces
{
    public interface ICategoriasIngressoService
    {
        Task<CategoriaIngresso?> ObterCategoriaPorIdAsync(Guid id);
        Task<List<CategoriaIngresso>> ObterTodasCategoriasAsync();
        Task<List<CategoriaIngresso>> ObterCategoriasPorEventoAsync(Guid eventoId);
        Task<CategoriaIngresso?> CriarCategoriaAsync(CategoriaIngresso categoria);
        Task<CategoriaIngresso?> AtualizarCategoriaAsync(CategoriaIngresso categoria);
        Task<bool> DeletarCategoriaAsync(Guid id);
        Task<bool> VerificarDisponibilidadeAsync(Guid categoriaId, int quantidade);
        Task<int> ObterQuantidadeDisponivel(Guid categoriaId);
        Task<int> ObterQuantidadeVendida(Guid categoriaId);
    }
}
