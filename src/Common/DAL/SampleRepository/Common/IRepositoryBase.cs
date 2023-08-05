using System.Linq.Expressions;

namespace SampleRepository.Common;

/// <summary>
/// The Base Repository Interface
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepositoryBase<TEntity>where TEntity : class
{
    ValueTask<TEntity?> GetByIdAsync(int id);
    Task<IList<TEntity>> GetAllAsync();
    IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IList<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IList<TEntity> entities);
    void UpdateState(TEntity entity);
    void Detach(TEntity entity);
}