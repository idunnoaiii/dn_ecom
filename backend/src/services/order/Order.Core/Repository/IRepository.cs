using System.Linq.Expressions;
using Order.Core.Common;

namespace Order.Core.Repository;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task<IReadOnlyCollection<TEntity>> GetAll();
    Task<IReadOnlyCollection<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetById(int id);
    Task<TEntity> Add(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
}