using Eventify.Core.Entities;

namespace Eventify.Core.Interfaces.Services
{
    interface ICidadeService
    {
        public Task<List<Cidade>> ProcurarPorUF(Guid estadoId);
    }
}
