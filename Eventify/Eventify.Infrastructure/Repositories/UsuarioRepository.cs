using Eventify.Core.Interfaces.Repositories;
using Eventify.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EventifyDbContext _context;
        public UsuarioRepository(EventifyDbContext context)
        {
            _context = context;
        }
    }
}
