using App.Domain.DTO;
using App.Domain.DTO.Auth;
using App.Domain.Entities;
using App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Interfaces.Application
{
    public interface IPessoaService
    {
        AuthData Autenticar(AuthRequest obj);
        Pessoa UsuarioAutenticado(AuthData auth);
        void Editar(AuthData auth, Pessoa obj);
        void Deletar(AuthData auth, int id);
        void Criar(AuthData auth, Pessoa obj);
        Pessoa BuscarPorId(AuthData auth, int id);
        List<Pessoa> BuscarLista(AuthData auth);
    }
}
