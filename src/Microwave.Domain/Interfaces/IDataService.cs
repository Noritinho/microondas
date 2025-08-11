using System.Linq.Expressions;

namespace Microwave.Domain.Interfaces;

public interface IDataService<TEntity> where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetAllAsync();

    public Task<TEntity?> GetByIdAsync(object id);

    public Task CreateAsync(TEntity entity);

    Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> predicate);

    public Task UpdateAsync(TEntity entity);

    public Task DeleteAsync(TEntity entity);
}