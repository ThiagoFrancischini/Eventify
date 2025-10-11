using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Models
{
    public class UsuarioModel
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
        [StringLength(100, ErrorMessage = "O email não pode ter mais de 100 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime? Dt_Nascimento { get; set; }

        [Required(ErrorMessage = "O número de celular é obrigatório.")]
        public string Celular { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; } = string.Empty;

        public EnderecoModel Endereco { get; set; } = new EnderecoModel();
    }
}
