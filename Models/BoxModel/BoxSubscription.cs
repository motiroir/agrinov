using AgriNov.Models;
using AgriNov.Models.BoxModel;
using AgriNov.Models.ProductionModel;

namespace AgriNov.Models
{
	public class BoxSubscription
	{
		public int Id { get; set; }
        public BoxSize BoxSize { get; set; }
        public List<ProductType> ProductTypes { get; set; } = null!;
        public DeliveryFrequency DeliveryFrequency { get; set; }
		public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateSigned { get; set; }
		public SubscriptionStatus SubscriptionStatus { get; set; }
        public Season Season { get; set; } = null!;
        public UserAccount UserAccount { get; set; } = null!;
        public virtual ICollection<Box> Boxes { get; set; }
    }
}
