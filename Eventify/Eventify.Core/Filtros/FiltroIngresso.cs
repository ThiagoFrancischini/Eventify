using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Filtros
{
    public class FiltroIngresso
    {
        public Guid? EventoId { get; set; }
        public Guid? UsuarioCompraId { get; set; }
        public Guid? CategoriaIngressoId { get; set; }
        public bool? Valido { get; set; }
        public DateTime? DataCompraInicio { get; set; }
        public DateTime? DataCompraFim { get; set; }
        public bool? Usado { get; set; }
        public string? Codigo { get; set; }
        public bool OrderByDataCompra { get; set; }
        public bool OrderByDataCompraDesc { get; set; }
    }
}
