using App.Domain.Enums;

namespace App.Domain.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoPessoa Nivel { get; }
    }
}
