using System.ComponentModel.DataAnnotations;

namespace App.Domain.DTO.Auth
{
    public class AuthRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}