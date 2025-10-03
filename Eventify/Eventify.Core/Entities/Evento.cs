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
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public string MaisSobre { get; set; }
        public string ImagemEvento { get; set; }
        public string Status { get; set; }
        public string Categoria { get; set; }
        public Guid EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public CategoriaIngresso[] CategoriasIngressos { get; set; }
    }
}
