namespace AgriNov.Models
{
	public class BoxContractSubscription
	{
		public int Id { get; set; }
		public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public DateTime DateSigned { get; set; }
		public SubscriptionStatus SubscriptionStatus { get; set; }
        public BoxContract BoxContract { get; set; } = null!;

        // reference to user

        public ICollection<Box> Boxes { get; } = new List<Box>();
    }
}
