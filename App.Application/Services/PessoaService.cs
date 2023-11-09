using App.Domain.Entities;
using App.Domain.Interfaces.Application;
using App.Domain.Interfaces.Repositories;

namespace App.Application.Services
{
    public class PessoaService : IPessoaService
    {
        private IRepositoryBase<Pessoa> _repository { get; set; }
        public PessoaService(IRepositoryBase<Pessoa> repository)
        {
            _repository = repository;
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

        public void Criar(Pessoa pessoa)
        {
            ValidarDados(pessoa);

            _repository.Save(pessoa);
            _repository.SaveChanges();
        }

        public void Editar(Pessoa pessoa)
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



        public void Deletar(int id)
        {
            var dadosAntigos = _repository.Query(x => x.Id == id).FirstOrDefault();

            if (dadosAntigos == null)
            {
                throw new ArgumentException("Pessoa não encontrada.");
            }

            _repository.Delete(id);
            _repository.SaveChanges();
        }

        public Pessoa BuscarPorId(int id)
        {
            var obj = _repository.Query(x => x.Id == id).FirstOrDefault();
            return obj;
        }

        public List<Pessoa> BuscarLista()
        {
            return _repository.Query(x => 1 == 1).ToList();
        }

    }
}