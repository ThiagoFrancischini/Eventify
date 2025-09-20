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
    public class EstadoService : IEstadoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EstadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Estado>> GetEstados()
        {
            return await _unitOfWork.EstadoRepository.GetEstados();
        }
    }
}
