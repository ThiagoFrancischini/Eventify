using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEstadoRepository EstadoRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        ICidadeRepository CidadeRepository { get; }
        IEnderecoRepository EnderecoRepository { get; }
        IEventoRepository EventoRepository { get; }
        IIngressoRepository IngressoRepository { get; } 
        ICategoriasIngressoRepository CategoriasIngressoRepository { get; }
        Task<int> CompleteAsync();
    }
}
