using System.ComponentModel.DataAnnotations;

namespace Eventify.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;

    namespace Eventify.Models
    {
        public class CategoriaIngressoModel
        {
            [Required(ErrorMessage = "O título do ingresso é obrigatório.")]
            [StringLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
            public string Titulo { get; set; } = string.Empty;

            [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
            public string Descricao { get; set; } = string.Empty;

            [Required(ErrorMessage = "O valor é obrigatório.")]
            [Range(0.00, 99999.99, ErrorMessage = "O valor deve ser entre R$ 0,00 e R$ 99.999,99.")]
            public decimal Valor { get; set; }

            [Required(ErrorMessage = "O limite de compra é obrigatório.")]
            [Range(1, int.MaxValue, ErrorMessage = "O limite deve ser no mínimo 1.")]
            public int LimiteCompra { get; set; }
        }
    }
}
