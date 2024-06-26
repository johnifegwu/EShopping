
namespace Cache.Repositories
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
        Task<TEntity> GetAsync(string key);

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
        Task<IEnumerable<TEntity>> GetRangeAsync(string key);

        /// <summary>
        /// Gets a string value from the cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        string GetString(string key);

        /// <summary>
        /// Gets a string value from the cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<string> GetStringAsync(string key);

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
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity, string key);

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
        Task<IEnumerable<TEntity>> SwapOneAsync(TEntity entity, string IdFieldName, string key);

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
        Task<IEnumerable<TEntity>> SwapRangeAsync(TEntity entity, string ClassName, string IdFieldName, string key);

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        IEnumerable<TEntity> AppendUnionRange(IEnumerable<TEntity> entity, string IdFieldName, string key);

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AppendUnionRangeAsync(IEnumerable<TEntity> entity, string IdFieldName, string key);

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="ClassName">Child entity name (where IdFieldName is equal to Entity.ChildClass.IdFieldName).</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        IEnumerable<TEntity> AppendUnionRange(IEnumerable<TEntity> entity, string ClassName, string IdFieldName, string key);

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="ClassName">Child entity name (where IdFieldName is equal to Entity.ChildClass.IdFieldName).</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AppendUnionRangeAsync(IEnumerable<TEntity> entity, string ClassName, string IdFieldName, string key);

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
        Task<TEntity> AddAsync(TEntity entity, string key);

        /// <summary>
        /// Adds a string value to the cache.
        /// </summary>
        /// <param name="entity">String value to be cached.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        string Add(string entity, string key);

        /// <summary>
        /// Adds a string value to the cache.
        /// </summary>
        /// <param name="entity">String value to be cached.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<string> AddAsync(string entity, string key);

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
        Task<TEntity> UpdateAsync(TEntity entity, string key);

        /// <summary>
        /// Replaces an entity in the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entity, string key);

        /// <summary>
        /// Replaces an entity in the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entity, string key);

        /// <summary>
        /// Replaces a string value in the cache.
        /// </summary>
        /// <param name="entity">String value.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        string Update(string entity, string key);

        /// <summary>
        /// Replaces a string value in the cache.
        /// </summary>
        /// <param name="entity">String value.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        Task<string> UpdateAsync(string entity, string key);

        /// <summary>
        /// Removes the cached data from the system.
        /// </summary>
        /// <param name="key">Cache key.</param>
        void Delete(string key);

        /// <summary>
        /// Removes the cached data from the system.
        /// </summary>
        /// <param name="key">Cache key.</param>
        Task DeleteAsync(string key);
    }
}
