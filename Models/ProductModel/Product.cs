using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models.ProductModel
{
    public class Product
    {
        //internal object ProductType;

        [Key]
        public int Id { get; set; }

        public string Image { get; set; }
        public string DescriptionForReview { get; set; }
        public int Stock { get; set; }

        [Required, MaxLength(100)]
        public string Category { get; set; }

        public string ValidationStatus { get; set; }
    }
}
