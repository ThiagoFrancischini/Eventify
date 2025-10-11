using Eventify.Core.Entities;

namespace Eventify.Core.Interfaces.Services
{
    public interface ICidadeService
    {
        public Task<List<Cidade>> ProcurarPorUF(Guid estadoId);

        public Task<Cidade> Salvar(Cidade cidade);
    }
}
