using Eventify.Core.Entities;
using Eventify.Models;

namespace Eventify.Mapping
{
    public static class UsuarioMapper
    {
        public static Usuario ToEntity(UsuarioModel model, List<Cidade> cidadesDisponiveis)
        {
            if (model.Dt_Nascimento == null)
            {
                throw new InvalidOperationException("A data de nascimento não pode ser nula.");
            }

            var dataNascimentoUtc = DateTime.SpecifyKind(model.Dt_Nascimento.Value, DateTimeKind.Utc);

            var usuarioEntity = new Usuario
            {
                Nome = model.Nome,
                Cpf = model.Cpf,
                Email = model.Email,
                Celular = model.Celular,
                Senha = model.Senha,
                Dt_Nascimento = dataNascimentoUtc,

                Endereco = EnderecoMapper.ToEntity(model.Endereco, cidadesDisponiveis)
            };

            return usuarioEntity;
        }
    }
}
