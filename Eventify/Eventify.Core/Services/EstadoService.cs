﻿using Eventify.Core.Entities;
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

        public async Task<List<Estado>> GetAllAsync()
        {
            return await _unitOfWork.EstadoRepository.GetEstados();
        }

        public async Task<Estado?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.EstadoRepository.GetById(id);
        }

        public async Task<Estado> CriarEstadoAsync(Estado novoEstado)
        {
            novoEstado.Id = Guid.NewGuid();

            await _unitOfWork.EstadoRepository.Salvar(novoEstado);
            await _unitOfWork.CompleteAsync();

            return novoEstado;
        }
    }
}
