using Eventify.Core.Entities;
using Eventify.Models.Eventify.Models;

namespace Eventify.Mapping
{
    public static class CategoriaIngressoMapper
    {
        public static CategoriaIngresso ToEntity(CategoriaIngressoModel model)
        {
            return new CategoriaIngresso
            {
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
    }
}