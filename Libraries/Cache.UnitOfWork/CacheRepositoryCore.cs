
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Cache.Repositories.AspNetCore;

namespace Cache.Repositories.AspNetCore
{
    internal class CacheRepositoryCore<TEntity> : ICacheRepositoryCore<TEntity>
        where TEntity : class
    {

        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _cacheOptions;

        public CacheRepositoryCore(IDistributedCache distributedCache, DistributedCacheEntryOptions cacheOptions)
        {
            _distributedCache = distributedCache;
            _cacheOptions = cacheOptions;
        }

        /// <summary>
        /// Adds an entity to the cache.
        /// </summary>
        /// <param name="entity">Entity to be cached.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public TEntity Add(TEntity entity, string key)
        {
            var jsonString = JsonConvert.SerializeObject(entity);
            _distributedCache.SetString(key, jsonString, _cacheOptions);
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entity, string key)
        {
            var jsonString = JsonConvert.SerializeObject(entity);
            _distributedCache.SetString(key, jsonString, _cacheOptions);
            return entity;
        }

        /// <summary>
        /// Adds a list of entities to the Cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public async Task<TEntity> AddAsync(TEntity entity, string key, CancellationToken cancellationToken = default)
        {
            var jsonString = JsonConvert.SerializeObject(entity);
            await _distributedCache.SetStringAsync(key, jsonString, _cacheOptions, cancellationToken);
            return entity;
        }

        /// <summary>
        /// Adds a list of entities to the Cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity, string key, CancellationToken cancellationToken = default)
        {
            var jsonString = JsonConvert.SerializeObject(entity);
            await _distributedCache.SetStringAsync(key, jsonString, _cacheOptions, cancellationToken);
            return entity;
        }

        /// <summary>
        /// Gets an entity from the cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public TEntity Get(string key)
        {
            //get entry by key
            var jsonString = _distributedCache.GetString(key);

            if (string.IsNullOrEmpty(jsonString))
            {
                return default!;
            }

            var result = JsonConvert.DeserializeObject<TEntity>(jsonString);

            if(result == null)
                return default!;

            return result;
        }

        /// <summary>
        /// Gets a list of entities from the Cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetRange(string key)
        {
            //get entry by key
            var jsonString = _distributedCache.GetString(key);

            if (string.IsNullOrEmpty(jsonString))
            {
                return default!;
            }

            var result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString);

            if (result == null)
                return default!;

            return result;
        }

        /// <summary>
        /// Gets an entity from the cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(string key, CancellationToken cancellationToken = default)
        {
            //Get entry by key
            var jsonString = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(jsonString))
            {
                return default!;
            }

            var result = await Task.FromResult(JsonConvert.DeserializeObject<TEntity>(jsonString));

            if (result == null)
                return default!;

