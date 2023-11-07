using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);
        void Save(TEntity obj);
        void Update(TEntity obj);
        void Delete(int id);
        int SaveChanges();
        DbContext Context();
    }
}