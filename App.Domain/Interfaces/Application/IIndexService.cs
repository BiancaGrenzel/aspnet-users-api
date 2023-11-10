using App.Domain.DTO;
using App.Domain.Entities;
using Autenticador.Domain.DTOs.Auth;

namespace App.Domain.Interfaces.Application
{
    public interface IIndexService
    {
        AuthData Logar(LoginDTO login);
        Pessoa Autenticado(AuthData auth);
    }
}
