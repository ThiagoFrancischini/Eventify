namespace Eventify.Core.Entities
{
    public class Cidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Guid EstadoId { get; set; }
        public Estado Estado { get; set; } = new Estado();
    }
}
