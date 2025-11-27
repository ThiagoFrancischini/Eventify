using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Models.States
{
    public class PedidoStateService
    {
        public EventoModel? EventoSelecionado { get; private set; }
        public CategoriaIngressoModel? CategoriaSelecionada { get; private set; }
        public int Quantidade { get; private set; }

        public void IniciarPedido(EventoModel evento, CategoriaIngressoModel categoria, int quantidade)
        {
            EventoSelecionado = evento;
            CategoriaSelecionada = categoria;
            Quantidade = quantidade;
        }

        public void LimparPedido()
        {
            EventoSelecionado = null;
            CategoriaSelecionada = null;
            Quantidade = 0;
        }

        public bool TemPedidoValido()
        {
            return EventoSelecionado != null && CategoriaSelecionada != null && Quantidade > 0;
        }
    }
}
