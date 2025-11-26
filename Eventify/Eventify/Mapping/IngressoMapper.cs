using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify.Core.Entities;
using Eventify.Models;

namespace Eventify.Mapping
{
    public static class IngressoMapper
    {
        public static IngressoModel ToModel(Ingresso entity)
        {
            if (entity == null)
                return null;

            return new IngressoModel
            {
                Id = entity.Id,
                UsuarioCompraId = entity.UsuarioCompraId,
                UsuarioCompraNome = entity.UsuarioCompra?.Nome ?? string.Empty,
                Valido = entity.Valido,
                DataCompra = entity.DataCompra,
                DataUso = entity.DataUso,
                Codigo = entity.Codigo,
                Evento = EventoMapper.ToModel(entity.Evento),
                EventoId = entity.EventoId,
                EventoTitulo = entity.Evento?.Titulo ?? string.Empty,
                CategoriaIngressoId = entity.CategoriaIngressoId,
                CategoriaNome = entity.CategoriaIngresso?.Titulo ?? string.Empty,
                CategoriaValor = entity.CategoriaIngresso?.Valor ?? 0
            };
        }

        public static Ingresso ToEntity(IngressoModel model)
        {
            if (model == null)
                return null;

            return new Ingresso
            {
                Id = model.Id,
                UsuarioCompraId = model.UsuarioCompraId,
                Valido = model.Valido,
                DataCompra = model.DataCompra,
                DataUso = model.DataUso,
                Evento = new Evento
                {
                    Id = model.EventoId,
                },
                Codigo = model.Codigo,
                EventoId = model.EventoId,
                CategoriaIngressoId = model.CategoriaIngressoId
            };
        }

        public static List<IngressoModel> ToModelList(List<Ingresso> entities)
        {
            if (entities == null || !entities.Any())
                return new List<IngressoModel>();

            return entities.Select(ToModel).ToList();
        }

        public static List<Ingresso> ToEntityList(List<IngressoModel> models)
        {
            if (models == null || !models.Any())
                return new List<Ingresso>();

            return models.Select(ToEntity).ToList();
        }
    }
}