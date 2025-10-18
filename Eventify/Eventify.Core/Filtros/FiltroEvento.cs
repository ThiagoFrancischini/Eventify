using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Filtros
{
    public class FiltroEvento
    {
        public string? Titulo { get; set; }
        public string? Categoria { get; set; }
        public string? Status { get; set; }
        public Guid? EstadoId { get; set; }
        public Guid? CidadeId { get; set; }
        public DateTime? DataMin { get; set; }
        public DateTime? DataMax { get; set; }
        public bool OrderByMaisVendidos { get; set; } = false;
    }
}
