namespace Eventify.Core.Entities
{
    public class Usuario
    {
        private string senha = string.Empty;

        public Guid Id { get;set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get;set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Dt_Nascimento { get;set; }
        public string Celular { get; set; } = string.Empty;
        public Guid EnderecoId { get;set; }
        public Endereco Endereco { get; set; } = new Endereco();
        public string Senha
        {
            get
            {
                return senha;
            }
            set
            {
                senha = value;
            }
        }
        public List<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
