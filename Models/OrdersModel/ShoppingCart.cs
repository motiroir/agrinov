using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class ShoppingCart
    {
        public int Id {get; set;}
        public int UserAccountId {get; set;}
        public ICollection<ShoppingCartItem> ShoppingCartItems {get;} = new List<ShoppingCartItem>();
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public decimal Total { get; private set; }

    }
}