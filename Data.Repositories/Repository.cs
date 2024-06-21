using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

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

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(context.Set<T>().ToList());
        }

        public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await context.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public virtual IQueryable<T> Read()
        {
            return context.Set<T>().AsNoTracking();
        }

        public virtual T Add(T entity)
        {
            context.Set<T>().Add(entity);

            SaveChanges();

            return entity;
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await context.Set<T>().AddAsync(entity, cancellationToken);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }

        public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await context.Set<T>().AddRangeAsync(entities, cancellationToken);

            await SaveChangesAsync(cancellationToken);

            return entities;
        }

        public virtual T Update(T entity)
        {
            context.Set<T>().Update(entity);

            SaveChanges();

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            context.Set<T>().Update(entity);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }

        public virtual async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            context.Set<T>().UpdateRange(entities);

            await SaveChangesAsync();

            return entities;
        }

        public virtual int Delete(T entity)
        {
            context.Set<T>().Remove(entity);

            return SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            context.Set<T>().Remove(entity);

            return await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            context.Set<T>().RemoveRange(entities);

            return await SaveChangesAsync(cancellationToken);
        }

        public virtual int SaveChanges()
        {
            return context.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }

    }
}

