﻿
namespace Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get();
        IQueryable<T> Read();
        Task<IEnumerable<T>> GetAllAsync();
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
