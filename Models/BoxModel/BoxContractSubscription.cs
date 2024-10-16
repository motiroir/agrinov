namespace AgriNov.Models
{
	public class BoxContractSubscription
	{
		public int Id { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateSigned { get; set; }
		public SubscriptionStatus SubscriptionStatus { get; set; }
	}
}
