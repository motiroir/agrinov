namespace AgriNov.Models
{
	public class BoxContract
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ContentDescription { get; set; }
		public decimal Price { get; set; }
		public DeliveryFrequency DeliveryFrequency { get; set; }
	}
}
