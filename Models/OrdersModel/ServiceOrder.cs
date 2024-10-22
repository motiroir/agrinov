using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class ServiceOrder : IServiceOrder
    {
        private BDDContext _DBContext;

        public ServiceOrder()
        {
            _DBContext = new BDDContext();
        }
        public void Dispose()
        {
            _DBContext.Dispose();
        }

        public void InitializeTable()
        {
            SaveShoppingCartAsAnOrder(2);
            SaveShoppingCartAsAnOrder(2);
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void SaveShoppingCartAsAnOrder(int shoppingCartId)
        {
            //Get Shopping Cart
            ShoppingCart shoppingCart;
            using (IServiceShoppingCart sSC = new ServiceShoppingCart())
            {
                shoppingCart = sSC.GetShoppingCartForUserAccount(shoppingCartId);
                //If empty or not found
                if (shoppingCart == null || !shoppingCart.ShoppingCartItems.Any())
                {
                    return;
                }
                // Copy it to order and order item
                Order order = new Order() { UserAccountId = shoppingCart.UserAccountId, Total = shoppingCart.Total };
                foreach (ShoppingCartItem item in shoppingCart.ShoppingCartItems)
                {
                    OrderItem oItem = new OrderItem()
                    {
                        Quantity = item.Quantity,
                        Total = item.Total
                    };
                    if (item.MemberShipFeeId != null)
                    {
                        oItem.MemberShipFeeId = item.MemberShipFeeId;
                    }
                    else if (item.ProductId != null)
                    {
                        oItem.ProductId = item.ProductId;
                    }
                    order.OrderItems.Add(oItem);
                }
                InsertOrder(order);
                //Empty Shopping Cart
                sSC.EmptyShoppingCart(shoppingCart.UserAccountId);
            }



        }

        public void InsertOrder(Order order)
        {
            _DBContext.Orders.Add(order);
            Save();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}