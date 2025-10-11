using Eventify.Core.Entities;
using Eventify.Core.Interfaces.Repositories;
using Eventify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EventifyDbContext _context;
        public UsuarioRepository(EventifyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _context.Usuarios
               .AsNoTracking()
               .Include(u => u.Endereco)
               .ThenInclude(e => e.Cidade)
               .ThenInclude(c => c.Estado)
               .ToListAsync();
        }

        public async Task<Usuario?> GetById(Guid id)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.Endereco)
                .ThenInclude(e => e.Cidade)
                .ThenInclude(c => c.Estado)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetByEmail(string email)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task Salvar(Usuario usuario)
        {
            var existingUser = await _context.Usuarios.FindAsync(usuario.Id);

            if (existingUser == null)
            {
                await _context.Usuarios.AddAsync(usuario);
            }
            else
            {
                _context.Entry(existingUser).CurrentValues.SetValues(usuario);
            }
        }

        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            return await _context.Usuarios.Where(x => x.Email == email && x.Senha == senha).FirstOrDefaultAsync();
        }
    }
}
