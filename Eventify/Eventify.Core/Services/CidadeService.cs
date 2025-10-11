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
    public class CidadeService : ICidadeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CidadeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Cidade>> ProcurarPorUF(Guid estadoId)
        {
            return await _unitOfWork.CidadeRepository.GetCidadesPorEstado(estadoId);
        }

        public async Task<Cidade> Salvar(Cidade cidade)
        {
            cidade.Id = Guid.NewGuid();

            await _unitOfWork.CidadeRepository.Salvar(cidade);

            return cidade;
        }
    }
}
