using AgriNov.Models.SharedStatus;
using System.ComponentModel;

namespace AgriNov.Models.ProductionModel
{
    public class Production
    {
        public int Id { get; set; }

        [DisplayName("Quantité (arrondir au kg)")]
        public int VolumePerDelivery { get; set; }

        public decimal Price { get; set; }

        public DateTime DateLimitForReview { get; set; }

        public ValidationStatus ValidationStatus { get; set; }

        public ProductType ProductType { get; set; }

        public DeliveryFrequency DeliveryFrequency { get; set; }

    }
}
