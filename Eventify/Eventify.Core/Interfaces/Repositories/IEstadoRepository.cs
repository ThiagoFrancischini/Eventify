using Eventify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Interfaces.Repositories
{
    public interface IEstadoRepository
    {
        public Task<List<Estado>> GetEstados();
    }
}
