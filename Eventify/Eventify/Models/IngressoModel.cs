using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Models
{
    public class IngressoModel
    {
        public Guid Id { get; set; }
        public Guid UsuarioCompraId { get; set; }
        public string UsuarioCompraNome { get; set; } = string.Empty;
        public bool Valido { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime? DataUso { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public Guid EventoId { get; set; }
        public EventoModel Evento { get; set; }
        public string EventoTitulo { get; set; } = string.Empty;
        public Guid CategoriaIngressoId { get; set; }
        public string CategoriaNome { get; set; } = string.Empty;
        public decimal CategoriaValor { get; set; }

        // Propriedades calculadas para exibição
        public bool Usado => DataUso.HasValue;
        public string Status => !Valido ? "Inválido" : Usado ? "Usado" : "Válido";
        public string StatusCor => !Valido ? "#DC3545" : Usado ? "#6C757D" : "#28A745";
        public string DataCompraFormatada => DataCompra.ToString("dd/MM/yyyy HH:mm");
        public string DataUsoFormatada => DataUso?.ToString("dd/MM/yyyy HH:mm") ?? "-";
    }
}