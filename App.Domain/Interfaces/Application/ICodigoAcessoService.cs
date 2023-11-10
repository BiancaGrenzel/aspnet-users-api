using Autenticador.Domain.DTOs.Auth;

namespace App.Domain.Interfaces.Application
{
    public interface ICodigoAcessoService
    {
        string Gerar(AuthData Auth);
    }
}
