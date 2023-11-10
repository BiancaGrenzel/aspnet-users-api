using App.Domain.Entities;

namespace App.Domain.Interfaces.Application
{
    public interface IPessoaService
    {
        void Editar(Pessoa obj);
        void Deletar(Guid id);
        void Criar(Pessoa obj);
        Pessoa BuscarPorId(Guid id);
        List<Pessoa> BuscarLista();
    }
}
