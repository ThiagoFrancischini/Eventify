using Eventify.Core.Entities;
using Eventify.Models;
using System;
using System.Linq;
using System.Collections.Generic;

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
    }
}