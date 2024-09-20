using CFEventHandler.Interfaces;
using System;

namespace CFEventHandler.Services
{
    /// <summary>
    /// Base class for cache service
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    /// <typeparam name="TIDType"></typeparam>
    public abstract class CacheService<TEntityType, TIDType> : ICacheService<TEntityType, TIDType>
    {
        protected readonly Dictionary<TIDType, TEntityType> _cache = new Dictionary<TIDType, TEntityType>();

        public IEnumerable<TEntityType> GetAll()
        {
            return _cache.Values.ToList();
        }

        public void Add(TEntityType entity, TIDType id)
        {
            if (_cache.ContainsKey(id)) _cache.Remove(id);
            _cache.Add(id, entity);
        }

        public TEntityType? GetById(TIDType id)
        {
            return _cache.ContainsKey(id) ? _cache[id] : default(TEntityType);
        }

        public void DeleteById(TIDType id)
        {
            if (_cache.ContainsKey(id)) _cache.Remove(id);
        }

        public void DeleteAll()
        {
            _cache.Clear();
        }
    }
}
