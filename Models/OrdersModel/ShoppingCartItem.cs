using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                UpdateTotal();
                DateLastModified = DateTime.Now;
            }
        }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public decimal Total { get; private set; }
        public int UserAccountId { get; set; }
        public int MemberShipFeeId { get; set; }
        public MemberShipFee MemberShipFee { get; set; }

        public ShoppingCartItem()
        {
            DateCreated = DateTime.Now;
        }

        private void UpdateTotal()
        {
            if (MemberShipFee != null)
            {
                Total = _quantity * MemberShipFee.Price;
            }
            else
            {
                Total = 0;
            }
        }
    }
}