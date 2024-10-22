using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class Order
    {
        public int Id {get; set;}
        public int UserAccountId { get; set; }
        public ICollection<OrderItem> OrderItems {get; }= new List<OrderItem>();
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public decimal Total { get; private set; }
        public bool WasDelivered {get; set;}
        public Payment Payment {get; set;}
    }

}