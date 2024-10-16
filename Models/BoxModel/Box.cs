namespace AgriNov.Models
{
	public class Box
	{
		public DateTime DatePickUp { get; set; }
		public BoxStatus Status { get; set; }
        public BoxContractSubscription BoxContractSubscription { get; set; } = null!;

    }
}
