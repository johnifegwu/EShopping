﻿
namespace Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets the DbSet object.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Get();

        /// <summary>
        /// Gets the DbSet object as NonTracking.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Read();

        /// <summary>
        ///  Fetches data form the database based on Raw Sql command.
        /// </summary>
        /// <param name="query">Sql statement or stored procedure.</param>
        /// <returns>IEnumerable</returns>
        /// <remarks>Not for MongoDB or Similar Document Database.</remarks>
        Task<IEnumerable<T>> RunSqlAsync(string query);

        /// <summary>
        ///  Fetches data form the database based on Raw Sql command.
        /// </summary>
        /// <param name="query">Sql statement or stored procedure.</param>
        /// <param name="parameters">Parameters.</param>
        /// <returns>IEnumerable</returns>
        /// <remarks>Not for MongoDB or Similar Document Database.</remarks>
        Task<IEnumerable<T>> RunSqlAsync(string query, params object[] parameters);

        /// <summary>
        /// Fetch All.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Fecth All in a pginated formart.
        /// </summary>
        /// <param name="PageIndex">Page index, must be greater than zero.</param>
        /// <param name="PageSize">Page size, must be at least one.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(int PageIndex, int PageSize);
        Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
        T Add(T entity);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        int Delete(T entity);
        Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<int> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        T Update(T entity);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
