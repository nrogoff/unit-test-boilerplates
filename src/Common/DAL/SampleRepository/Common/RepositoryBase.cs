using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SampleRepository.Common
{
    /// <summary>
    /// The base repository class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity> where TEntity : class
        where TContext : DbContext
    {
        #region Protected Variables

        protected readonly TContext Context;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(TContext context)
        {
            this.Context = context;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to add data asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        /// <summary>
        /// Method to add range asynchronously
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(IList<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        /// <summary>
        /// Method to find data
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        /// <summary>
        /// Method to find data asynchronously
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Method to get all the data asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Method to get the data asynchronously by id passed as query param
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ValueTask<TEntity?> GetByIdAsync(int id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Method to remove data
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Method to remove range of data
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IList<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        /// <summary>
        /// Method to retrieve the an element that satisfies a specified condition or a default value 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Method to retrieve a sequence that satisfies a specified connection or a default value
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Method to update state
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateState(TEntity entity)
        {
            Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Method to detach connection
        /// </summary>
        /// <param name="entity"></param>
        public void Detach(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        #endregion
    }
}
