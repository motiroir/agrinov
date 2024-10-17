using AgriNov.Models.ProductionModel;

namespace AgriNov.Models
{
	public class BoxContract
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ContentDescription { get; set; }
		public decimal Price { get; set; }
		public DeliveryFrequency DeliveryFrequency { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public DateTime SaleStartingDate { get; set; }
        public DateTime SaleEndingDate { get; set; }
        public int NumberOfBCS { get; set; }
        public ICollection<BoxSubscription> BoxContractSubscriptions { get; } = new List<BoxSubscription>();

    }


}
