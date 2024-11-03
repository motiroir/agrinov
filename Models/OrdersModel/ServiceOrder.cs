using Microsoft.EntityFrameworkCore;

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
            Payment p1 = new Payment
            {
                Date = new DateTime(2024, 10, 19),
                PaymentType = PaymentType.CARD,
                Received = true
            };
            Payment p2 = new Payment
            {
                Date = new DateTime(2024, 10, 24),
                PaymentType = PaymentType.CASH,
                Received = true
            };
            Payment p3 = new Payment
            {
                Date = new DateTime(2024, 10, 19),
                PaymentType = PaymentType.CARD,
                Received = true
            };
            Payment p4 = new Payment
            {
                Date = new DateTime(2024, 10, 24),
                PaymentType = PaymentType.CARD,
                Received = true
            };

            Payment p5 = new Payment
            {
                Date = new DateTime(2024, 10, 24),
                PaymentType = PaymentType.CASH,
                Received = true
            };

            Payment p6 = new Payment
            {
                Date = new DateTime(2024, 10, 26),
                PaymentType = PaymentType.CARD,
                Received = true
            };

            Payment p7 = new Payment
            {
                PaymentType = PaymentType.NA,
                Received = false
            };

            Payment p8 = new Payment
            {
                Date = new DateTime(2024, 10, 28),
                PaymentType = PaymentType.CARD,
                Received = true
            };


            Payment p9 = new Payment
            {
                PaymentType = PaymentType.NA,
                Received = false
            };

            SaveShoppingCartAsAnOrder(1, p1);
            SaveShoppingCartAsAnOrder(12, p2);
            SaveShoppingCartAsAnOrder(4, p3);
            SaveShoppingCartAsAnOrder(15, p4);  
            SaveShoppingCartAsAnOrder(17, p5);   
            SaveShoppingCartAsAnOrder(10, p6);  
            SaveShoppingCartAsAnOrder(2, p7);   
            SaveShoppingCartAsAnOrder(9, p8);  
            SaveShoppingCartAsAnOrder(3, p9);

            Order o1 = GetOrderById(1);
            Order o2 = GetOrderById(2);
            Order o3 = GetOrderById(3);
            Order o4 = GetOrderById(4);
            Order o5 = GetOrderById(5);
            Order o6 = GetOrderById(6);
            Order o7 = GetOrderById(7);
            Order o8 = GetOrderById(8);
            Order o9 = GetOrderById(9);
               
            
            o1.DateCreated = new DateTime(2024, 10, 15);
            o1.WasDelivered = true;
            
            o2.DateCreated = new DateTime(2024, 10, 15);
            o2.WasDelivered = true;

            o3.DateCreated = new DateTime(2024, 10, 16);
            o3.WasDelivered = true;

            o4.DateCreated = new DateTime(2024, 10, 16);
            o4.WasDelivered = true;

            o5.DateCreated = new DateTime(2024, 10, 16);
            o5.WasDelivered = true;


            o6.DateCreated = new DateTime(2024, 10, 26);

            o7.DateCreated = new DateTime(2024, 10, 26);

            o8.DateCreated = new DateTime(2024, 10, 28);


            o9.DateCreated = new DateTime(2024, 10, 28);

            UpdateOrder(o1);
            UpdateOrder(o2);
            UpdateOrder(o3);
            UpdateOrder(o4);
            UpdateOrder(o5);
            UpdateOrder(o6);
            UpdateOrder(o7);
            UpdateOrder(o8);
            UpdateOrder(o9);
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
            //Does not recaculate prices and totals, do not use for updating anything other than payment and delivery
            //Do not modify order items with this
            Order oldOrder = _DBContext.Orders.FirstOrDefault(o=> o.Id == order.Id);
            _DBContext.Entry(oldOrder).CurrentValues.SetValues(order);
            Save();
        }

        public List<Order> GetAllOrdersWithoutDetails()
        {
            return _DBContext.Orders.ToList();
        }

        public List<Order> GetAllOrdersWithJustUserDetails()
        {
            return _DBContext.Orders.Include(order => order.UserAccount).ThenInclude(ua => ua.User)
                                    .Include(order => order.UserAccount).ThenInclude(ua => ua.Supplier)
                                    .Include(order => order.UserAccount).ThenInclude(ua => ua.CorporateUser).ToList();
        }
        public List<Order> GetAllOrders()
        {
            return _DBContext.Orders.Include(order => order.OrderItems).ThenInclude(orderItem => orderItem.MemberShipFee)
                                    .Include(order => order.OrderItems).ThenInclude(orderItem => orderItem.Product)
                                    .Include(order => order.OrderItems).ThenInclude(orderItem => orderItem.BoxContract)
                                    .ToList();
        }
        public List<Order> GetAllOrdersForUserAccount(int userAccountId)
        {
            return _DBContext.Orders.Where(order => order.UserAccountId == userAccountId)
                                    .Include(order => order.OrderItems).ThenInclude(orderItem => orderItem.MemberShipFee)
                                    .Include(order => order.OrderItems).ThenInclude(orderItem => orderItem.Product)
                                    .Include(order => order.OrderItems).ThenInclude(orderItem => orderItem.BoxContract)
                                    .ToList();

        }
        public List<Order> GetAllOrdersForUserAccount(string userAccountIdStr)
        {
            int id;
            if (int.TryParse(userAccountIdStr, out id))
            {
                return GetAllOrdersForUserAccount(id);
            }
            return null;
        }

        public Order GetOrderById(int orderId)
        {
            return this._DBContext.Orders.Find(orderId);
        }

        public Order GetOrderById(string orderIDStr)
        {
            int id;
            if (int.TryParse(orderIDStr, out id))
            {
                return this.GetOrderById(id);
            }
            return null;
        }
    }
}