using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
