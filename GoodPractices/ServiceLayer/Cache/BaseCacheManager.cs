namespace GoodPractices.ServiceLayer.Cache
{
    using System;

    public static class BaseCacheManager
    {
        public static readonly TimeSpan RefreshInterval = new TimeSpan(0, 0, 10);

        public static void Initialize()
        {
            SpecificCacheImplementation.Instance.Initialize();
        }
    }
}
