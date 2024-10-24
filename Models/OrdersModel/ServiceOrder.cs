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
            SaveShoppingCartAsAnOrder(1, null);
            SaveShoppingCartAsAnOrder(12, null);
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void SaveShoppingCartAsAnOrder(string shoppingCartIdStr, Payment payment)
        {
            int id;
            if (int.TryParse(shoppingCartIdStr, out id))
            {
                SaveShoppingCartAsAnOrder(id, payment);
            }
            return;
        }

        public void SaveShoppingCartAsAnOrder(int shoppingCartId, Payment payment)
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
                // If product or membership decrease stock, if membership mark as ok
                foreach (ShoppingCartItem item in shoppingCart.ShoppingCartItems)
                {
                    if (item.Product != null)
                    {
                        using (IProductService sP = new ProductService())
                        {
                            item.Product.Stock -= item.Quantity;
                            sP.UpdateProduct(item.Product);
                        }
                    }
                    if (item.MemberShipFee != null)
                    {
                        using (IServiceMemberShipFee sMF = new ServiceMemberShipFee())
                        {
                            item.MemberShipFee.WasPaid = true;
                            sMF.UpdateMemberShipFee(item.MemberShipFee);
                        }
                    }
                    if(item.BoxContract != null)
                    {
                        using(IServiceBoxContract sBC = new ServiceBoxContract())
                        {
                            item.BoxContract.MaxSubscriptions -= item.Quantity;
                            sBC.UpdateBoxContract(item.BoxContract);
                        }
                    }
                }
                // Copy it to order and order item
                Order order = new Order() { UserAccountId = shoppingCart.UserAccountId, Total = shoppingCart.Total, Payment = payment };
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
                    else if(item.BoxContract != null)
                    {
                        oItem.BoxContractId = item.BoxContractId;
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

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }
        public List<Order> GetAllOrdersForUserAccount(int userAccountId)
        {
            throw new NotImplementedException();

        }
        public List<Order> GetAllOrdersForUserAccount(string userAccountIdStr)
        {
            throw new NotImplementedException();

        }
    }
}