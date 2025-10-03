using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Entities
{
    public class Evento
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }
        public string MaisSobre { get; set; }
        public string ImagemEvento { get; set; }
        public string Status { get; set; }
        public string Categoria { get; set; }
        public Guid EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public CategoriaIngresso[] CategoriasIngressos { get; set; }

    }
}
