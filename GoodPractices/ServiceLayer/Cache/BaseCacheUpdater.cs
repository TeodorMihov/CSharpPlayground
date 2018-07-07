namespace GoodPractices.ServiceLayer.Cache
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BaseCacheUpdater<T> : BaseCache
    {
        private static int FilteredParameterKey = 2; // remove this is only to test updater 

        private readonly ConcurrentDictionary<int, ICollection<T>> CachedData =
            new ConcurrentDictionary<int, ICollection<T>>();

        public BaseCacheUpdater(TimeSpan refreshInterval)
            : base(refreshInterval)
        { }

        protected sealed override void UpdateCache()
        {
            try
            {
                // remove this is only to test updater 
                if (FilteredParameterKey == 1)
                {
                    FilteredParameterKey = 2;
                }
                else
                {
                    FilteredParameterKey = 1;
                }

                // filter all tables or apis and pass key for caching
                var result = GetData(FilteredParameterKey);

                this.CachedData.AddOrUpdate(FilteredParameterKey, result, (key, value) => value = result);
            }
            catch (Exception)
            {
                // TODO: log error
                throw;
            }
        }

        public List<T> Get()
        {
            return new List<T>(this.CachedData.SelectMany(c => c.Value).ToList());
        }

        protected virtual ICollection<T> GetData(int parameterToGetData)
        {
            return new List<T>();
        }
    }
}
