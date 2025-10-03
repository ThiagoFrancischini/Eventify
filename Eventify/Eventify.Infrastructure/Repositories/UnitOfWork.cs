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
        public IIngressoRepository IngressoRepository { get; }
        public IEventoRepository EventoRepository { get; }
        public ICategoriasIngressoRepository CategoriasIngressoRepository { get; }

        public UnitOfWork(
            EventifyDbContext context,
            IEstadoRepository estadoRepository,
            IUsuarioRepository usuarioRepository,
            ICidadeRepository cidadeRepository,
            IEnderecoRepository enderecoRepository,
            IIngressoRepository ingressoRepository,
            IEventoRepository eventoRepository,
            ICategoriasIngressoRepository categoriasIngressoRepository
            )
        {
            _context = context;
            EstadoRepository = estadoRepository;
            UsuarioRepository = usuarioRepository;
            CidadeRepository = cidadeRepository;
            EnderecoRepository = enderecoRepository;
            IngressoRepository = ingressoRepository;
            EventoRepository = eventoRepository;
            CategoriasIngressoRepository = categoriasIngressoRepository;
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
