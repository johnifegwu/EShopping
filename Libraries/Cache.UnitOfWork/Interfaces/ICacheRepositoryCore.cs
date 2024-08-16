
namespace Cache.Repositories.AspNetCore
{
    public interface ICacheRepositoryCore<TEntity>
        where TEntity : class
    {

        /// <summary>
        /// Gets an entity from the cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        TEntity Get(string key);

        /// <summary>
        /// Gets an entity from the cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of entities from the Cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetRange(string key);

        /// <summary>
        /// Gets a list of entities from the Cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetRangeAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a list of entities to the Cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entity, string key);

        /// <summary>
        /// Adds a list of entities to the Cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity, string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Swaps one entity from a list of enities.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        IEnumerable<TEntity> SwapOne(TEntity entity, string IdFieldName, string key);

        /// <summary>
        /// Swaps one entity from a list of enities.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> SwapOneAsync(TEntity entity, string IdFieldName, string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Swaps multiple entities from a list of enities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> SwapRangeAsync(IEnumerable<TEntity> entities, string IdFieldName, string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Swaps one entity from a list of enities.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="ClassName">Child entity name (where IdFieldName is equal to Entity.ChildClass.IdFieldName).</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        IEnumerable<TEntity> SwapOne(TEntity entity, string ClassName, string IdFieldName, string key);

        /// <summary>
        /// Swaps one entity from a list of enities.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="ClassName">Child entity name (where IdFieldName is equal to Entity.ChildClass.IdFieldName).</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> SwapOneAsync(TEntity entity, string ClassName, string IdFieldName, string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Swaps multiple entities from a list of enities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="ClassName">Child entity name (where IdFieldName is equal to Entity.ChildClass.IdFieldName).</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> SwapRangeAsync(IEnumerable<TEntity> entities, string ClassName, string IdFieldName, string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        IEnumerable<TEntity> AppendUnionRange(IEnumerable<TEntity> entities, string IdFieldName, string key);

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AppendUnionRangeAsync(IEnumerable<TEntity> entities, string IdFieldName, string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="ClassName">Child entity name (where IdFieldName is equal to Entity.ChildClass.IdFieldName).</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        IEnumerable<TEntity> AppendUnionRange(IEnumerable<TEntity> entities, string ClassName, string IdFieldName, string key);

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entities">Entity.</param>
        /// <param name="ClassName">Child entity name (where IdFieldName is equal to Entity.ChildClass.IdFieldName).</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AppendUnionRangeAsync(IEnumerable<TEntity> entities, string ClassName, string IdFieldName, string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds an entity to the cache.
        /// </summary>
        /// <param name="entity">Entity to be cached.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        TEntity Add(TEntity entity, string key);

        /// <summary>
        /// Adds an entity to the cache.
        /// </summary>
        /// <param name="entity">Entity to be cached.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity, string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Replaces an entity in the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        TEntity Update(TEntity entity, string key);

        /// <summary>
        /// Replaces an entity in the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity, string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Replaces an entity in the cache.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities, string key);

        /// <summary>
        /// Replaces an entity in the cache.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes the cached data from the system.
        /// </summary>
        /// <param name="key">Cache key.</param>
        bool Delete(string key);

        /// <summary>
        /// Removes the cached data from the system.
        /// </summary>
        /// <param name="key">Cache key.</param>
        Task<bool> DeleteAsync(string key, CancellationToken cancellationToken = default);
    }
}
