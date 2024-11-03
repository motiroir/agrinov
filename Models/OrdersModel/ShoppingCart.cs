using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriNov.Models
{
    public class ShoppingCart
    {
        //One-to-One relationship, Shared Primary Key
        [Key, ForeignKey("UserAccount")]
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
        // Nabigation property to the ShoppingCartItems
        public List<ShoppingCartItem> ShoppingCartItems { get; } = new List<ShoppingCartItem>();
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public decimal Total { get; private set; }

        public ShoppingCart()
        {
        }

        public void CalculateTotal()
        {
            // Sum the Total of each ShoppingCartItem in the collection
            Total = ShoppingCartItems.Sum(item => item.Total);
        }
    }
}