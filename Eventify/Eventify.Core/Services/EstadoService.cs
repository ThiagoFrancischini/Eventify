using Eventify.Core.Entities;
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
        public Task<List<Estado>> GetEstados()
        {
            throw new NotImplementedException();
        }
    }
}
