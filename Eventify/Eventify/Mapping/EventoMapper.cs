using Eventify.Core.Entities;
using Eventify.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Eventify.Models;

namespace Eventify.Mapping
{
    public static class EventoMapper
    {
        public static Evento ToEntity(EventoModel model, List<Cidade> cidadesDisponiveis)
        {
            if (model.DataInicio == null || model.HoraInicio == null || model.DataTermino == null || model.HoraFim == null)
            {
                throw new InvalidOperationException("Todos os campos de data e hora são obrigatórios e não podem ser nulos.");
            }

            var dataHoraInicio = DateTime.SpecifyKind(
                model.DataInicio.Value.Date + model.HoraInicio.Value,
                DateTimeKind.Utc
            );

            var dataHoraTermino = DateTime.SpecifyKind(
                model.DataTermino.Value.Date + model.HoraFim.Value,
                DateTimeKind.Utc
            );

            var endereco = new Endereco
            {

            };

            var eventoEntity = new Evento
            {
                Titulo = model.Titulo,
                Descricao = model.Descricao,
                ImagemEvento = model.ImagemEvento,
                Categoria = model.Categoria,

                DataInicio = dataHoraInicio,
                DataTermino = dataHoraTermino,

                Status = "Ativo",

                HoraInicio = model.HoraInicio.Value,
                HoraFim = model.HoraFim.Value,

                Endereco = EnderecoMapper.ToEntity(model.Endereco, cidadesDisponiveis),
                CategoriasIngressos = CategoriaIngressoMapper.ToEntityList(model.CategoriasIngresso)
            };

            return eventoEntity;
        }

        public static EventoModel ToModel(this Evento entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new EventoModel
            {
                Titulo = entity.Titulo,
                ImagemEvento = entity.ImagemEvento,
                DataInicio = entity.DataInicio,
                HoraInicio = entity.HoraInicio,
                DataTermino = entity.DataTermino,
                HoraFim = entity.HoraFim,
                Descricao = entity.Descricao,
                Categoria = entity.Categoria,
                Id = entity.Id,
                UsuarioCriacaoId = entity.UsuarioCriacaoId,

                Endereco = entity.Endereco.ToModel() ?? new EnderecoModel(),
                CategoriasIngresso = entity.CategoriasIngressos?
                    .Select(ci => ci.ToModel())
                    .ToList() ?? new List<CategoriaIngressoModel>(),
            };
        }
    }
}