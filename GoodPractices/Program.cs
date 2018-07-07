namespace GoodPractices
{
    using System;
    using System.Timers;
    using GoodPractices.ServiceLayer;
    using GoodPractices.ServiceLayer.Cache;

    public class Program
    {
        public static void Main(string[] args)
        {
        }

        private static void CacheUsage()
        {
            // initialize all cache instances
            BaseCacheManager.Initialize();

            var timer = new Timer
            {
                Interval = new TimeSpan(0, 0, 5).TotalMilliseconds
            };

            timer.Elapsed += (s, ev) =>
            {
                var cacheResult = SpecificCacheImplementation.Instance.Get(); // get refreshed cache data

                Console.WriteLine(string.Join(", ", cacheResult));
            };

            timer.Start();

            // something to block main thread
            Console.ReadKey();
        }

        private static void ServiceLayerUsage()
        {
            // has error
            var result = ServiceResultBase.Error("Some error message");

            if (result.IsError)
            {
                Console.WriteLine(result.ErrorMessage);
            }

            // no error
            Console.WriteLine(ServiceResultBase.Success);
        }
    }
}
