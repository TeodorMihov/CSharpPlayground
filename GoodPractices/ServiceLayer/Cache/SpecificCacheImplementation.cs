namespace GoodPractices.ServiceLayer.Cache
{
    using System.Collections.Generic;

    public class SpecificCacheImplementation : BaseCacheUpdater<SpecificCacheModel>
    {
        public static readonly SpecificCacheImplementation Instance = new SpecificCacheImplementation();

        public SpecificCacheImplementation() 
            : base(BaseCacheManager.RefreshInterval)
        { }

        protected override ICollection<SpecificCacheModel> GetData(int parameterToGetData)
        {
            // Get your data from DB or API or whatever you want
            if (parameterToGetData == 1)
            {
                return new List<SpecificCacheModel>
                {
                    new SpecificCacheModel
                    {
                        Id = 1,
                        Name = "Test"
                    }
                };
            }
            if (parameterToGetData == 2)
            {
                return new List<SpecificCacheModel>
                {
                    new SpecificCacheModel
                    {
                        Id = 3,
                        Name = "Test"
                    },
                     new SpecificCacheModel
                    {
                        Id = 4,
                        Name = "Test_2"
                    }
                };
            }
            return base.GetData(parameterToGetData);
        }
    }
}
