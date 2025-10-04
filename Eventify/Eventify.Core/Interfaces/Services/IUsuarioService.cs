using Eventify.Core.Entities;
using Eventify.Core.Interfaces.Repositories;

namespace Eventify.Core.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario> Autenticar(Usuario usuario);
        Task<Usuario> CriarNovoUsuarioAsync(Usuario novoUsuario);
    }
}
