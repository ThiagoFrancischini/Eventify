using Eventify.Core.Entities;


namespace Eventify.Core.Interfaces.Repositories
{
    public interface ICidadeRepository
    {
        Task<List<Cidade>> GetCidadesPorEstado(Guid estadoId);
        Task<Cidade?> GetById(Guid id);
        Task Salvar(Cidade cidade);
    }
}
