using System.ComponentModel.DataAnnotations;

namespace Eventify.Models
{
    public class EnderecoModel
    {
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(8, ErrorMessage = "O CEP deve ter 8 caracteres.")]
        public string Cep { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione um Estado.")]
        public Guid? EstadoId { get; set; }

        [Required(ErrorMessage = "Selecione uma Cidade.")]
        public Guid? CidadeId { get; set; }

        [Required(ErrorMessage = "O nome da rua é obrigatório.")]
        [StringLength(200, ErrorMessage = "A rua não pode ter mais de 200 caracteres.")]
        public string Rua { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome do bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O bairro não pode ter mais de 100 caracteres.")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número é obrigatório.")]
        [StringLength(20, ErrorMessage = "O número não pode ter mais de 20 caracteres.")]
        public string Numero { get; set; } = string.Empty;

        public string? Complemento { get; set; }
    }
}
