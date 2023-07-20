using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Model;
using System.Linq.Expressions;

namespace GrupoSelect.Domain.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteAll(Expression<Func<TEntity, bool>>? filter = null);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes);

        PaginateResult<IEnumerable<TEntity>> GetAllPaginate(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int page = 1, int qtPage = 20, params Expression<Func<TEntity, object>>[] includes);

        void Save();
    }
}
