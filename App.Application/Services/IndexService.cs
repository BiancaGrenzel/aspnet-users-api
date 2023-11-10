using App.Domain.DTO;
using App.Domain.Entities;
using App.Domain.Enum;
using App.Domain.Interfaces.Application;
using App.Domain.Interfaces.Repositories;
using Autenticador.Domain.DTOs.Auth;
using System;
using System.Linq;

namespace App.Application.Services
{
    public class IndexService : IIndexService
    {
        private IRepositoryBase<Pessoa> _repository { get; set; }
        public IndexService(IRepositoryBase<Pessoa> repository)
        {
            _repository = repository;
        }

        public AuthData Logar(LoginDTO login)
        {
            if (String.IsNullOrEmpty(login.Email))
            {
                throw new Exception("Informe o login");
            }
            if (String.IsNullOrEmpty(login.Senha))
            {
                throw new Exception("Informe a senha");
            }

            var obj = _repository.Query(x => x.Senha.Trim() == login.Senha.Trim() && x.Email.Trim() == login.Email.Trim()).FirstOrDefault();
            if (obj == null)
            {
                throw new Exception("Usuário ou senha incorretos");
            }
            else
            {
                if (obj.Permissao == 1)
                {
                    return new AuthData(obj.Id, PermissaoEnum.Administrador);
                }
                else
                {
                    return new AuthData(obj.Id, PermissaoEnum.Cliente);
                }

            }
        }

        public Pessoa Autenticado(AuthData auth)
        {
            var obj = _repository.Query(x => x.Id == Convert.ToInt32(auth.IdUsuario)).Select(x => new Pessoa
            {
                Id = x.Id,
                Nome = x.Nome,
                Email = x.Email,
                Senha = x.Senha,
            }).FirstOrDefault();
            return obj;
        }
    }
}
