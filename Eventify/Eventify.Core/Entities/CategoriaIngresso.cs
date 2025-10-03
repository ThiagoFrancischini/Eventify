using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Entities
{
    public class CategoriaIngresso
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int LimiteCompra { get; set; }
        public Guid EventoId { get; set; }
        public Evento Evento { get; set; }
        public Ingresso[] Ingressos { get; set; }
    }
}
