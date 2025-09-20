using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Entities
{
    public class Usuario
    {
        public Guid Id { get;set; }
        public string Nome { get;set; }
        public string Cpf { get;set; }
        public string Email { get; set; }
        public DateTime Dt_Nascimento { get;set; }
        public string Celular { get;set; }
    }
}
