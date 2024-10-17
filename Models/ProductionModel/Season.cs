namespace AgriNov.Models.ProductionModel
{
    public class Season
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public ICollection<Stock> Stocks { get; set; }
        public ICollection<BoxSubscription> BoxSubscriptions { get; set; }
    }
}
