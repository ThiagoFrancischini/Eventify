using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Entities
{
    public class Ingresso
    {
        public Guid Id { get; set; }
        public Guid UsuarioCompraId { get; set; }
        public Usuario UsuarioCompra { get; set; }
        public bool Valido { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime? DataUso { get; set; }
        public string Codigo { get; set; }
        public Guid EventoId { get; set; }
        public Evento Evento { get; set; }
        public Guid CategoriaIngressoId { get; set; }
        public CategoriaIngresso CategoriaIngresso { get; set; }
    }
}
