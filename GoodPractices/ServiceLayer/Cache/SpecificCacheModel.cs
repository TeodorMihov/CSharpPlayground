namespace GoodPractices.ServiceLayer.Cache
{
    public class SpecificCacheModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{this.Id}_{this.Name}";
        }
    }
}
