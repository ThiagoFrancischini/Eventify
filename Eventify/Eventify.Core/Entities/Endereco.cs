using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Entities
{
    public class Endereco
    {
        public Guid Id { get; set; }
        public string Cep { get; set; } 
        public string Rua { get;set; }
    }
}
