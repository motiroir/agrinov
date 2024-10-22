using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set;}
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public decimal Total { get; private set; }
        // Relationship to the Order
        public int OrderId {get; set;}
        public Order Order {get; set;}
        //Optional relationship
        public int? MemberShipFeeId { get; set; }
        public MemberShipFee MemberShipFee { get; set; }

        //Optional relationship
        public int? ProductId {get; set;}
        public Product Product {get; set;}

    }
}