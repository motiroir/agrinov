namespace AgriNov.Models.ProductModel
{
    public class Product
    {
        internal object ProductType;

        [Key]
        public int Id { get; set; }

        public string Image { get; set; }
        public string DescriptionForReview { get; set; }
        public int Stock { get; set; }

        [Required, MaxLength(100)]
        public string Category { get; set; }

        public string ValidationStatus { get; set; }


        // Navigation properties
        ///public virtual ICollection<ProductFavorite> ProductFavorites { get; set; }
        // public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        // public virtual ICollection<OrderItem> OrderItems { get; set; }
        public DateTime DateCreated { get; internal set; }
        public DateTime DateLastModified { get; internal set; }
        public int VolumePerDelivery { get; internal set; }
        public int Price { get; internal set; }
        public DateTime DateLimitForReview { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
    }



    // Definition for ShoppingCartItem class
    public class ShoppingCartItem
    {
        public int Id { get; set; } // Add properties as needed
        public int ProductId { get; set; } // Foreign key to Product
        public int Quantity { get; set; }

        public virtual Product Product { get; set; } // Navigation property
    }
}
