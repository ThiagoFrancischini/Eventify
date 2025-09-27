namespace Eventify.Core.Entities
{
    public class Endereco
    {
        public Guid Id { get; set; }
        public string Cep { get; set; } = string.Empty;
        public string Rua { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Guid CidadeId { get;set; }
        public Cidade Cidade { get; set; } = new Cidade();
    }
}
