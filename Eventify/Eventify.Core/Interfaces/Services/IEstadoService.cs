using Eventify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Interfaces.Services
{
    public interface IEstadoService
    {
        public Task<List<Estado>> GetEstados();
    }
}