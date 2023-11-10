using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class CodigoAcesso
    {
        [Key]
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public Guid UsuarioId { get; set; }
        public Pessoa Usuario { get; set; }
    }
}
