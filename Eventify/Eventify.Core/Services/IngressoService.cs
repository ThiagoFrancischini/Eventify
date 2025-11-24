using Eventify.Core.Entities;
using Eventify.Core.Filtros;
using Eventify.Core.Interfaces;
using Eventify.Core.Interfaces.Repositories;
using Eventify.Core.Interfaces.Services;


namespace Eventify.Services
{
    public class IngressoService : IIngressoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngressoService(IUnitOfWork unitOfWork)
        {
            try
            {
                _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no construtor IngressoService: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<Ingresso?> ObterIngressoPorIdAsync(Guid id)
        {
            try
            {
                return await _unitOfWork.IngressoRepository.GetById(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Ingresso?> ObterIngressoPorCodigoAsync(string codigo)
        {
            try
            {
                var ingressos = await _unitOfWork.IngressoRepository.GetAll();
                return ingressos.FirstOrDefault(i => i.Codigo == codigo);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Ingresso>> ObterTodosIngressosAsync(FiltroIngresso? filtro = null)
        {
            try
            {
                var ingressos = await _unitOfWork.IngressoRepository.GetAll();

                if (filtro != null)
                {
                    if (filtro.EventoId.HasValue)
                    {
                        ingressos = ingressos.Where(i => i.EventoId == filtro.EventoId.Value).ToList();
                    }

                    if (filtro.UsuarioCompraId.HasValue)
                    {
                        ingressos = ingressos.Where(i => i.UsuarioCompraId == filtro.UsuarioCompraId.Value).ToList();
                    }

                    if (filtro.CategoriaIngressoId.HasValue)
                    {
                        ingressos = ingressos.Where(i => i.CategoriaIngressoId == filtro.CategoriaIngressoId.Value).ToList();
                    }

                    if (filtro.Valido.HasValue)
                    {
                        ingressos = ingressos.Where(i => i.Valido == filtro.Valido.Value).ToList();
                    }

                    if (filtro.DataCompraInicio.HasValue)
                    {
                        ingressos = ingressos.Where(i => i.DataCompra >= filtro.DataCompraInicio.Value).ToList();
                    }

                    if (filtro.DataCompraFim.HasValue)
                    {
                        ingressos = ingressos.Where(i => i.DataCompra <= filtro.DataCompraFim.Value).ToList();
                    }

                    if (filtro.Usado.HasValue)
                    {
                        if (filtro.Usado.Value)
                        {
                            ingressos = ingressos.Where(i => i.DataUso.HasValue).ToList();
                        }
                        else
                        {
                            ingressos = ingressos.Where(i => !i.DataUso.HasValue).ToList();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.Codigo))
                    {
                        ingressos = ingressos.Where(i => i.Codigo.Contains(filtro.Codigo)).ToList();
                    }

                    if (filtro.OrderByDataCompra)
                    {
                        ingressos = ingressos.OrderBy(i => i.DataCompra).ToList();
                    }
                    else if (filtro.OrderByDataCompraDesc)
                    {
                        ingressos = ingressos.OrderByDescending(i => i.DataCompra).ToList();
                    }
                }

                return ingressos;
            }
            catch
            {
                return new List<Ingresso>();
            }
        }

        public async Task<List<Ingresso>> ObterIngressosPorEventoAsync(Guid eventoId)
        {
            try
            {
                var ingressos = await _unitOfWork.IngressoRepository.GetByEvent(eventoId);
                return ingressos.OrderByDescending(i => i.DataCompra).ToList();
            }
            catch
            {
                return new List<Ingresso>();
            }
        }

        public async Task<List<Ingresso>> ObterIngressosPorUsuarioAsync(Guid usuarioId)
        {
            try
            {
                var ingressos = await _unitOfWork.IngressoRepository.GetByUser(usuarioId);
                return ingressos.OrderByDescending(i => i.DataCompra).ToList();
            }
            catch
            {
                return new List<Ingresso>();
            }
        }

        public async Task<List<Ingresso>> ObterIngressosPorCategoriaAsync(Guid categoriaId)
        {
            try
            {
                var ingressos = await _unitOfWork.IngressoRepository.GetByCategory(categoriaId);
                return ingressos.OrderByDescending(i => i.DataCompra).ToList();
            }
            catch
            {
                return new List<Ingresso>();
            }
        }

        public async Task<Ingresso?> CriarIngressoAsync(Ingresso ingresso)
        {
            try
            {
                // Verifica disponibilidade
                var categoria = await _unitOfWork.CategoriasIngressoRepository.GetById(ingresso.CategoriaIngressoId);

                if (categoria == null)
                    return null;

                var quantidadeVendida = categoria.Ingressos?.Count ?? 0;
                if (quantidadeVendida >= categoria.LimiteCompra)
                    return null;

                ingresso.Id = Guid.NewGuid();
                ingresso.DataCompra = DateTime.Now;
                ingresso.Valido = true;
                ingresso.Codigo = GerarCodigoUnico();

                await _unitOfWork.IngressoRepository.Salvar(ingresso);
                await _unitOfWork.CompleteAsync();

                return ingresso;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Ingresso>?> CriarIngressosEmLoteAsync(List<Ingresso> ingressos)
        {
            try
            {
                var ingressosCriados = new List<Ingresso>();

                // Agrupa por categoria para verificar disponibilidade
                var ingressosPorCategoria = ingressos.GroupBy(i => i.CategoriaIngressoId);

                foreach (var grupo in ingressosPorCategoria)
                {
                    var categoriaId = grupo.Key;
                    var quantidadeSolicitada = grupo.Count();

                    var categoria = await _unitOfWork.CategoriasIngressoRepository.GetById(categoriaId);

                    if (categoria == null)
                        return null;

                    var quantidadeVendida = categoria.Ingressos?.Count ?? 0;
                    var quantidadeDisponivel = categoria.LimiteCompra - quantidadeVendida;

                    if (quantidadeDisponivel < quantidadeSolicitada)
                        return null;
                }

                // Cria os ingressos
                foreach (var ingresso in ingressos)
                {
                    ingresso.Id = Guid.NewGuid();
                    ingresso.DataCompra = DateTime.Now;
                    ingresso.Valido = true;
                    ingresso.Codigo = GerarCodigoUnico();

                    await _unitOfWork.IngressoRepository.Salvar(ingresso);
                    ingressosCriados.Add(ingresso);
                }

                await _unitOfWork.CompleteAsync();
                return ingressosCriados;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Ingresso?> AtualizarIngressoAsync(Ingresso ingresso)
        {
            try
            {
                var ingressoExistente = await _unitOfWork.IngressoRepository.GetById(ingresso.Id);

                if (ingressoExistente == null)
                    return null;

                ingressoExistente.Valido = ingresso.Valido;
                ingressoExistente.DataUso = ingresso.DataUso;

                await _unitOfWork.IngressoRepository.Salvar(ingressoExistente);
                await _unitOfWork.CompleteAsync();

                return ingressoExistente;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ValidarIngressoAsync(Guid ingressoId)
        {
            try
            {
                var ingresso = await _unitOfWork.IngressoRepository.GetById(ingressoId);

                if (ingresso == null)
                    return false;

                ingresso.Valido = true;
                await _unitOfWork.IngressoRepository.Salvar(ingresso);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> InvalidarIngressoAsync(Guid ingressoId)
        {
            try
            {
                var ingresso = await _unitOfWork.IngressoRepository.GetById(ingressoId);

                if (ingresso == null)
                    return false;

                ingresso.Valido = false;
                await _unitOfWork.IngressoRepository.Salvar(ingresso);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UsarIngressoAsync(Guid ingressoId)
        {
            try
            {
                var ingresso = await _unitOfWork.IngressoRepository.GetById(ingressoId);

                if (ingresso == null || !ingresso.Valido || ingresso.DataUso.HasValue)
                    return false;

                ingresso.DataUso = DateTime.Now;
                await _unitOfWork.IngressoRepository.Salvar(ingresso);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeletarIngressoAsync(Guid id)
        {
            try
            {
                await _unitOfWork.IngressoRepository.Remover(id);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> ContarIngressosVendidosPorEventoAsync(Guid eventoId)
        {
            try
            {
                var ingressos = await _unitOfWork.IngressoRepository.GetByEvent(eventoId);
                return ingressos.Count;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> ContarIngressosVendidosPorCategoriaAsync(Guid categoriaId)
        {
            try
            {
                var ingressos = await _unitOfWork.IngressoRepository.GetByCategory(categoriaId);
                return ingressos.Count;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> VerificarIngressoValidoAsync(string codigo)
        {
            try
            {
                var ingressos = await _unitOfWork.IngressoRepository.GetAll();
                var ingresso = ingressos.FirstOrDefault(i => i.Codigo == codigo);

                return ingresso != null && ingresso.Valido && !ingresso.DataUso.HasValue;
            }
            catch
            {
                return false;
            }
        }

        private string GerarCodigoUnico()
        {
            // Gera um código único de 12 caracteres alfanuméricos
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}