using Eventify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetById(Guid id);
        Task<Usuario?> GetByEmail(string email);
        Task Salvar(Usuario usuario);
        Task<List<Usuario>> GetAll();
    }
}
