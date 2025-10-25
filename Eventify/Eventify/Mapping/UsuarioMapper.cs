using System.Text.RegularExpressions;
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

            // Remover formatação de CPF e Celular
            var cpfLimpo = Regex.Replace(model.Cpf ?? "", "[^0-9]", "");
            var celularLimpo = Regex.Replace(model.Celular ?? "", "[^0-9]", "");

            var usuarioEntity = new Usuario
            {
                Nome = model.Nome,
                Cpf = cpfLimpo,
                Email = model.Email,
                Celular = celularLimpo,
                Senha = model.Senha,
                Dt_Nascimento = dataNascimentoUtc,

                Endereco = EnderecoMapper.ToEntity(model.Endereco, cidadesDisponiveis)
            };

            return usuarioEntity;
        }

        public static UsuarioModel ToModel(Usuario entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new UsuarioModel
            {
                Nome = entity.Nome,
                Email = entity.Email,
                // Converte a data de UTC (do banco) para Local (para exibição na tela)
                Dt_Nascimento = entity.Dt_Nascimento.ToLocalTime(),

                // Formata os dados limpos do banco de volta para o formato com máscara
                Cpf = FormatarCpf(entity.Cpf),
                Celular = FormatarCelular(entity.Celular),

                // Mapeia o endereço
                // Assumindo que EnderecoMapper.ToModel(entity.Endereco) existe
                // e que entity.Endereco (e entity.Endereco.Cidade) foi carregado
                Endereco = EnderecoMapper.ToModel(entity.Endereco),

                // Senha e ConfirmarSenha ficam vazios por segurança ao carregar dados
                Senha = string.Empty,
                ConfirmarSenha = string.Empty
            };
        }

        // MÉTODOS AUXILIARES PARA FORMATAÇÃO

        /// <summary>
        /// Formata um CPF de 11 dígitos para o formato ###.###.###-##
        /// </summary>
        private static string FormatarCpf(string cpfLimpo)
        {
            // Retorna o valor original se não estiver no formato esperado
            if (string.IsNullOrWhiteSpace(cpfLimpo) || cpfLimpo.Length != 11 || !long.TryParse(cpfLimpo, out _))
            {
                return cpfLimpo;
            }

            try
            {
                // Converte para ulong para usar a formatação de string numérica
                return Convert.ToUInt64(cpfLimpo).ToString(@"000\.000\.000\-00");
            }
            catch
            {
                return cpfLimpo; // Retorno seguro em caso de falha
            }
        }

        /// <summary>
        /// Formata um celular de 11 dígitos para o formato (##) #####-####
        /// </summary>
        private static string FormatarCelular(string celularLimpo)
        {
            // Retorna o valor original se não estiver no formato esperado
            if (string.IsNullOrWhiteSpace(celularLimpo) || celularLimpo.Length != 11 || !long.TryParse(celularLimpo, out _))
            {
                return celularLimpo;
            }

            try
            {
                return Convert.ToUInt64(celularLimpo).ToString(@"(00)\ 00000\-0000");
            }
            catch
            {
                return celularLimpo; // Retorno seguro em caso de falha
            }
        }
    }
}
