using Eventify.Core.Interfaces.Repositories;
using Eventify.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventifyDbContext _context;

        private IEstadoRepository _estadoRepository;
        private IUsuarioRepository _usuarioRepository;

        public UnitOfWork(EventifyDbContext context)
        {
            _context = context;
        }

        public IEstadoRepository EstadoRepository => _estadoRepository ??= new EstadoRepository(_context);

        public IUsuarioRepository UsuarioRepository => _usuarioRepository ??= new UsuarioRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
