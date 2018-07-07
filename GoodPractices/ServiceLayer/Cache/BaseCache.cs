namespace GoodPractices.ServiceLayer.Cache
{
    using System;
    using System.Timers;

    public abstract class BaseCache
    {
        private readonly TimeSpan refreshInterval;

        public BaseCache(TimeSpan refreshInterval)
        {
            this.refreshInterval = refreshInterval;
        }

        public void Initialize()
        {
            try
            {
                UpdateCache();

                var timer = new Timer
                {
                    Interval = this.refreshInterval.TotalMilliseconds,
                };

                timer.Elapsed += (sender, ev) => { UpdateCache(); };
                timer.Start();
            }
            catch (Exception)
            {
                // TODO: Log exception
                throw;
            }
        }

        protected abstract void UpdateCache();
    }
}
