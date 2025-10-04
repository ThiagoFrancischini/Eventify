using Eventify.Core.Entities;
using Eventify.Core.Interfaces.Repositories;
using Eventify.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _unitOfWork.UsuarioRepository.GetAll();
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.UsuarioRepository.GetById(id);
        }

        public async Task<Usuario> Autenticar(Usuario usuario)
        {
            return await _unitOfWork.UsuarioRepository.GetById(usuario.Id);
        }

        public async Task<Usuario> CriarNovoUsuarioAsync(Usuario novoUsuario)
        {
            if (novoUsuario?.Endereco == null)
            {
                throw new ArgumentNullException("Os dados do usuário e do endereço são obrigatórios.");
            }

            novoUsuario.Id = Guid.NewGuid();

            var emailExistente = await _unitOfWork.UsuarioRepository.GetByEmail(novoUsuario.Email);
            if (emailExistente != null)
            {
                throw new ApplicationException("Este e-mail já está em uso.");
            }

            await _unitOfWork.UsuarioRepository.Salvar(novoUsuario);

            await _unitOfWork.CompleteAsync();

            return novoUsuario;
        }
    }
}
