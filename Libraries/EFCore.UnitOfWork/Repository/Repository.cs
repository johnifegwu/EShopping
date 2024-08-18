using EFCore.UnitOfWorkCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    /// <summary>
    /// IRepository implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private IJayDbContext context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        public Repository(IJayDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the DbSet object.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> Get()
        {
            return context.Set<T>();
        }

        /// <summary>
        ///  Fetches data form the database based on Raw Sql command.
        /// </summary>
        /// <param name="query">Sql statement or stored procedure.</param>
        /// <returns>IEnumerable</returns>
        /// <remarks>Not for MongoDB or Similar Document Database.</remarks>
        public virtual async Task<IEnumerable<T>> RunSqlAsync(string query)
        {
            return await Task.FromResult(context.Set<T>().FromSqlRaw<T>(query).AsEnumerable().ToList());
        }

        /// <summary>
        ///  Fetches data form the database based on Raw Sql command.
        /// </summary>
        /// <param name="query">Sql statement or stored procedure.</param>
        /// <param name="parameters">Parameters.</param>
        /// <returns>IEnumerable</returns>
        /// <remarks>Not for MongoDB or Similar Document Database.</remarks>
        public virtual async Task<IEnumerable<T>> RunSqlAsync(string query, params object[] parameters)
        {
            return await Task.FromResult(context.Set<T>().FromSqlRaw<T>(query, parameters).AsEnumerable().ToList());
        }

        /// <summary>
        /// Fetch All.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(context.Set<T>().ToList());
        }

        /// <summary>
        /// Fecth All in a pginated formart.
        /// </summary>
        /// <param name="PageIndex">Page index, must be greater than zero.</param>
        /// <param name="PageSize">Page size, must be at least one.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(int PageIndex, int PageSize)
        {
            return await Task.FromResult(context.Set<T>().Skip((PageIndex - 1) * PageSize)
                .Take(PageSize).ToList());
        }

        /// <summary>
        /// Fetch by Id.
        /// </summary>
        /// <typeparam name="TId">Id type.</typeparam>
        /// <param name="id">Id value.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns></returns>
        public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await context.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the DbSet object as NonTracking.
        /// </summary>
        public virtual IQueryable<T> Read()
        {
            return context.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Adds an entity to the database.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns></returns>
        public virtual T Add(T entity)
        {
            context.Set<T>().Add(entity);

            SaveChanges();

            return entity;
        }

        /// <summary>
        /// Adds an entity to the database.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns></returns>
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await context.Set<T>().AddAsync(entity, cancellationToken);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }

        /// <summary>
        /// Adds a range of entities to the database.
        /// </summary>
        /// <param name="entities">Entities to be added.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await context.Set<T>().AddRangeAsync(entities, cancellationToken);

            await SaveChangesAsync(cancellationToken);

            return entities;
        }

        /// <summary>
        /// Updates the given entity in the database.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <returns></returns>
        public virtual T Update(T entity)
        {
            context.Set<T>().Update(entity);

            SaveChanges();

            return entity;
        }

        /// <summary>
        /// Updates the given entity in the database.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns></returns>
        public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            context.Set<T>().Update(entity);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates the given entities in the database.
        /// </summary>
        /// <param name="entities">Entities to be updated.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            context.Set<T>().UpdateRange(entities);

            await SaveChangesAsync();

            return entities;
        }

        /// <summary>
        /// Removes an entity from the database.
        /// </summary>
        /// <param name="entity">Entity to be removed.</param>
        /// <returns></returns>
        public virtual int Delete(T entity)
        {
            context.Set<T>().Remove(entity);

            return SaveChanges();
        }

        /// <summary>
        /// Removes an entity from the database.
        /// </summary>
        /// <param name="entity">Entity to be removed.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            context.Set<T>().Remove(entity);

            return await SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Removes a range of entities from the database.
        /// </summary>
        /// <param name="entities">Entities to be added.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns></returns>
        public virtual async Task<int> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            context.Set<T>().RemoveRange(entities);

            return await SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Persists all changes to the database.
        /// </summary>
        public virtual int SaveChanges()
        {
            return context.SaveChanges();
        }

        /// <summary>
        /// Persists all changes to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }

    }
}

