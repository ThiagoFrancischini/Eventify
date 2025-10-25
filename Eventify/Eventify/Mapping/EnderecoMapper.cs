using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify.Core.Entities;
using Eventify.Models;

namespace Eventify.Mapping
{
    public static class EnderecoMapper
    {
        public static Endereco ToEntity(EnderecoModel model, List<Cidade> cidadesDisponiveis)
        {
            if (model.CidadeId == null)
            {
                throw new InvalidOperationException("A cidade não foi selecionada.");
            }

            var cidadeSelecionada = cidadesDisponiveis.FirstOrDefault(c => c.Id == model.CidadeId.Value);
            if (cidadeSelecionada == null)
            {
                throw new InvalidOperationException("A cidade selecionada é inválida.");
            }

            // Remover formatação do CEP
            var cepLimpo = model.Cep?.Replace("-", "").Trim();

            return new Endereco
            {
                Cep = cepLimpo,
                Rua = model.Rua,
                Bairro = model.Bairro,
                Numero = model.Numero,
                Complemento = model.Complemento,
                Cidade = cidadeSelecionada
            };
        }

        public static EnderecoModel ToModel(this Endereco entity)
        {
            if (entity == null)
            {
                return null;
            }

            // Remover formatação do CEP
            entity.Cep =String.IsNullOrWhiteSpace( entity.Cep) ? entity.Cep : entity.Cep.Replace("-", "").Trim();

            return new EnderecoModel
            {
                Cep = entity.Cep,
                Rua = entity.Rua,
                Bairro = entity.Bairro,
                Numero = entity.Numero,
                Complemento = entity.Complemento,
                CidadeId = entity.CidadeId,
                CidadeNome = entity.Cidade?.Nome,
                EstadoId = entity.Cidade?.EstadoId
            };
        }
    }
}
