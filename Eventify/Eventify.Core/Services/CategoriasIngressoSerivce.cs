using Eventify.Core.Entities;
using Eventify.Core.Interfaces;
using Eventify.Core.Interfaces.Repositories;

namespace Eventify.Services
{
    public class CategoriasIngressoService : ICategoriasIngressoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriasIngressoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoriaIngresso?> ObterCategoriaPorIdAsync(Guid id)
        {
            try
            {
                return await _unitOfWork.CategoriasIngressoRepository.GetById(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CategoriaIngresso>> ObterTodasCategoriasAsync()
        {
            try
            {
                var categorias = await _unitOfWork.CategoriasIngressoRepository.GetAll();
                return categorias.OrderBy(c => c.Valor).ToList();
            }
            catch
            {
                return new List<CategoriaIngresso>();
            }
        }

        public async Task<List<CategoriaIngresso>> ObterCategoriasPorEventoAsync(Guid eventoId)
        {
            try
            {
                var categorias = await _unitOfWork.CategoriasIngressoRepository.GetByEvent(eventoId);
                return categorias.OrderBy(c => c.Valor).ToList();
            }
            catch
            {
                return new List<CategoriaIngresso>();
            }
        }

        public async Task<CategoriaIngresso?> CriarCategoriaAsync(CategoriaIngresso categoria)
        {
            try
            {
                categoria.Id = Guid.NewGuid();

                await _unitOfWork.CategoriasIngressoRepository.Salvar(categoria);
                await _unitOfWork.CompleteAsync();

                return categoria;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CategoriaIngresso?> AtualizarCategoriaAsync(CategoriaIngresso categoria)
        {
            try
            {
                var categoriaExistente = await _unitOfWork.CategoriasIngressoRepository.GetById(categoria.Id);

                if (categoriaExistente == null)
                    return null;

                categoriaExistente.Titulo = categoria.Titulo;
                categoriaExistente.Descricao = categoria.Descricao;
                categoriaExistente.Valor = categoria.Valor;
                categoriaExistente.LimiteCompra = categoria.LimiteCompra;

                await _unitOfWork.CategoriasIngressoRepository.Salvar(categoriaExistente);
                await _unitOfWork.CompleteAsync();

                return categoriaExistente;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeletarCategoriaAsync(Guid id)
        {
            try
            {
                var categoria = await _unitOfWork.CategoriasIngressoRepository.GetById(id);

                if (categoria == null)
                    return false;

                // Verifica se existem ingressos vendidos
                if (categoria.Ingressos != null && categoria.Ingressos.Any())
                {
                    // Não permite deletar categoria com ingressos já vendidos
                    return false;
                }

                await _unitOfWork.CategoriasIngressoRepository.Remover(id);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> VerificarDisponibilidadeAsync(Guid categoriaId, int quantidade)
        {
            try
            {
                var categoria = await _unitOfWork.CategoriasIngressoRepository.GetById(categoriaId);

                if (categoria == null)
                    return false;

                var quantidadeVendida = categoria.Ingressos?.Count ?? 0;
                var quantidadeDisponivel = categoria.LimiteCompra - quantidadeVendida;

                return quantidadeDisponivel >= quantidade;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> ObterQuantidadeDisponivel(Guid categoriaId)
        {
            try
            {
                var categoria = await _unitOfWork.CategoriasIngressoRepository.GetById(categoriaId);

                if (categoria == null)
                    return 0;

                var quantidadeVendida = categoria.Ingressos?.Count ?? 0;
                return categoria.LimiteCompra - quantidadeVendida;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> ObterQuantidadeVendida(Guid categoriaId)
        {
            try
            {
                var categoria = await _unitOfWork.CategoriasIngressoRepository.GetById(categoriaId);

                return categoria?.Ingressos?.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}