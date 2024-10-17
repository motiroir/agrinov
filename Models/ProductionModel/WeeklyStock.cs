namespace AgriNov.Models.ProductionModel
{
    public class WeeklyStock
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public int AvailableQuantity { get; set; }
        public Stock Stock { get; set; }
    }
}