            return result;
        }

        /// <summary>
        /// Gets a list of entities from the Cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetRangeAsync(string key, CancellationToken cancellationToken = default)
        {
            //Get entry by key
            var jsonString = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(jsonString))
            {
                return default!;
            }

            var result = await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString));
            
            if (result == null)
                return default!;

            return result;
        }

        /// <summary>
        /// Replaces an entity in the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public TEntity Update(TEntity entity, string key)
        {
            //delete entry by key and insert entry by key
            try
            {
                //delete entry by key
                _distributedCache.Remove(key);
            }
            catch
            {
                //do nothing
            }
            var jsonString = JsonConvert.SerializeObject(entity);
            _distributedCache.SetString(key, jsonString, _cacheOptions);
            return entity;
        }

        /// <summary>
        /// Replaces an entity in the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public async Task<TEntity> UpdateAsync(TEntity entity, string key, CancellationToken cancellationToken = default)
        {
            try
            {
                //delete entry by key
                await _distributedCache.RemoveAsync(key, cancellationToken);
            }
            catch
            {
                //do nothing
            }
            var jsonString = await Task.FromResult(JsonConvert.SerializeObject(entity));
            await _distributedCache.SetStringAsync(key, jsonString, _cacheOptions, cancellationToken);
            return entity;
        }

        /// <summary>
        /// Replaces an entity in the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entity, string key)
        {
            //delete entry by key and insert entry by key
            try
            {
                //delete entry by key
                _distributedCache.Remove(key);
            }
            catch
            {
                //do nothing
            }
            var jsonString = JsonConvert.SerializeObject(entity);
            _distributedCache.SetString(key, jsonString, _cacheOptions);
            return entity;
        }

        /// <summary>
        /// Replaces an entity in the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entity, string key, CancellationToken cancellationToken = default)
        {
            try
            {
                //delete entry by key
                await _distributedCache.RemoveAsync(key, cancellationToken);
            }
            catch
            {
                //do nothing
            }
            var jsonString = await Task.FromResult(JsonConvert.SerializeObject(entity));
            await _distributedCache.SetStringAsync(key, jsonString, _cacheOptions, cancellationToken);
            return entity;
        }

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> AppendUnionRange(IEnumerable<TEntity> entity, string IdFieldName, string key)
        {
            //Get entry by key
            var jsonString = _distributedCache.GetString(key);

            try
            {
                //delete entry by key
                _distributedCache.Remove(key);
            }
            catch
            {
                //do nothing
            }

            IEnumerable<TEntity>? list = null;

            if (!string.IsNullOrEmpty(jsonString))
            {
                list = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString);
            }

            var unionList = entity;

            if (list != null)
            {
                unionList = GetUnion(list, entity, IdFieldName);
            }

            jsonString = JsonConvert.SerializeObject(unionList);

            _distributedCache.SetString(key, jsonString, _cacheOptions);

            return unionList;
        }

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> AppendUnionRangeAsync(IEnumerable<TEntity> entity, string IdFieldName, string key, CancellationToken cancellationToken = default)
        {
            //Get entry by key
            var jsonString = await _distributedCache.GetStringAsync(key, cancellationToken);

            try
            {
                //delete entry by key
                await _distributedCache.RemoveAsync(key, cancellationToken);
            }
            catch
            {
                //do nothing
            }

            IEnumerable<TEntity>? list = null;

            if (!string.IsNullOrEmpty(jsonString))
            {
                list = await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString));
            }

            var unionList = entity;

            if (list != null)
            {
                unionList = await Task.FromResult(GetUnion(list, entity, IdFieldName));
            }

            jsonString = await Task.FromResult(JsonConvert.SerializeObject(unionList));

            await _distributedCache.SetStringAsync(key, jsonString, _cacheOptions, cancellationToken);

            return unionList;
        }

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="ClassName">Child entity name (where IdFieldName is equal to Entity.ChildClass.IdFieldName).</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> AppendUnionRange(IEnumerable<TEntity> entity, string ClassName, string IdFieldName, string key)
        {
            //Get entry by key
            var jsonString = _distributedCache.GetString(key);

            try
            {
                //delete entry by key
                _distributedCache.Remove(key);
            }
            catch
            {
                //do nothing
            }

            IEnumerable<TEntity>? list = null;

            if (!string.IsNullOrEmpty(jsonString))
            {
                list = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString);
            }

            var unionList = entity;

            if (list != null)
            {
                unionList = GetUnion(list, entity, ClassName, IdFieldName);
            }

            jsonString = JsonConvert.SerializeObject(unionList);

            _distributedCache.SetString(key, jsonString, _cacheOptions);

            return unionList;
        }

        /// <summary>
        /// Appends Union a range of entities to the cache.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="ClassName">Child entity name (where IdFieldName is equal to Entity.ChildClass.IdFieldName).</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> AppendUnionRangeAsync(IEnumerable<TEntity> entity, string ClassName, string IdFieldName, string key, CancellationToken cancellationToken = default)
        {
            //Get entry by key
            var jsonString = await _distributedCache.GetStringAsync(key, cancellationToken);

            try
            {
                //delete entry by key
                await _distributedCache.RemoveAsync(key);
            }
            catch
            {
                //do nothing
            }

            IEnumerable<TEntity>? list = null;

            if (!string.IsNullOrEmpty(jsonString))
            {
                list = await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString));
            }

            var unionList = entity;

            if (list != null)
            {
                unionList = await Task.FromResult(GetUnion(list, entity, ClassName, IdFieldName));
            }

            jsonString = await Task.FromResult(JsonConvert.SerializeObject(unionList));

            await _distributedCache.SetStringAsync(key, jsonString, _cacheOptions, cancellationToken);

            return unionList;
        }

        /// <summary>
        /// Swaps one entity from a list of enities.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> SwapOne(TEntity entity, string IdFieldName, string key)
        {
            //Get entry by key
            var jsonString = _distributedCache.GetString(key);

            try
            {
                //delete entry by key
                _distributedCache.Remove(key);
            }
            catch
            {
                //do nothing
            }

            IEnumerable<TEntity>? list = null;

            if (!string.IsNullOrEmpty(jsonString))
            {
                list = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString);
            }

            //Swap item in list here.
            IEnumerable<TEntity> swapedList = new List<TEntity> { entity };

            if (list != null)
            {
                swapedList = Swap(list, entity, IdFieldName);
            }

            jsonString = JsonConvert.SerializeObject(swapedList);

            _distributedCache.SetString(key, jsonString, _cacheOptions);

            return swapedList;
        }

        /// <summary>
        /// Swaps one entity from a list of enities.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> SwapOneAsync(TEntity entity, string IdFieldName, string key, CancellationToken cancellationToken = default)
        {
            //Get entry by key
            var jsonString = await _distributedCache.GetStringAsync(key, cancellationToken);

            try
            {
                //delete entry by key
                await _distributedCache.RemoveAsync(key, cancellationToken);
            }
            catch
            {
                //do nothing
            }

            IEnumerable<TEntity>? list = null;

            if (!string.IsNullOrEmpty(jsonString))
            {
                list = await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString));
            }

            //Swap item in list here.
            IEnumerable<TEntity> swapedList = new List<TEntity> { entity };

            if (list != null)
            {
                swapedList = await Task.FromResult(Swap(list, entity, IdFieldName));
            }

            jsonString = await Task.FromResult(JsonConvert.SerializeObject(swapedList));

            await _distributedCache.SetStringAsync(key, jsonString, _cacheOptions, cancellationToken);

            return swapedList;
        }

        /// <summary>
        /// Swaps one entity from a list of enities.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="ClassName">Child entity name (where IdFieldName is equal to Entity.ChildClass.IdFieldName).</param>
        /// <param name="IdFieldName">Id field name.</param>
        /// <param name="key">Cache key.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> SwapOne(TEntity entity, string ClassName, string IdFieldName, string key)
        {
            //Get entry by key
            var jsonString = _distributedCache.GetString(key);

            try
            {
                //delete entry by key
                _distributedCache.Remove(key);
            }
            catch
            {
                //do nothing
            }

            IEnumerable<TEntity>? list = null;

            if (!string.IsNullOrEmpty(jsonString))
            {
                list = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString);
            }

            //Swap item in list here.
            IEnumerable<TEntity> swapList = new List<TEntity> { entity };

            if (list != null)
            {
                swapList = Swap(list, entity, ClassName, IdFieldName);
            }

            jsonString = JsonConvert.SerializeObject(swapList);

            _distributedCache.SetString(key, jsonString, _cacheOptions);

            return swapList;
        }

        public async Task<IEnumerable<TEntity>> SwapRangeAsync(TEntity entity, string ClassName, string IdFieldName, string key, CancellationToken cancellationToken = default)
        {
            //Get entry by key
            var jsonString = await _distributedCache.GetStringAsync(key, cancellationToken);

            try
            {
                //delete entry by key
                await _distributedCache.RemoveAsync(key, cancellationToken);
            }
            catch
            {
                //do nothing
            }

            IEnumerable<TEntity>? list = null;

            if (!string.IsNullOrEmpty(jsonString))
            {
                list = await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString));
            }

            //Swap item in list here.
            IEnumerable<TEntity> swapedList = new List<TEntity> { entity };

            if (list != null)
            {
                swapedList = await Task.FromResult(Swap(list, entity, ClassName, IdFieldName));
            }

            jsonString = await Task.FromResult(JsonConvert.SerializeObject(swapedList));

            await _distributedCache.SetStringAsync(key, jsonString, _cacheOptions, cancellationToken);

            return swapedList;
        }

        /// <summary>
        /// Removes the cached data from the system.
        /// </summary>
        /// <param name="key">Cache key.</param>
        public bool Delete(string key)
        {
            //delete entry by key
            _distributedCache.Remove(key);

            return true;
        }

        /// <summary>
        /// Removes the cached data from the system.
        /// </summary>
        /// <param name="key">Cache key.</param>
        public async Task<bool> DeleteAsync(string key, CancellationToken cancellationToken = default)
        {
            //delete entry by key
            await _distributedCache.RemoveAsync(key, cancellationToken);

            return true;
        }

        private IEnumerable<TEntity> Swap(IEnumerable<TEntity> list, TEntity entity, string IdFieldName)
        {
            var newList = list.Where(x =>
            {
                var x_json = JsonConvert.SerializeObject(x);
                var entity_json = JsonConvert.SerializeObject(entity);
                return JObject.Parse(x_json)[IdFieldName].ToString() !=
                JObject.Parse(entity_json)[IdFieldName].ToString();
            }).ToList();
            newList.Add(entity);
            return newList;
        }

        private IEnumerable<TEntity> Swap(IEnumerable<TEntity> list, TEntity entity, string ClassName, string IdFieldName)
        {
            //Remove entity from list where IdFieldName is not equal to entity.IdFieldName
            var newList = list.Where(x =>
            {
                var x_json = JsonConvert.SerializeObject(x);
                var entity_json = JsonConvert.SerializeObject(entity);
                return JObject.Parse(x_json)[ClassName][IdFieldName].ToString() !=
                JObject.Parse(entity_json)[ClassName][IdFieldName].ToString();
            }).ToList();
            newList.Add(entity);
            return newList;
        }

        private IEnumerable<TEntity> GetUnion(IEnumerable<TEntity> list, IEnumerable<TEntity> list2, string IdFieldName)
        {
            List<TEntity> returnList = new List<TEntity>();

            for (var i = 0; i < list.Count(); i++)
            {
                var element = list.ElementAt(i);
                var id = JObject.Parse(JsonConvert.SerializeObject(element))[IdFieldName].ToString();
                var x2 = list2.Where(x => JObject.Parse(JsonConvert.SerializeObject(x))[IdFieldName].ToString() == id).FirstOrDefault();
                if (x2 == null)
                {
                    returnList.Add(element);
                }

            }
            returnList.AddRange(list2);
            return returnList;
        }

        private IEnumerable<TEntity> GetUnion(IEnumerable<TEntity> list, IEnumerable<TEntity> list2, string ClassName, string IdFieldName)
        {
            List<TEntity> returnList = new List<TEntity>();

            for (var i = 0; i < list.Count(); i++)
            {
                var element = list.ElementAt(i);
                var id = JObject.Parse(JsonConvert.SerializeObject(element))[ClassName][IdFieldName].ToString();
                var x2 = list2.Where(x => JObject.Parse(JsonConvert.SerializeObject(x))[ClassName][IdFieldName].ToString() == id).FirstOrDefault();
                if (x2 == null)
                {
                    returnList.Add(element);
                }

            }
            returnList.AddRange(list2);
            return returnList;
        }
    }
}
