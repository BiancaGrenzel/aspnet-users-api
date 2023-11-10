using Autenticador.Domain.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Application
{
    public interface ICodigoAcessoService
    {
        string Gerar(AuthData Auth);
    }
}
