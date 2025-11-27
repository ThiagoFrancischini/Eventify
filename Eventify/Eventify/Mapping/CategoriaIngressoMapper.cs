using Eventify.Core.Entities;
using Eventify.Models;
using Eventify.Models;

namespace Eventify.Mapping
{
    public static class CategoriaIngressoMapper
    {
        public static CategoriaIngresso ToEntity(CategoriaIngressoModel model)
        {
            return new CategoriaIngresso
            {
                Id = model.Id,
                Titulo = model.Titulo,
                Descricao = model.Descricao,
                Valor = model.Valor,
                LimiteCompra = model.LimiteCompra,

            };
        }

        public static List<CategoriaIngresso> ToEntityList(List<CategoriaIngressoModel> models)
        {
            if (models == null)
            {
                return new List<CategoriaIngresso>();
            }

            return models.Select(ToEntity).ToList();
        }

        public static CategoriaIngressoModel ToModel(this CategoriaIngresso entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new CategoriaIngressoModel
            {
                Id = entity.Id,
                Titulo = entity.Titulo,
                Descricao = entity.Descricao,
                Valor = entity.Valor,
                LimiteCompra = entity.LimiteCompra
            };
        }
    }
}