using App.Domain.DTO.Auth;
using App.Domain.Entities;
using App.Domain.Interfaces.Application;
using App.Domain.Interfaces.Repositories;

namespace App.Application.Services
{
    public class PessoaService : IPessoaService
    {
        private IRepositoryBase<Pessoa> _repository { get; set; }
        private readonly IActiveDirectoryService _adService;
        public PessoaService(IRepositoryBase<Pessoa> repository, IActiveDirectoryService adService)
        {
            _repository = repository;
            _adService = adService;
        }

        public AuthData Autenticar(AuthRequest obj)
        {
            if (String.IsNullOrEmpty(obj.Email))
            {
                throw new Exception("Informe seu email");
            }

            if (String.IsNullOrEmpty(obj.Senha))
            {
                throw new Exception("Informe sua senha");
            }
            var nome = _adService.Autenticar(obj);
            if (String.IsNullOrEmpty(nome))
            {
                throw new Exception("Usuário e/ou senha inválido(s)");
            }

            var usuario = _repository.Query(x => x.Email.Trim().ToLower() == obj.Email.Trim().ToLower()).FirstOrDefault();
            var auth = new AuthData(usuario.Id, usuario.Nivel);
            return auth;
        }

        public Pessoa UsuarioAutenticado(AuthData auth)
        {
            var obj = _repository.Query(x => x.Id == auth.IdUsuario)
                .Select(x => new Pessoa()
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Email = x.Email,
                    Senha = x.Senha,
                }).FirstOrDefault();
            return obj;
        }
        private void ValidarDados(Pessoa pessoa)
        {
            if (string.IsNullOrEmpty(pessoa.Nome))
            {
                throw new ArgumentNullException(nameof(pessoa.Nome), "Nome não pode estar vazio.");
            }

            if (string.IsNullOrEmpty(pessoa.Email))
            {
                throw new ArgumentNullException(nameof(pessoa.Email), "Email não pode estar vazio.");
            }

            if (string.IsNullOrEmpty(pessoa.Senha))
            {
                throw new ArgumentNullException(nameof(pessoa.Senha), "Senha não pode estar vazia.");
            }
        }

        public void Criar(AuthData auth,Pessoa pessoa)
        {
            ValidarDados(pessoa);       

            _repository.Save(pessoa);
            _repository.SaveChanges();
        }

        public void Editar(AuthData auth, Pessoa pessoa)
        {
            var dadosAntigos = _repository.Query(x => x.Id == pessoa.Id).FirstOrDefault();

            if (dadosAntigos == null)
            {
                throw new ArgumentException("Pessoa não encontrada.");
            }

            Pessoa dadosAtualizados = new Pessoa();
            dadosAtualizados.Id = dadosAntigos.Id;

            dadosAtualizados.Nome = (pessoa.Nome != null) ? pessoa.Nome : dadosAntigos.Nome;
            dadosAtualizados.Email = (pessoa.Email != null) ? pessoa.Email : dadosAntigos.Email;
            dadosAtualizados.Senha = (pessoa.Senha != null) ? pessoa.Senha : dadosAntigos.Senha;

            _repository.Update(dadosAtualizados);
            _repository.SaveChanges();
        }



        public void Deletar(AuthData auth, int id)
        {
            var dadosAntigos = _repository.Query(x => x.Id == id).FirstOrDefault();

            if (dadosAntigos == null)
            {
                throw new ArgumentException("Pessoa não encontrada.");
            }

            _repository.Delete(id);
            _repository.SaveChanges();
        }

        public Pessoa BuscarPorId(AuthData auth, int id)
        {
            var obj = _repository.Query(x => x.Id == id).FirstOrDefault();
            return obj;
        }

        public List<Pessoa> BuscarLista(AuthData auth)
        {
            return _repository.Query(x => 1 == 1).ToList();
        }
    }
}