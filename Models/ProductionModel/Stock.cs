namespace AgriNov.Models.ProductionModel
{
    public class Stock
    {
        public int Id { get; set; }
        public ProductType ProductType { get; set; } 
        public int TotalQuantity { get; set; } 
        public Season Season { get; set; }
        public virtual ICollection<WeeklyStock> WeeklyStocks { get; set; }

        public virtual ICollection<Production> Productions { get; set; }
    }
}
