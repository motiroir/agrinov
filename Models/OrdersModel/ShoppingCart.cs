using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class ShoppingCart
    {
        //One-to-One relationship, Shared Primary Key
        [Key, ForeignKey("UserAccount")]
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
        // Nabigation property to the ShoppingCartItems
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; } = new List<ShoppingCartItem>();
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public decimal Total { get; private set; }

        public ShoppingCart()
        {
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            // Sum the Total of each ShoppingCartItem in the collection
            Total = ShoppingCartItems.Sum(item => item.Total);
        }
    }
}