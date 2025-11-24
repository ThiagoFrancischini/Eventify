using Eventify.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventify.Models
{
    public class EventoModel
    {
        [Required(ErrorMessage = "O título do evento é obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O título deve ter entre 5 e 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A imagem de divulgação é obrigatória.")]
        public string ImagemEvento { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "A hora de início é obrigatória.")]
        public TimeSpan? HoraInicio { get; set; }

        [Required(ErrorMessage = "A data de término é obrigatória.")]
        public DateTime? DataTermino { get; set; }

        [Required(ErrorMessage = "A hora de término é obrigatória.")]
        public TimeSpan? HoraFim { get; set; }

        [Required(ErrorMessage = "A descrição do evento é obrigatória.")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "A descrição deve ter entre 20 e 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A categoria do evento é obrigatória.")]
        [StringLength(50, ErrorMessage = "A categoria não pode exceder 50 caracteres.")]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "Deve haver ao menos um tipo de ingresso.")]
        public List<CategoriaIngressoModel> CategoriasIngresso { get; set; } = new List<CategoriaIngressoModel>();

        [Required(ErrorMessage = "As informações de endereço são obrigatórias.")]
        public EnderecoModel Endereco { get; set; } = new EnderecoModel();

        [Range(typeof(bool), "true", "true", ErrorMessage = "Você deve aceitar os termos de responsabilidade para criar o evento.")]
        public bool AceiteTermos { get; set; } = false;

        public Guid Id { get; set; }
    }
}