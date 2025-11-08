using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Models.States
{
    public class PedidoStateService
    {
        public object IngressoAtual { get; private set; }

        public void DefinirIngressoParaPagar(object ingresso)
        {
            IngressoAtual = ingresso;
        }

        public void LimparPedido()
        {
            IngressoAtual = null;
        }
    }
}
