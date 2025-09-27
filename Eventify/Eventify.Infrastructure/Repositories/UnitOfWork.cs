using Eventify.Core.Interfaces.Repositories;
using Eventify.Infrastructure.Data;

namespace Eventify.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventifyDbContext _context;

        public IEstadoRepository EstadoRepository { get; }
        public IUsuarioRepository UsuarioRepository { get; }
        public ICidadeRepository CidadeRepository { get; }
        public IEnderecoRepository EnderecoRepository { get; }

        public UnitOfWork(
            EventifyDbContext context,
            IEstadoRepository estadoRepository,
            IUsuarioRepository usuarioRepository,
            ICidadeRepository cidadeRepository,
            IEnderecoRepository enderecoRepository)
        {
            _context = context;
            EstadoRepository = estadoRepository;
            UsuarioRepository = usuarioRepository;
            CidadeRepository = cidadeRepository;
            EnderecoRepository = enderecoRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
